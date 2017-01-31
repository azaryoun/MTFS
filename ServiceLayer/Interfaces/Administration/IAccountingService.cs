using System.Collections.Generic;
using MTFS.Business.Dtos.DtoClasses;
using System.Threading.Tasks;

namespace MTFS.Business.Services.Interfaces
{
    public interface IAccountingService
    {
        Task<UserDto> getAndCheckLoginUser(string username, string password);

        Task<List<MenutitleDto>> getMenutitles(int userID, bool isItemAdmin);

        Task<bool> checkUserMenuitemAccess(BaseDto baseDto);

        Task<string> getMenuitemPageTitle(BaseDto baseDto);

        Task<bool> checkAndUpdateUserPassword(int userId, string cuurentPassword, string newPassword);

        Task<bool> updateComapnyLogo(ComapnyLogoDto companyLogoDto);

        Task<UserInfoDto> getUserInfo(BaseDto baseDto);
    }
}
