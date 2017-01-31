using MTFS.Business.Dtos.DtoClasses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MTFS.Business.Services.Interfaces
{
    public interface ICountryService
    {
         Task<List<DdlDto>> getCountriesDdlDto();
    }
}
