using System;
using MTFS.DAL.Context;
using System.Data.Entity;
using MTFS.Business.Domain.Model;
using System.Linq;
using AutoMapper;
using System.Collections.Generic;
using MTFS.Business.Dtos.DtoClasses;
using MTFS.Business.Services.Interfaces;
using System.Threading.Tasks;
using MTFS.Business.Services.ExtentionMethods;
using MTFS.Utilities.Enum;
using MTFS.Utilities.General;

namespace MTFS.Business.Services.Classes
{
    public class NegotiationService : INegotiationService
    {
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Negotiation> _Negotiations;
        private readonly ICurrencyService _CurrencyService;
        private readonly ICustomerService _CustomerService; 
        private readonly ILocationService _LocationService;

        public NegotiationService(IUnitOfWork uow,
                                   ICurrencyService currencyService,
                                   ICustomerService customerService, 
                                   ILocationService locationService)
        {

            _uow = uow;
            _Negotiations = _uow.Set<Negotiation>();
            _CurrencyService = currencyService;
            _CustomerService = customerService; 
            _LocationService = locationService;

        }

        #region Retrive Data 

        public async Task<GetNegotiationsManagementDto> getNegotiationsManagement(GridInitialDto gridInitialDto)
        {
            if (gridInitialDto.pageNo < 1)
                gridInitialDto.pageNo = 1;


            var oNegotiationsManagementDto = new GetNegotiationsManagementDto();


            IQueryable<Negotiation> oNegotiations = _Negotiations.Include(r => r.placeofDeliveryLocation)
                                                                 .Include(r => r.placeofReceiptLocation)
                                                                 .Include(i => i.currency).Include(i => i.contractor);

            if (!string.IsNullOrEmpty(gridInitialDto.filter))
                oNegotiations = oNegotiations
                    .Where(w => w.referenceNo.Contains(gridInitialDto.filter) == true);

            int totalRecordCount = await oNegotiations.AsNoTracking().CountAsync();

            if (totalRecordCount != 0)
            {

                int totalPages = (int)Math.Ceiling((double)totalRecordCount / gridInitialDto.recordCountPerPage);
                if (gridInitialDto.pageNo > totalPages)
                    gridInitialDto.pageNo = totalPages;

                var intFrom = (gridInitialDto.pageNo - 1) * gridInitialDto.recordCountPerPage;
                oNegotiationsManagementDto.getOutNegotiationsDto = Mapper.Map<IEnumerable<Negotiation>, List<GetNegotiationsDto>>(await oNegotiations.GetPageRecords(gridInitialDto.pageNo, gridInitialDto.recordCountPerPage).ToListAsync());

                oNegotiationsManagementDto.currentPage = gridInitialDto.pageNo;
                oNegotiationsManagementDto.totalRecordCount = totalRecordCount;

            }

            return oNegotiationsManagementDto;
        }

        public async Task<GetNegotiationDto> getNegotiationInitial()
        {
            return await fillDdl(new GetNegotiationDto());
        }

        public async Task<GetNegotiationDto> getNegotiation(BaseDto baseDto)
        {
            Negotiation oNegotiation = await _Negotiations.AsNoTracking().SingleOrDefaultAsync(i => i.id == baseDto.id);
            GetNegotiationDto oNegotiationDto = Mapper.Map<Negotiation, GetNegotiationDto>(oNegotiation);

            oNegotiationDto.state =(Utilities.Enum.NegotiationStates) oNegotiation.state;

            if (oNegotiation.contractorId == oNegotiation.shipperId)
                oNegotiationDto.contractor = CustomerTypes.Shipper;

            else if (oNegotiation.contractorId == oNegotiation.consigneeId)
                oNegotiationDto.contractor = CustomerTypes.Consignee;

            else if (oNegotiation.contractorId == oNegotiation.notify1Id)
                oNegotiationDto.contractor = CustomerTypes.Notify1;

            else if (oNegotiation.contractorId == oNegotiation.notify2Id)
                oNegotiationDto.contractor = CustomerTypes.Notify2;

            if (oNegotiation.notify1Id == oNegotiation.consigneeId)
                oNegotiationDto.notify1Id = 0;

            if (oNegotiation.notify2Id == oNegotiation.consigneeId)
                oNegotiationDto.notify2Id = 0;

            return await fillDdl(oNegotiationDto);
        }
         
        #endregion

