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
    public class LocationService : ILocationService
    {
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Location> _Locations;
        private readonly IDbSet<LocationTransporttype> _LocationTransporttypes;
        private readonly ICountryService _CountryService;
        private readonly ITransporttypeService _TransporttypeService;

        public  LocationService(IUnitOfWork uow,
                               ICountryService countryService,
                               ITransporttypeService transporttypeService)
        {

            _uow = uow;
            _Locations = _uow.Set<Location>();
            _LocationTransporttypes = _uow.Set<LocationTransporttype>();
            _CountryService = countryService;
            _TransporttypeService = transporttypeService;
        }

        #region Retrive Data 

        public async Task<GetLocationsManagementDto>  getLocationsManagement(GridInitialDto gridInitialDto)
        { 
            if (gridInitialDto.pageNo < 1)
                gridInitialDto.pageNo = 1;


            var oLocationsManagementDto = new GetLocationsManagementDto();


            IQueryable<Location> oLocations = _Locations.Include(r => r.country);

            if ( !string.IsNullOrEmpty(gridInitialDto.filter))
                oLocations = oLocations
                    .Where(w => w.locationName.Contains(gridInitialDto.filter) == true);

            int totalRecordCount =await  oLocations.AsNoTracking().CountAsync ();

            if (totalRecordCount != 0)
            {

                int totalPages = (int)Math.Ceiling((double)totalRecordCount / gridInitialDto.recordCountPerPage);
                if (gridInitialDto.pageNo > totalPages)
                    gridInitialDto.pageNo = totalPages;

                var intFrom = (gridInitialDto.pageNo - 1) * gridInitialDto.recordCountPerPage;
                oLocationsManagementDto.getLocationsDto = Mapper.Map<IEnumerable<Location>, List<GetLocationsDto>>(await  oLocations.GetPageRecords(gridInitialDto.pageNo, gridInitialDto.recordCountPerPage).ToListAsync());
  
                oLocationsManagementDto.currentPage = gridInitialDto.pageNo;
                oLocationsManagementDto.totalRecordCount = totalRecordCount;

            }

            return oLocationsManagementDto;
        }

        public async  Task<GetLocationDto> getLocationInitial()
        {

            GetLocationDto oLocationDto = new GetLocationDto();
             
            oLocationDto.countries =await  _CountryService.getCountriesDdlDto();
            oLocationDto.transporttypes =await  _TransporttypeService.getTransporttypesDto();

            return oLocationDto;

        }

        public async Task<GetLocationDto> getLocation(BaseDto baseDto)
        { 
            GetLocationDto oLocationDto = Mapper.Map<Location, GetLocationDto>(await _Locations.AsNoTracking().SingleOrDefaultAsync(i=>i.id==baseDto.id));
            oLocationDto.countries =await  _CountryService.getCountriesDdlDto();
            oLocationDto.transporttypes =await  _TransporttypeService.getTransporttypesDto(baseDto);

            return oLocationDto;
        }

        public async Task<List<DdlDto>> getLocationsDdlDto()
        {
            return (Mapper.Map<IEnumerable<Location>, List<DdlDto>>(await _Locations.Where(i=>i.isActive==true).OrderBy(o => o.locationName).AsNoTracking().ToListAsync()));
        }

        #endregion

        public async Task<bool> insertLocation(GetLocationDto getLocationDto)
        {
            try
            {

                Location oLocation =  Mapper.Map<GetLocationDto, Location>(getLocationDto);

                _Locations.Add(oLocation);

                foreach (var item in getLocationDto.transporttypes)
                {
                    if (!item.isChecked) continue;

                    _LocationTransporttypes.Add(new LocationTransporttype
                    {
                        locationId = getLocationDto.id,
                        transporttypeId = item.id,
                        createdUserId= getLocationDto.userId
                    });

                }

               await  _uow.SaveChangesAsync ();

                return true;
            }
            catch
            {
                return false;
            }
        } 
        public async Task<bool> updateLocation(GetLocationDto getLocationDto)
        {
            try
            {

                var oLocation =await  _Locations.SingleOrDefaultAsync(i=>i.id== getLocationDto.id);

                oLocation.locationName = getLocationDto.locationName;
                oLocation.locationCode = getLocationDto.locationCode;
                oLocation.countryId = getLocationDto.countryId;
                oLocation.isActive = getLocationDto.isActive;
                oLocation.modiferUserId  = getLocationDto.userId;

                foreach (var item in getLocationDto.transporttypes)
                {

                    var lnqLocationTransporttypes =await  _LocationTransporttypes
                                                            .SingleOrDefaultAsync(x =>
                                                             x.locationId == getLocationDto.id &&
                                                             x.transporttypeId == item.id);

                    if (item.isChecked && lnqLocationTransporttypes != null) continue;

                    if (!item.isChecked && lnqLocationTransporttypes == null) continue;


                    if (!item.isChecked && lnqLocationTransporttypes != null)

                        _LocationTransporttypes.Remove(lnqLocationTransporttypes);


                    if (item.isChecked && lnqLocationTransporttypes == null)

                        _LocationTransporttypes.Add(new LocationTransporttype
                        {
                            locationId = getLocationDto.id,
                            transporttypeId = item.id,
                            createdUserId= getLocationDto.userId 
                        });

                }
                await   _uow.SaveChangesAsync();

                return true;
            }
            catch
            {

                return false;
            }
        }

        public async Task<bool> deleteLocation(BaseDto baseDto)
        {
            try
            {

                var oLocation = await _Locations.SingleOrDefaultAsync(i => i.id == baseDto.id);

                if (oLocation == null)
                    return false;

                _Locations.Remove(oLocation);
                await _uow.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            } 

        }

    }

}
