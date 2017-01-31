using AutoMapper;
using MTFS.Business.Domain.Model;
using MTFS.Business.Dtos.DtoClasses;
using MTFS.Business.Services.ExtentionMethods;
using MTFS.Business.Services.Interfaces;
using MTFS.DAL.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace MTFS.Business.Services.Classes
{
    public class NegotiationplanrouteCFService : INegotiationplanrouteCFService
    {
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<NegotiationplanrouteCF> _NegotiationplanrouteCFs;  
        private readonly IAgentCarrierService _AgentCarrierService;
        private readonly INegotiationplanrouteService _NegotiationplanrouteService;

        public NegotiationplanrouteCFService(IUnitOfWork uow,   
                                           IAgentCarrierService agentCarrierService,
                                           INegotiationplanrouteService negotiationplanrouteService)
        {
            _uow = uow;
            _NegotiationplanrouteCFs = _uow.Set<NegotiationplanrouteCF>();
            _AgentCarrierService = agentCarrierService; 
            _NegotiationplanrouteService = negotiationplanrouteService;
        }

        #region Retrive Data 
        public async Task<GetNegotiationplanrouteCFsManagementDto> getNegotiationplanrouteCFsManagement(GridChildInitialDto gridInitialDto)
        {
            int pageNo = gridInitialDto.pageNo;

            if (gridInitialDto.pageNo < 1) pageNo = 1;

            IQueryable<NegotiationplanrouteCF> oNegotiationplanrouteCFs = _NegotiationplanrouteCFs.Include(i => i.carrier)
                                                                        .Include(i => i.forwarder)
                                                                        .Include(i => i.agent ) 
                                                                        .Where(i => i.negotiationplanrouteId == gridInitialDto.parentId).AsQueryable();

            if (!string.IsNullOrEmpty(gridInitialDto.filter))
                oNegotiationplanrouteCFs = oNegotiationplanrouteCFs
                       .Where(w => w.carrier.fullName.Contains(gridInitialDto.filter) == true);

            int totalRecordCount = await oNegotiationplanrouteCFs.AsNoTracking().CountAsync();

            GetNegotiationplanrouteCFsManagementDto oGetNegotiationplanrouteCFsManagementDto = new GetNegotiationplanrouteCFsManagementDto();

            if (totalRecordCount != 0)
            {

                int totalPages = (int)Math.Ceiling((double)totalRecordCount / gridInitialDto.recordCountPerPage);

                if (pageNo > totalPages) pageNo = totalPages;


                oGetNegotiationplanrouteCFsManagementDto.getNegotiationplanrouteCFsDto = Mapper.Map<IEnumerable<NegotiationplanrouteCF>, List<GetNegotiationplanrouteCFsDto>>
                                                       (await oNegotiationplanrouteCFs.GetPageRecords(pageNo, gridInitialDto.recordCountPerPage).ToListAsync());

                oGetNegotiationplanrouteCFsManagementDto.currentPage = pageNo;
                oGetNegotiationplanrouteCFsManagementDto.totalRecordCount = totalRecordCount;

            }
            oGetNegotiationplanrouteCFsManagementDto.title = gridInitialDto.parentTitle;
            oGetNegotiationplanrouteCFsManagementDto.parent_ParentId = gridInitialDto.parentId;

            return oGetNegotiationplanrouteCFsManagementDto;
        }
        public async Task<GetNegotiationplanrouteCFDto> getNegotiationplanrouteCF(BaseDto baseDto)
        {

            GetNegotiationplanrouteCFDto oNegotiationplanrouteCFDto = Mapper.Map<NegotiationplanrouteCF, GetNegotiationplanrouteCFDto>(
                await _NegotiationplanrouteCFs.AsNoTracking().SingleOrDefaultAsync(i => i.id == baseDto.id));

            return await fillDdl(oNegotiationplanrouteCFDto);
        }
        public async Task<GetNegotiationplanrouteCFDto> getNegotiationplanrouteCFInitial(BaseDto baseDto)
        {

            GetNegotiationplanrouteCFDto oNegotiationplanrouteCFDto = new GetNegotiationplanrouteCFDto();

            oNegotiationplanrouteCFDto.negotiationplanrouteId  = baseDto.id;

            return await fillDdl(oNegotiationplanrouteCFDto);

        }

        #endregion

        public async Task<bool> insertNegotiationplanrouteCF(GetNegotiationplanrouteCFDto getNegotiationplanrouteCFDto)
        {
            try
            {
                NegotiationplanrouteCF oNegotiationplanrouteCF = Mapper.Map<GetNegotiationplanrouteCFDto, NegotiationplanrouteCF>(getNegotiationplanrouteCFDto);

                if (getNegotiationplanrouteCFDto.isAccepted)
                {
                    if (await _NegotiationplanrouteCFs.AsNoTracking().AnyAsync(i => i.negotiationplanrouteId == getNegotiationplanrouteCFDto.negotiationplanrouteId && i.isAccepted == true))
                        return true;
                }
                 
                if (getNegotiationplanrouteCFDto.carrierId != 0)
                    oNegotiationplanrouteCF.agentId = getNegotiationplanrouteCFDto.agentCarriers.Where(i => i.id == getNegotiationplanrouteCFDto.carrierId).Select(i => i.parentId).Single();

                _NegotiationplanrouteCFs.Add(oNegotiationplanrouteCF);
                await _uow.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> updateNegotiationplanrouteCF(GetNegotiationplanrouteCFDto getNegotiationplanrouteCFDto)
        {
            try
            {

                NegotiationplanrouteCF oNegotiationplanrouteCF = await _NegotiationplanrouteCFs.SingleAsync(i => i.id == getNegotiationplanrouteCFDto.id);

                if (!oNegotiationplanrouteCF.isAccepted && oNegotiationplanrouteCF.isAccepted)
                {
                    if (await _NegotiationplanrouteCFs.AnyAsync(i => i.negotiationplanrouteId == getNegotiationplanrouteCFDto.negotiationplanrouteId && i.isAccepted == true))
                        return true;
                }

                if (getNegotiationplanrouteCFDto.carrierId !=0) 
                    oNegotiationplanrouteCF.agentId = getNegotiationplanrouteCFDto.agentCarriers.Where(i => i.id == getNegotiationplanrouteCFDto.carrierId).Select(i => i.parentId).Single();
                   
                oNegotiationplanrouteCF.carrierId = getNegotiationplanrouteCFDto.carrierId;
                oNegotiationplanrouteCF.forwarderId = getNegotiationplanrouteCFDto.forwarderId;
                oNegotiationplanrouteCF.netPrice = getNegotiationplanrouteCFDto.netPrice;
                oNegotiationplanrouteCF.modiferUserId = getNegotiationplanrouteCFDto.userId;

                await _uow.SaveChangesAsync();

                return true;
            }
            catch(Exception ex)
            {

                return false;
            }
        }

        #region Private function
        private async Task<GetNegotiationplanrouteCFDto> fillDdl(GetNegotiationplanrouteCFDto oNegotiationplanrouteCFDto)
        {

            int locationId= await  _NegotiationplanrouteService.getFromLocationId(oNegotiationplanrouteCFDto.negotiationplanrouteId);
           // oNegotiationplanrouteCFDto.forwarders = await _ForwarderService.getForwardersDdlDto();
            oNegotiationplanrouteCFDto.agentCarriers = await _AgentCarrierService.getAgentCarriersDdlDto(locationId); 
            return oNegotiationplanrouteCFDto;

        }

        #endregion
    }
}
