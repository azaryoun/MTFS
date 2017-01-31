using System.Collections.Generic;
using System.Linq;
using MTFS.DAL.Context;
using System.Data.Entity;
using MTFS.Business.Domain.Model;
using MTFS.Business.Services.Interfaces;
using AutoMapper;
using MTFS.Business.Dtos.DtoClasses;
using System.Threading.Tasks;

namespace MTFS.Business.Services.Classes
{
    public class AccountingService : IAccountingService
    {
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<User> _Users;
        private readonly IDbSet<AccessgroupMenuitem> _AccessgroupMenuitems;
        private readonly IDbSet<Menutitle> _Menutitles;
        private readonly IDbSet<Menuitem> _Menuitems;
        private readonly IDbSet<Company> _Comapnies;
         
        public AccountingService(IUnitOfWork uow)
        { 
            _uow = uow;
            _Users = _uow.Set<User>();
            _AccessgroupMenuitems = _uow.Set<AccessgroupMenuitem>();
            _Menutitles = uow.Set<Menutitle>();
            _Menuitems = uow.Set<Menuitem>();
            _Comapnies = uow.Set<Company>();
        }


        public async Task<UserDto> getAndCheckLoginUser(string username, string password)
        {

            var oFoundUser =await  _Users
                .Where(current =>
                    current.username == username
                    && current.password == password
                    && current.isActive == true
                    && current.company.isActive == true
                    )
                    .Include(r => r.company)
                    .AsNoTracking()
                    .SingleOrDefaultAsync()  ;
             
                return  Mapper.Map<User, UserDto>(oFoundUser);
           

            //UserDto oUserDto = null;
            //if (oFoundUser != null)
            //{
            //    oUserDto = new  UserDto();
            //    oUserDto.id = oFoundUser.id;
            //    oUserDto.username = oFoundUser.username;
            //    oUserDto.fullName = oFoundUser.fullName;
            //    oUserDto.isItemAdmin = oFoundUser.isItemAdmin;
            //    oUserDto.companyName = oFoundUser.company.companyName;
            //    oUserDto.companyId = oFoundUser.company.id;
            //    oUserDto.isDataAdmin = oFoundUser.isDataAdmin;

            //    _ContextIOC.userId = oFoundUser.id;
            //    _ContextIOC.companyId = oFoundUser.companyId;


            //    if (oFoundUser.company.logo !=null)
            //    {
            //        oUserDto.companyLogo = Convert.ToBase64String(oFoundUser.company.logo);
            //        oUserDto.companyLogoMime = oFoundUser.company.logoMime;
            //    }
            //    else
            //        oUserDto.companyLogo = null;
            //} 
            

        }


        public async Task<bool> checkUserMenuitemAccess(BaseDto baseDto)
        {
            var blnHasAccess =await  _AccessgroupMenuitems.AsNoTracking()
                                   .AnyAsync(current =>
                                   current.menuitemId == baseDto.id  &&
                                   current.accessgroup.accessgroupUsers
                                   .Any(current2 =>
                                   current2.userId == baseDto.userId 
                                       )
                                   )
                                   ;

            return (blnHasAccess);

        }

