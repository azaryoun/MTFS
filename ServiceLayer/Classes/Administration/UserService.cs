using System;
using System.Collections.Generic;
using System.Linq;
using MTFS.DAL.Context;
using System.Data.Entity;
using MTFS.Business.Domain.Model;
using MTFS.Business.Services.Interfaces;
using MTFS.Business.Services.ExtentionMethods;
using MTFS.Business.Dtos.DtoClasses;
using AutoMapper;
using System.Threading.Tasks;

namespace MTFS.Business.Services.Classes
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<User> _Users;
        private readonly IDbSet<Accessgroup> _Accessgroups;
        private readonly IDbSet<AccessgroupUser> _AccessgroupUsers;

        public UserService(IUnitOfWork uow)
        {
            _uow = uow;
            _Users = uow.Set<User>();
            _Accessgroups = uow.Set<Accessgroup>();
            _AccessgroupUsers = uow.Set<AccessgroupUser>();
        }

        public async Task<UsersManagementDto> getUsersManagement(GridInitialDto gridInitialDto)
        {
            if (gridInitialDto.pageNo < 1)
                gridInitialDto.pageNo = 1;

            IQueryable<User> oUsers = _Users.AsQueryable().Where(x => x.companyId == gridInitialDto.companyId);

            if (!string.IsNullOrEmpty(gridInitialDto.filter))
                oUsers = oUsers.Where(x => x.username.Contains(gridInitialDto.filter) == true &&
                         x.fullName.Contains(gridInitialDto.filter) == true
                    // &&  x.isItemAdmin.getDataAdminText().Contains(filter) == true &&
                    //&& x.isReal.getRealLegalText().Contains(filter) == true 
                    // x.isActive.getIsActiveText().Contains(filter) == true
                    );


            int intTotalRecordCount =await  oUsers.AsNoTracking().CountAsync();

            var oUsersManagementDto = new UsersManagementDto();
            if (intTotalRecordCount != 0)
            {

                int intTotalPages = (int)Math.Ceiling((double)intTotalRecordCount / gridInitialDto.recordCountPerPage);
                if (gridInitialDto.pageNo > intTotalPages)
                    gridInitialDto.pageNo = intTotalPages;
                 

                foreach (var oUser in await  oUsers.GetPageRecords(gridInitialDto.pageNo, gridInitialDto.recordCountPerPage).ToListAsync())
                {

                    var oUserDto = new UserDto
                    {
                        id = oUser.id,
                        fullName = oUser.fullName,
                        username = oUser.username,
                        isItemAdminText = oUser.isItemAdmin == true ? "Yes" : "No",
                        isDataAdminText = oUser.isDataAdmin == true ? "Yes" : "No",
                        isActiveText = oUser.isActive == true ? "Yes" : "No",
                    };


                    oUsersManagementDto.usersDto.Add(oUserDto);
                }

                oUsersManagementDto.currentPage = gridInitialDto.pageNo;
                oUsersManagementDto.totalRecordCount = intTotalRecordCount;

            }
            return (oUsersManagementDto);
        }

        public async Task<UserDto> getUserInitial(BaseDto BaseDto)
        {

            var oUserDto = new UserDto();

            oUserDto.isItemAdmin = false;
            oUserDto.isDataAdmin = false;
            oUserDto.isActive = true;


            var oAccessgroups =await  _Accessgroups 
                .Where(x => x.ownerCompanyId == BaseDto.companyId)
                .OrderBy(x => x.groupName).ToListAsync();

            foreach (var oAccessgroup in oAccessgroups)
            {
                var oAccessgroupDto = new AccessgroupDto
                {
                    groupName = oAccessgroup.groupName,
                    id = oAccessgroup.id,
                    isChecked = false,
                    menuItemsDto = null,
                };

                oUserDto.accessgroupsDto.Add(oAccessgroupDto);
            }

            return (oUserDto);

        }

        public async Task<bool> insertUser( UserDto userDto)
        {
            try
            {
                var blnIsExistsUsername =await  _Users .AnyAsync ( r => r.username == userDto.username);

                if (blnIsExistsUsername == true)
                    return (false); //Repetetive Username



                var oUser = new User
                {
                    address = userDto.address,
                    email = userDto.email,
                    fullName = userDto.fullName,
                    isItemAdmin = userDto.isItemAdmin,
                    mobile = userDto.mobile,
                    nationalCode = userDto.nationalCode,
                    password = userDto.password,
                    telephone = userDto.telephone,
                    username = userDto.username,
                    isActive = userDto.isActive,
                    companyId = userDto.companyId,
                    isDataAdmin = userDto.isDataAdmin,
                    createdUserId  = userDto.userId ,
                };

                foreach (var oAccessgroupDto in userDto.accessgroupsDto)
                    if (oAccessgroupDto.isChecked == true)
                    {
                        var oAccessgroupUser = new AccessgroupUser();
                        oAccessgroupUser.accessgroupId = oAccessgroupDto.id;
                        oUser.accessgroupUsers.Add(oAccessgroupUser);
                    }


                _Users.Add(oUser);
                await  _uow.SaveChangesAsync ();
                  
                return (true);
            }
            catch
            {
                return (false);
            }

        }

        public async Task<UserDto> getUser(BaseDto baseDto)
        {

            UserDto oUserDto = null; 

            var oUser =await  _Users
                                .AsNoTracking()
                                .SingleOrDefaultAsync(x =>
                                x.companyId == baseDto.companyId &&
                                x.id == baseDto.id );

            if (oUser != null)
                oUserDto = new UserDto
                {
                    id = oUser.id,
                    address = oUser.address,
                    email = oUser.email,
                    fullName = oUser.fullName,
                    isItemAdmin = oUser.isItemAdmin,
                    isDataAdmin = oUser.isDataAdmin,
                    mobile = oUser.mobile,
                    nationalCode = oUser.nationalCode,
                    password = oUser.password,
                    telephone = oUser.telephone,
                    username = oUser.username,
                    accessgroupsDto =await  getAccessgroups(new BaseDto { id= baseDto.id , companyId= baseDto.companyId , userId=baseDto.userId }),
                    isActive = oUser.isActive,
                    companyId = oUser.companyId,
                };

            return (oUserDto);

        }

        private async Task<List<AccessgroupDto>> getAccessgroups(BaseDto baseDto)
        {

            var oAccessgroups =await  _Accessgroups
                                        .AsNoTracking()
                                        .Where(x => x.ownerCompanyId == baseDto.companyId)
                                        .OrderBy(x => x.groupName)
                                        .ToListAsync() ;
            
            var oAccessgroupsDto = new List<AccessgroupDto>();


            foreach (var oAccessgroup in oAccessgroups)
            {
                var oAccessgroupDto = new AccessgroupDto();
                oAccessgroupDto.id = oAccessgroup.id;
                oAccessgroupDto.groupName = oAccessgroup.groupName;


                var oAccessgroupUser =await  _AccessgroupUsers
                                            .SingleOrDefaultAsync(x =>
                                            x.userId == baseDto.userId
                                            && x.accessgroupId == oAccessgroup.id );

                if (oAccessgroupUser != null)
                    oAccessgroupDto.isChecked = true;



                oAccessgroupsDto.Add(oAccessgroupDto);
            }

            return (oAccessgroupsDto);
        }

        public async Task<bool> updateUser(UserDto userDto)
        {
            try
            {

                var oUser =await  _Users
                                    .SingleAsync (x =>
                                    x.companyId == userDto.companyId  &&
                                    x.id == userDto.id );


                oUser.address = userDto.address;
                oUser.email = userDto.email;
                oUser.fullName = userDto.fullName;
                oUser.isItemAdmin = userDto.isItemAdmin;
                oUser.isDataAdmin = userDto.isDataAdmin;
                oUser.mobile = userDto.mobile;
                oUser.modiferUserId = userDto.userId ;
                oUser.nationalCode = userDto.nationalCode;
                oUser.password = userDto.password;
                oUser.telephone = userDto.telephone;
                oUser.isActive = userDto.isActive;
                oUser.modiferUserId = userDto.userId;

                foreach (var oAccessgroupDto in userDto.accessgroupsDto)
                {
                    var oFoundAccessgroupUser =await  _AccessgroupUsers
                                                       .SingleOrDefaultAsync(x =>
                                                       x.userId == userDto.id &&
                                                       x.accessgroupId == oAccessgroupDto.id );


                    if (oFoundAccessgroupUser == null)
                    {
                        if (oAccessgroupDto.isChecked == true)
                        {
                            var oNewAccessgroupUser = new AccessgroupUser();
                            oNewAccessgroupUser.userId = userDto.id;
                            oNewAccessgroupUser.accessgroupId = oAccessgroupDto.id;
                            _AccessgroupUsers.Add(oNewAccessgroupUser);
                        }
                    }
                    else
                    {
                        if (oAccessgroupDto.isChecked == false)
                            _AccessgroupUsers.Remove(oFoundAccessgroupUser);
                    }

                }

               await  _uow.SaveChangesAsync ();

                return (true);
            }
            catch
            {

                return (false);
            }

        }

  
    }
}
