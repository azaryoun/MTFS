using MTFS.Business.Dtos.DtoClasses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MTFS.Business.Services.Interfaces 
{
    public interface ICurrencyService
    {
        Task<List<DdlDto>> getCurrenciesDdlDto();
    }
}
