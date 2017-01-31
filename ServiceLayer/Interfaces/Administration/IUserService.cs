using MTFS.Business.Dtos.DtoClasses;
using System.Threading.Tasks;

namespace MTFS.Business.Services.Interfaces
{
    public interface IUserService
    {
        Task<UsersManagementDto> getUsersManagement(GridInitialDto gridInitialDto);

        Task<UserDto> getUserInitial(BaseDto baseDto);

        Task<bool> insertUser(UserDto userDto);

        Task<UserDto> getUser(BaseDto baseDto);

        Task<bool> updateUser(UserDto userDto);
         
    }
}
