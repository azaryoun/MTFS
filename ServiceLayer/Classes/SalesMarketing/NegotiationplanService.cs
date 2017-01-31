using MTFS.Business.Domain.Model;
using MTFS.Business.Services.Interfaces;
using MTFS.DAL.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MTFS.Business.Dtos.DtoClasses;
using AutoMapper;
using MTFS.Business.Services.ExtentionMethods;
using System.Threading.Tasks;

namespace MTFS.Business.Services.Classes
{
    public class NegotiationplanService : INegotiationplanService
    {
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Negotiationplan> _Negotiationplans;
        private readonly IDbSet<Negotiation> _Negotiation;

        public NegotiationplanService(IUnitOfWork uow)
        {
            _uow = uow;
            _Negotiationplans = _uow.Set<Negotiationplan>();
            _Negotiation = _uow.Set<Negotiation>();
        }

        #region Retrive Data 
        public async Task<GetNegotiationplansManagementDto> getNegotiationplansManagement(GridChildInitialDto gridInitialDto)
        {
            int pageNo = gridInitialDto.pageNo;

            if (gridInitialDto.pageNo < 1) pageNo = 1;

            IQueryable<Negotiationplan> oNegotiationplans = _Negotiationplans.Where(i => i.negotiationId == gridInitialDto.parentId).AsQueryable();

            if (!string.IsNullOrEmpty(gridInitialDto.filter))
                oNegotiationplans = oNegotiationplans
                       .Where(w => w.planName.Contains(gridInitialDto.filter) == true);

            int totalRecordCount = await oNegotiationplans.AsNoTracking().CountAsync();

            GetNegotiationplansManagementDto oGetNegotiationplansManagementDto = new GetNegotiationplansManagementDto();

            if (totalRecordCount != 0)
            {

                int totalPages = (int)Math.Ceiling((double)totalRecordCount / gridInitialDto.recordCountPerPage);

                if (pageNo > totalPages) pageNo = totalPages;


                oGetNegotiationplansManagementDto.getNegotiationplansDto = Mapper.Map<IEnumerable<Negotiationplan>, List<GetNegotiationplansDto>>
                                                       (await oNegotiationplans.GetPageRecords(pageNo, gridInitialDto.recordCountPerPage).ToListAsync());

                oGetNegotiationplansManagementDto.currentPage = pageNo;
                oGetNegotiationplansManagementDto.totalRecordCount = totalRecordCount;

            }

            oGetNegotiationplansManagementDto.title = gridInitialDto.parentTitle;
            oGetNegotiationplansManagementDto.parent_ParentId = gridInitialDto.parentId;

            return oGetNegotiationplansManagementDto;
        }

        public async Task<GetNegotiationplanDto> getNegotiationplan(BaseDto baseDto)
        {
            return Mapper.Map<Negotiationplan, GetNegotiationplanDto>(
                await _Negotiationplans.AsNoTracking().SingleOrDefaultAsync(i => i.id == baseDto.id));

        }

        #endregion

        public async Task<bool> insertNegotiationplan(GetNegotiationplanDto getNegotiationplanDto)
        {
            try
            {
                Negotiationplan oNegotiationplan = Mapper.Map<GetNegotiationplanDto, Negotiationplan>(getNegotiationplanDto);
                _Negotiationplans.Add(oNegotiationplan);
                await _uow.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> updateNegotiationplan(GetNegotiationplanDto getNegotiationplanDto)
        {
            try
            {
                Negotiationplan oNegotiationplan = await _Negotiationplans.SingleAsync(i => i.id == getNegotiationplanDto.id);

                if (!oNegotiationplan.isAccepted && getNegotiationplanDto.isAccepted )
                {
                    if (await _Negotiationplans.AnyAsync(i => i.negotiationId == getNegotiationplanDto.negotiationId && i.isAccepted == true))
                            return true;
                }

                if (getNegotiationplanDto.isAccepted)
                {

                    Negotiation ONegotiation = await _Negotiation.SingleAsync(i => i.id == getNegotiationplanDto.negotiationId);
                    ONegotiation.state = NegotiationStates.ConfirmedbyCustomer;

                }

                oNegotiationplan.planName = getNegotiationplanDto.planName;
                oNegotiationplan.isAccepted = getNegotiationplanDto.isAccepted;
                oNegotiationplan.modiferUserId = getNegotiationplanDto.userId;

                await _uow.SaveChangesAsync();

                return true;
            }
            catch
            {

                return false;
            }
        }

    }
}
