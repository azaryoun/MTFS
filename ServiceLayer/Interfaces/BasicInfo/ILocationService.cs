using MTFS.Business.Dtos.DtoClasses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MTFS.Business.Services.Interfaces
{
    public interface ILocationService
    {
        #region Retrive Data

        Task<GetLocationsManagementDto> getLocationsManagement(GridInitialDto gridInitialDto);
        Task<GetLocationDto> getLocation(BaseDto baseDto);
        Task<GetLocationDto> getLocationInitial();
        Task<List<DdlDto>> getLocationsDdlDto();

        #endregion

        Task<bool> insertLocation(GetLocationDto locationDto); 

        Task<bool> deleteLocation(BaseDto baseDto);

        Task<bool> updateLocation(GetLocationDto locationDto);
         
    }
}
