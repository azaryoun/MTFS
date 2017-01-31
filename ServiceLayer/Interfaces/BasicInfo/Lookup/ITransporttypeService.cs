using System.Collections.Generic;
using MTFS.Business.Dtos.DtoClasses;
using System.Threading.Tasks;

namespace MTFS.Business.Services.Interfaces
{
    public interface ITransporttypeService
    {
        Task<List<TransporttypeDto>> getTransporttypesDto();
        Task<List<TransporttypeDto>> getTransporttypesDto(BaseDto baseDto);
        Task<List<DdlDto>> getTransporttypesDdlDto();

    }
}
