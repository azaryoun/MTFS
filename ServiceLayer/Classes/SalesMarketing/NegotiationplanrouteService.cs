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
    public class NegotiationplanrouteService : INegotiationplanrouteService
    {
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Negotiationplanroute> _Negotiationplanroutes;
        private readonly ILocationService _LocationService;
        private readonly ITransporttypeService _TransporttypeService;

        public NegotiationplanrouteService(IUnitOfWork uow, 
                                           ILocationService locationService, 
                                           ITransporttypeService transporttypeService)
        {
            _uow = uow;
            _Negotiationplanroutes = _uow.Set<Negotiationplanroute>();
            _LocationService = locationService;
            _TransporttypeService = transporttypeService;
        }

        #region Retrive Data 
        public async Task<GetNegotiationplanroutesManagementDto> getNegotiationplanroutesManagement(GridChildInitialDto gridInitialDto)
        {
            int pageNo = gridInitialDto.pageNo;

            if (gridInitialDto.pageNo < 1) pageNo = 1;

            IQueryable<Negotiationplanroute> oNegotiationplanroutes = _Negotiationplanroutes.Include(i=>i.fromLocation)
                                                                        .Include(i=>i.toLocation) 
                                                                        .Include(i=>i.transporttype)
                                                                        .Where(i => i.negotiationplanId == gridInitialDto.parentId).AsQueryable();

            if (!string.IsNullOrEmpty(gridInitialDto.filter))
                oNegotiationplanroutes = oNegotiationplanroutes
                       .Where(w => w.fromLocation.locationName.Contains(gridInitialDto.filter) == true);

            int totalRecordCount = await oNegotiationplanroutes.AsNoTracking().CountAsync();

            GetNegotiationplanroutesManagementDto oGetNegotiationplanroutesManagementDto = new GetNegotiationplanroutesManagementDto();

            if (totalRecordCount != 0)
            {

                int totalPages = (int)Math.Ceiling((double)totalRecordCount / gridInitialDto.recordCountPerPage);

                if (pageNo > totalPages) pageNo = totalPages;


                oGetNegotiationplanroutesManagementDto.getNegotiationplanroutesDto = Mapper.Map<IEnumerable<Negotiationplanroute>, List<GetNegotiationplanroutesDto>>
                                                       (await oNegotiationplanroutes.GetPageRecords(pageNo, gridInitialDto.recordCountPerPage).ToListAsync());

                oGetNegotiationplanroutesManagementDto.currentPage = pageNo;
                oGetNegotiationplanroutesManagementDto.totalRecordCount = totalRecordCount;

            }
            oGetNegotiationplanroutesManagementDto.title = gridInitialDto.parentTitle;
            oGetNegotiationplanroutesManagementDto.parent_ParentId = gridInitialDto.parentId;

            return oGetNegotiationplanroutesManagementDto;
        }

        public async Task<GetNegotiationplanrouteDto> getNegotiationplanroute(BaseDto baseDto)
        {

            GetNegotiationplanrouteDto oNegotiationplanrouteDto= Mapper.Map<Negotiationplanroute, GetNegotiationplanrouteDto>(
                await _Negotiationplanroutes.AsNoTracking().SingleOrDefaultAsync(i => i.id == baseDto.id));

            return await fillDdl(oNegotiationplanrouteDto);
        }

        public async Task<GetNegotiationplanrouteDto> getNegotiationplanrouteInitial(BaseDto baseDto)
        {

            GetNegotiationplanrouteDto oNegotiationplanrouteDto = new GetNegotiationplanrouteDto();

            oNegotiationplanrouteDto.negotiationplanId = baseDto.id;

            return await  fillDdl(oNegotiationplanrouteDto); 

        }

        public async Task<int> getFromLocationId (int id)
        {
           return   (await _Negotiationplanroutes.Where(i => i.id == id).AsNoTracking().SingleAsync()).fromLocationId ;
        }

        #endregion

        public async Task<bool> insertNegotiationplanroute(GetNegotiationplanrouteDto getNegotiationplanrouteDto)
        {
            try
            {
                Negotiationplanroute oNegotiationplanroute = Mapper.Map<GetNegotiationplanrouteDto, Negotiationplanroute>(getNegotiationplanrouteDto);
                _Negotiationplanroutes.Add(oNegotiationplanroute);
                await _uow.SaveChangesAsync();

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> updateNegotiationplanroute(GetNegotiationplanrouteDto getNegotiationplanrouteDto)
        {
            try
            { 

                Negotiationplanroute oNegotiationplanroute = await _Negotiationplanroutes.SingleAsync(i => i.id == getNegotiationplanrouteDto.id);
                 
                oNegotiationplanroute.fromLocationId = getNegotiationplanrouteDto.fromLocationId;
                oNegotiationplanroute.toLocationId = getNegotiationplanrouteDto.toLocationId;
                oNegotiationplanroute.transporttypeId = getNegotiationplanrouteDto.transporttypeId;
                oNegotiationplanroute.modiferUserId = getNegotiationplanrouteDto.userId;

                await _uow.SaveChangesAsync();

                return true;
            }
            catch
            {

                return false;
            }
        }

        #region Private function
        private async  Task<GetNegotiationplanrouteDto> fillDdl(GetNegotiationplanrouteDto oNegotiationplanrouteDto)
        {
            oNegotiationplanrouteDto.fromLocations = await _LocationService.getLocationsDdlDto();
            oNegotiationplanrouteDto.toLocations = await _LocationService.getLocationsDdlDto();
            oNegotiationplanrouteDto.transporttypes = await _TransporttypeService.getTransporttypesDdlDto();
            return oNegotiationplanrouteDto;
        }

        #endregion
    }
}
