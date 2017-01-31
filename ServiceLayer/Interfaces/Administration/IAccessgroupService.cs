using MTFS.Business.Dtos.DtoClasses;
using System.Threading.Tasks;

namespace MTFS.Business.Services.Interfaces
{
    public interface IAccessgroupService
    { 
        Task<AccessgroupsManagementDto> getAccessgroupsManagement(GridInitialDto gridInitialDto);

        Task<bool> insertAccessgroup(AccessgroupDto accessgroupDto);

        Task<AccessgroupDto> getAccessgroup(BaseDto baseDto);

        Task<bool> deleteAccessgroup(BaseDto baseDto);

        Task<bool> updateAccessgroup(AccessgroupDto accessgroupDto);


    }
}