        public async Task<List<MenutitleDto>> getMenutitles(int userID, bool isItemAdmin)
        {
            IQueryable<Menutitle> oMenutitles = _Menutitles.AsQueryable();

            if (!isItemAdmin)
            { 
                oMenutitles = oMenutitles.Where(
                    r => r.titleText == "Home" || r.menuitems.Any(
                    x => x.accessgroupMenuitems.Any(
                    y => y.accessgroup.accessgroupUsers.Any(
                    z => z.userId == userID
                    ))));
            }

            //  List<MenutitleDto> oMenutitlesDto = Mapper.Map<IEnumerable<Menutitle>, List<MenutitleDto>>(oMenutitles.AsNoTracking().OrderBy(r => r.displayOrder));
            var oMenutitlesDto = new List<MenutitleDto>();
            foreach (var oMenutitle in await  oMenutitles.AsNoTracking().OrderBy(r => r.displayOrder).ToListAsync())
            {
                var oMenutitleDto = new MenutitleDto();
                oMenutitleDto.displayOrder = oMenutitle.displayOrder.Value;
                oMenutitleDto.href = oMenutitle.href;
                oMenutitleDto.id = oMenutitle.id;
                oMenutitleDto.pageTitle = oMenutitle.pageTitle;
                oMenutitleDto.titleStyle = oMenutitle.titleStyle;
                oMenutitleDto.titleText = oMenutitle.titleText;

                IQueryable<Menuitem> oMenuitems = _Menuitems.Where(r => r.menutitleId == oMenutitle.id);

                if (!isItemAdmin)
                {
                    oMenuitems = _Menuitems.Where(r => r.menutitleId == oMenutitle.id)
                        .Where(r => r.accessgroupMenuitems.Any(
                               x => x.accessgroup.accessgroupUsers.Any(
                                y => y.userId == userID)));
                }
               
                oMenutitleDto.menuitemsDto = Mapper.Map<IEnumerable<Menuitem>, List<MenuitemMenuDto>>(await  oMenuitems.AsNoTracking().OrderBy(r =>r.displayOrder).ToListAsync());
                oMenutitlesDto.Add(oMenutitleDto);
            }
            return oMenutitlesDto;

            //if (isItemAdmin == true)
            //{
            //    var oMenutitles = _Menutitles
            //                    .OrderBy(r =>
            //                    r.displayOrder
            //                    );

            //    var oMenutitlesDto = new List<MenutitleDto>();

            //    foreach (var oMenutitle in oMenutitles)
            //    {
            //        var oMenutitleDto = new MenutitleDto();
            //        oMenutitleDto.displayOrder = oMenutitle.displayOrder.Value;
            //        oMenutitleDto.href = oMenutitle.href;
            //        oMenutitleDto.id = oMenutitle.id;
            //        oMenutitleDto.pageTitle = oMenutitle.pageTitle;
            //        oMenutitleDto.titleStyle = oMenutitle.titleStyle;
            //        oMenutitleDto.titleText = oMenutitle.titleText;
            //        var oMenuitems = _Menuitems
            //            .Where(r =>
            //            r.menutitleId == oMenutitle.id
            //            )
            //            .OrderBy(r => r.displayOrder)
            //            ;


            //        var oMenuitemsDto = new List<MenuitemDto>();
            //        foreach (var oMeunitem in oMenuitems)
            //        {
            //            var oMenuitemDto = new MenuitemDto();
            //            oMenuitemDto.displayOrder = oMeunitem.displayOrder.Value;
            //            oMenuitemDto.href = oMeunitem.href;
            //            oMenuitemDto.id = oMeunitem.id;
            //            oMenuitemDto.itemStyle = oMeunitem.itemStyle;
            //            oMenuitemDto.itemText = oMeunitem.itemText;
            //            oMenuitemDto.meuntitleId = oMeunitem.menutitleId;
            //            oMenuitemDto.pageTitle = oMeunitem.pageTitle;
            //            oMenuitemsDto.Add(oMenuitemDto);
            //        }

            //        oMenutitleDto.menuitemsDto = oMenuitemsDto;
            //        oMenutitlesDto.Add(oMenutitleDto);

            //    }

            //    return (oMenutitlesDto);

            //}
            //else

            //{
            //    var oMenutitles = _Menutitles.AsQueryable();

            //    oMenutitles = oMenutitles.Where(
            //        r => r.titleText == "Home" || r.menuitems.Any(
            //            x => x.accessgroupMenuitems.Any(
            //                y => y.accessgroup.accessgroupUsers.Any(
            //                    z => z.userId == userID
            //                    ))));


            //    oMenutitles = oMenutitles
            //                           .OrderBy(r => r.displayOrder)
            //                           ;

            //    var oMenutitlesDto = new List<MenutitleDto>();

            //    foreach (var oMenutitle in oMenutitles)
            //    {
            //        var oMenutitleDto = new MenutitleDto();
            //        oMenutitleDto.displayOrder = oMenutitle.displayOrder.Value;
            //        oMenutitleDto.href = oMenutitle.href;
            //        oMenutitleDto.id = oMenutitle.id;
            //        oMenutitleDto.pageTitle = oMenutitle.pageTitle;
            //        oMenutitleDto.titleStyle = oMenutitle.titleStyle;
            //        oMenutitleDto.titleText = oMenutitle.titleText;

            //        var oMenuitems = _Menuitems.AsQueryable();

            //        oMenuitems = oMenuitems
            //            .Where(r =>
            //            r.menutitleId == oMenutitle.id
            //            );

            //        oMenuitems = oMenuitems.Where(
            //            r => r.accessgroupMenuitems.Any(
            //                x => x.accessgroup.accessgroupUsers.Any(
            //                    y => y.userId == userID
            //                    )));


            //        oMenuitems = oMenuitems
            //            .OrderBy(
            //            r => r.displayOrder
            //            );

            //        var oMenuitemsDto = new List<MenuitemDto>();
            //        foreach (var oMeunitem in oMenuitems)
            //        {
            //            var oMenuitemDto = new MenuitemDto();
            //            oMenuitemDto.displayOrder = oMeunitem.displayOrder.Value;
            //            oMenuitemDto.href = oMeunitem.href;
            //            oMenuitemDto.id = oMeunitem.id;
            //            oMenuitemDto.itemStyle = oMeunitem.itemStyle;
            //            oMenuitemDto.itemText = oMeunitem.itemText;
            //            oMenuitemDto.meuntitleId = oMeunitem.menutitleId;
            //            oMenuitemDto.pageTitle = oMeunitem.pageTitle;
            //            oMenuitemsDto.Add(oMenuitemDto);
            //        }

            //        oMenutitleDto.menuitemsDto = oMenuitemsDto;
            //        oMenutitlesDto.Add(oMenutitleDto);

            //    }

            //    return (oMenutitlesDto);

        }
         

        public async Task<string> getMenuitemPageTitle(BaseDto baseDto)
        {

            string  strPageTitle =(await  _Menuitems
                    .AsNoTracking()
                    .SingleOrDefaultAsync
                    (current => current.id == baseDto.id))
                    .pageTitle ;

            return (strPageTitle);
        }

        public async  Task<bool> checkAndUpdateUserPassword(int userId, string cuurentPassword, string newPassword)
        { 
            try
            {
                User foundUser = await _Users.SingleOrDefaultAsync(current =>
                       current.id == userId && current.password == cuurentPassword);

                if (foundUser == null)
                    return false;
                foundUser.password = newPassword;
                await _uow.SaveChangesAsync();

                return true;
            }
            catch 
            { 
                return false;
            } 
        }

        public async Task<bool> updateComapnyLogo(ComapnyLogoDto companyLogoDto)
        {
            try
            {
                var oCompany =await  _Comapnies.SingleOrDefaultAsync(r => r.id == companyLogoDto.id);

                oCompany.logo = companyLogoDto.logo;
                oCompany.logoMime = companyLogoDto.mime;
                await  _uow.SaveChangesAsync ();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<UserInfoDto> getUserInfo(BaseDto baseDto)
        {
            try
            {
                User oUser = await _Users.Include(r => r.company).AsNoTracking().SingleOrDefaultAsync(r => r.id == baseDto.userId);
                return  Mapper.Map<User, UserInfoDto>(oUser) ;

            }
            catch
            {
                return (null);
            }
        }
    }
}
