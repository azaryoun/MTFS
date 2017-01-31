using System.Collections.Generic;

namespace MTFS.Business.Dtos.DtoClasses
{ 
    public class UserLoginDto
    {
        public string username { get; set; }
        public string password { get; set; }
    }

    public class UserChangePassDto
    {
        public string currentPassword { get; set; }
        public string newPassword { get; set; }
        public string confirmPassword { get; set; }
    }


    public class UserDto:BaseDto 
    { 
        public string username { get; set; } //
        public string password { get; set; }//
        public string fullName { get; set; }//
        public bool isItemAdmin { get; set; } //
        public string isItemAdminText { get; set; }//
        public string nationalCode { get; set; } //
        public string mobile { get; set; } //
        public string telephone { get; set; } //
        public string email { get; set; } //
        public string address { get; set; } // 
        public string companyName { get; set; } //
        public bool isActive { get; set; }//
        public string isActiveText { get; set; }//
        public bool isDataAdmin { get; set; }//
        public string isDataAdminText { get; set; }//
        public string companyLogo { get; set; }//
        public string companyLogoMime { get; set; }// 
        public List<AccessgroupDto> accessgroupsDto { get; set; } //


        public UserDto()
        {
            this.accessgroupsDto = new List<AccessgroupDto>();
        }

    }

    public class UserInfoDto
    {

        public int id { get; set; } //
        public string username { get; set; } //
        public string fullName { get; set; }//
        public string companyName { get; set; } //
        public string companyLogo { get; set; }//
        public string companyLogoMime { get; set; }// 
    
    }


    public class UsersManagementDto : ManagementBaseDto
    {
        public List<UserDto> usersDto { get; set; }

        public UsersManagementDto()
        {
            this.usersDto  = new List<UserDto>();
        }


    }

}