        public async Task<bool> insertNegotiation(GetNegotiationDto dto)
        {
            try
            {
                Negotiation oNegotiation = Mapper.Map<GetNegotiationDto, Negotiation>(dto);

                #region Create RefrenceNo

                string part1_Year = DateTime.Now.Year.ToString().Substring(2);
                string part2_Type = "O";
                string part3_counter = "00001";

                string currentMax = await _Negotiations.Where(i => i.referenceNo.Substring(0, 2) == part1_Year && i.referenceNo.Substring(2, 1) == part2_Type).Select(i => i.referenceNo.Substring(3)).MaxAsync();

                if (currentMax != null)
                    part3_counter = BuildKey.BuildReferenceKey(currentMax);

                oNegotiation.referenceNo = part1_Year + part2_Type + part3_counter;

                #endregion

                oNegotiation.isInboundProcess = false;

                #region Cost plust set total net price

                if (oNegotiation.isCostplus) 
                    oNegotiation.costplusPercent = dto.costplusPercent;  
                else 
                    oNegotiation.totalNetPrice = dto.totalNetPrice;  

                #endregion 

                oNegotiation.contractorId=SetContractorId(dto.contractor,dto.shipperId, dto.consigneeId, dto.notify1Id, dto.notify2Id);
                SetNotifiesId(oNegotiation, dto.notifys1);

                #region Cost plus and set total net price

                if (dto.notify1Id == 0 || dto.notify2Id == 0)
                {
                    if (dto.notifys1.SingleOrDefault(i => i.id == dto.consigneeId) != null)
                    {
                        //Notify1 same as consignee (consignee is required)
                        if (dto.notify1Id == 0)
                        {
                            oNegotiation.notify1Id = oNegotiation.consigneeId;
                        }
                        //Notify2 sane as consignee (consignee is required)
                        if (dto.notify2Id == 0)
                        {
                            oNegotiation.notify2Id = oNegotiation.consigneeId;
                        }
                    }

                }

                #endregion

                _Negotiations.Add(oNegotiation);

                await _uow.SaveChangesAsync();

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
         
        public async Task<bool> updateNegotiation(GetNegotiationDto dto)
        {
            try
            {
                var oNegotiation = await _Negotiations.SingleOrDefaultAsync(i => i.id == dto.id);
               
                #region Set other property

                oNegotiation.currencyId = dto.currencyId; 
                oNegotiation.marketerId = dto.marketerId;

                oNegotiation.placeofDeliveryLocationId = dto.placeofDeliveryLocationId;
                oNegotiation.placeofReceiptLocationId = dto.placeofReceiptLocationId;

                oNegotiation.referenceNo = dto.referenceNo;
                oNegotiation.shipperId = dto.shipperId;

                oNegotiation.consigneeId = dto.consigneeId;
                oNegotiation.state = (Domain.Model.NegotiationStates)dto.state;

                oNegotiation.goodsDesc = dto.goodsDesc;
                oNegotiation.modiferUserId = dto.userId;

                oNegotiation.notify1Id = dto.notify1Id;
                oNegotiation.notify2Id = dto.notify2Id;

                #endregion

                SetNotifiesId(oNegotiation, dto.notifys1);

                oNegotiation.contractorId=SetContractorId(dto.contractor,dto.shipperId, dto.consigneeId, oNegotiation.notify1Id, oNegotiation.notify2Id);

                oNegotiation.isInboundProcess = false;

                #region Costplus and percent

                oNegotiation.isCostplus = dto.isCostplus;

                if (oNegotiation.isCostplus)
                {
                    oNegotiation.costplusPercent = dto.costplusPercent;
                    oNegotiation.totalNetPrice = null; 
                } 
                else
                {
                    oNegotiation.totalNetPrice = dto.totalNetPrice; 
                    oNegotiation.costplusPercent = null;
                }

                #endregion
                  
                await _uow.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
         
        #region Private Method

        private async Task<GetNegotiationDto> fillDdl(GetNegotiationDto oNegotiationDto)
        {

            oNegotiationDto.currencies = await _CurrencyService.getCurrenciesDdlDto();
            //oNegotiationDto.marketers = await _MarketerService.getMarketersDdlDto();
            oNegotiationDto.placeofDeliveryLocations = await _LocationService.getLocationsDdlDto();
            oNegotiationDto.placeofReceiptLocations = await _LocationService.getLocationsDdlDto();
            oNegotiationDto.shippers = await _CustomerService.getCustomersDdlDto(i => i.isActive == true && i.isShipper == true);
            oNegotiationDto.consignees = await _CustomerService.getCustomersDdlDto(i => i.isActive == true && i.isConsignee == true);

            List<DdlDto> notifyList = await _CustomerService.getCustomersDdlDto(i => i.isActive == true && i.isNotify == true);
            notifyList.Add(new DdlDto { id = 0, text = "Same As Consignee" });

            oNegotiationDto.notifys1 = notifyList.OrderBy(i => i.id).ToList();
            oNegotiationDto.notifys2 = oNegotiationDto.notifys1;

            List<DdlDto> negotiationStatesList = new List<DdlDto>();
            foreach (var value in Enum.GetValues(typeof(Domain.Model.NegotiationStates)))
            {
                negotiationStatesList.Add(new DdlDto { id = (byte)value, text = value.ToString() });
            }
            oNegotiationDto.states = negotiationStatesList;

            List<DdlDto> contractorStatesList = new List<DdlDto>();
            foreach (var value in Enum.GetValues(typeof(CustomerTypes)))
            {
                contractorStatesList.Add(new DdlDto { id = (byte)value, text = value.ToString() });
            }
            oNegotiationDto.contractors = contractorStatesList;

            return oNegotiationDto;
        }

        private int SetContractorId(CustomerTypes contractor, int shipperId, int consigneeId, int? notify1Id, int? notify2Id)
        {
            int contractId = 0;
            switch (contractor)
            {
                case CustomerTypes.Shipper:
                    contractId =shipperId;
                    break;
                case CustomerTypes.Consignee:
                    contractId = consigneeId;
                    break;
                case CustomerTypes.Notify1:
                    contractId = notify1Id.Value;
                    break;
                case CustomerTypes.Notify2:
                    contractId = notify2Id.Value;
                    break;
            }
            return contractId;
        }
         
        private void  SetNotifiesId(Negotiation negotiation,List<DdlDto> notifies)
        {
            if (negotiation.notify1Id == 0 || negotiation.notify2Id == 0)
            {
                if (notifies.SingleOrDefault(i => i.id == negotiation.consigneeId) != null)
                {
                    //Notify1 same as consignee (consignee is required)
                    if (negotiation.notify1Id == 0)
                    {
                        negotiation.notify1Id = negotiation.consigneeId;
                    }
                    //Notify2 sane as consignee (consignee is required)
                    if (negotiation.notify2Id == 0)
                    {
                        negotiation.notify2Id = negotiation.consigneeId;
                    }
                }
            }

            if (negotiation.notify2Id == 0)
                negotiation.notify2Id = null;

            if (negotiation.notify1Id == 0)
                negotiation.notify1Id = null;
        }
       
        #endregion

    }
}
