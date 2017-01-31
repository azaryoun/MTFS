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

namespace MTFS.Business.Services.Classes
{
    public class AgentCarrierService : IAgentCarrierService
    {
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<AgentCarrier> _AgentCarriers; 
        private readonly ILocationService _LocationService; 

        public AgentCarrierService( IUnitOfWork uow,   
                                    ILocationService locationService )
        {
            _uow = uow;
            _AgentCarriers = _uow.Set<AgentCarrier>(); 
            _LocationService =locationService; 
        }

        #region Retrive Data 

        public async Task<GetAgentCarriersManagementDto> getAgentCarriersManagement(GridChildInitialDto gridInitialDto)
        { 
            int pageNo = gridInitialDto.pageNo;

            if (gridInitialDto.pageNo < 1) pageNo = 1;

            IQueryable<AgentCarrier> oAgentCarriers = _AgentCarriers.Include(i=>i.carrier)
                                                                    .Include(i=>i.location)
                                                                    .Where(i=>i.agentId==gridInitialDto.parentId)
                                                                    .AsQueryable();

            if (!string.IsNullOrEmpty(gridInitialDto.filter))
                oAgentCarriers = oAgentCarriers
                       .Where(w => w.carrier.fullName.Contains(gridInitialDto.filter) == true);

            int totalRecordCount = await oAgentCarriers.AsNoTracking().CountAsync();

            GetAgentCarriersManagementDto  oAgentCarriersManagDto = new GetAgentCarriersManagementDto();

            if (totalRecordCount != 0)
            {

                int totalPages = (int)Math.Ceiling((double)totalRecordCount / gridInitialDto.recordCountPerPage);

                if (pageNo > totalPages) pageNo = totalPages;


                oAgentCarriersManagDto.getAgentCarriersDto = Mapper.Map<IEnumerable<AgentCarrier>, List<GetAgentCarriersDto >>
                                                       (await oAgentCarriers.GetPageRecords(pageNo, gridInitialDto.recordCountPerPage).ToListAsync());

                oAgentCarriersManagDto.currentPage = pageNo;
                oAgentCarriersManagDto.totalRecordCount = totalRecordCount; 
            }
            oAgentCarriersManagDto.title = gridInitialDto.parentTitle;
            return oAgentCarriersManagDto;
        }

        public async Task<GetAgentCarrierDto> getAgentCarrierInitial(BaseDto baseDto)
        {

            GetAgentCarrierDto oAgentCarrierDto = new GetAgentCarrierDto();
             
            //oAgentCarrierDto.carriers = await _CarrierService.getCarriersDdlDto();
            oAgentCarrierDto.locations = await _LocationService.getLocationsDdlDto();
            oAgentCarrierDto.agentId = baseDto.id;
            
            return oAgentCarrierDto;

        }

        public async Task<GetAgentCarrierDto> getAgentCarrier(BaseDto baseDto)
        {
            GetAgentCarrierDto oAgentCarrierDto = Mapper.Map<AgentCarrier, GetAgentCarrierDto>(await _AgentCarriers.AsNoTracking().SingleOrDefaultAsync(i => i.id == baseDto.id));
           // oAgentCarrierDto.carriers = await _CarrierService.getCarriersDdlDto();
            oAgentCarrierDto.locations = await _LocationService.getLocationsDdlDto();

            return oAgentCarrierDto;
        }

        public async Task<List<DdlWithParentDto>> getAgentCarriersDdlDto(int locationId)
        {
            return (Mapper.Map<IEnumerable<AgentCarrier>, List<DdlWithParentDto>>(await _AgentCarriers.Include(i=>i.carrier).Include(i=>i.agent)
                           .Where(i => i.locationId== locationId && i.isActive==true )
                           .AsNoTracking().ToListAsync()));
        }

        #endregion

        public async Task<bool> insertAgentCarrier(GetAgentCarrierDto getAgentCarrierDto)
        {
            try
            {
                if (getAgentCarrierDto.isActive)
                {
                    if (await _AgentCarriers.AsNoTracking().AnyAsync(i =>i.agentId==getAgentCarrierDto.agentId  && i.isActive==true ))
                        return true;
                }

                AgentCarrier oAgentCarrier = Mapper.Map<GetAgentCarrierDto, AgentCarrier>(getAgentCarrierDto);

                _AgentCarriers.Add(oAgentCarrier);

                await _uow.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }
          
        public async Task<bool> updateAgentCarrier(GetAgentCarrierDto getAgentCarrierDto)
        {
            try
            {
                AgentCarrier oAgentCarrier = await _AgentCarriers.SingleOrDefaultAsync(i => i.id == getAgentCarrierDto.id);
                 
                 if (getAgentCarrierDto.isActive  && !oAgentCarrier.isActive )
                {   
                    if (await _AgentCarriers.AsNoTracking().AnyAsync(i => i.agentId == getAgentCarrierDto.agentId && 
                                                                          i.isActive== true))
                    { 
                        return true;
                    }
                        
                } 
                 
                oAgentCarrier.carrierId  = getAgentCarrierDto.carrierId;
                oAgentCarrier.locationId = getAgentCarrierDto.locationId;
                oAgentCarrier.isActive  = getAgentCarrierDto.isActive; 
                oAgentCarrier.modiferUserId = getAgentCarrierDto.userId;
                 
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
