using MTFS.Business.Dtos.DtoClasses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MTFS.Business.Services.Interfaces
{
    public interface IMenuitemService
    { 
        Task<List<MenuitemDto>> getMenuitemsDto(); 
        Task<List<MenuitemDto>> getMenuitemsDto(BaseDto baseDto); 
    }
}
