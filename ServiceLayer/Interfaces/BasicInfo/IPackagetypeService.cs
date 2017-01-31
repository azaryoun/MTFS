using MTFS.Business.Dtos.DtoClasses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MTFS.Business.Services.Interfaces
{
    public interface IPackagetypeService
    {

        #region Retrive Data 

        Task<GetPackagetypesManagementDto> getPackagetypesManagement(GridInitialDto gridInitialDto);
        Task<GetPackagetypeDto> getPackagetype(BaseDto baseDto); 

        #endregion

        Task<bool> insertPackagetype(GetPackagetypeDto PackagetypeDto);

        Task<bool> deletePackagetype(BaseDto baseDto);

        Task<bool> updatePackagetype(GetPackagetypeDto PackagetypeDto);

    }
}
