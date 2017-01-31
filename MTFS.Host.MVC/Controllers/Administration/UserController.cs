using MTFS.Business.Dtos.DtoClasses;
using MTFS.Business.Services.Interfaces;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MTFS.Host.MVC.Controllers
{
    public class UserController : ApiController
    {
        private readonly  IUserService _UserService;

        public UserController(  IUserService userService)
       
        {
            _UserService = userService;
        }

        [HttpGet]
        public async Task<UsersManagementDto> getUsersManagement(int pageNo, string filter)
        {
            if (Setting.payloadDto.isItemAdmin == false)
                return null;

            int intCompanyId = Setting.payloadDto.companyId;


            return await _UserService.getUsersManagement( new GridInitialDto { recordCountPerPage= Setting.RECORD_COUNT_PAGE, companyId= intCompanyId, pageNo= pageNo, filter= filter });
              
        }

        [HttpGet]
        public async Task<UserDto> getUserInitial()
        {
            if (Setting.payloadDto.isItemAdmin == false)
                return null;
             
            var oUserDto =await  _UserService.getUserInitial(new BaseDto { userId= Setting.payloadDto.userId , companyId= Setting.payloadDto.companyId });

            return (oUserDto);

        }

        [HttpGet]
        public async Task<UserDto> getUser(int id)
        {
            if (Setting.payloadDto.isItemAdmin == false)
                return null;
             

            var oUserDto =await  _UserService.getUser(new BaseDto {id=id, companyId= Setting.payloadDto.companyId, userId= Setting.payloadDto.userId  });

            if (oUserDto != null)
                oUserDto.password = Utilities.Security.EncDec.Decrypt(oUserDto.password);


            return (oUserDto);

        }

        [HttpPost]
        public async Task<HttpResponseMessage> insertUser(UserDto userDto)
        {

            if (Setting.payloadDto.isItemAdmin == false)
                return null;

            HttpResponseMessage result = new HttpResponseMessage();
           // var oResult = new ResultDto();
            if (userDto.username.Trim() == ""
                || userDto.password.Trim() == ""
                || userDto.fullName.Trim() == "")
            {
                result.StatusCode = HttpStatusCode.NotFound;
                // oResult.resultCode = "404"; //Sent Data is Invalid
            }
            else
            {
                int intComapnyId = Setting.payloadDto.companyId;
                userDto.companyId = intComapnyId;
                userDto.password = Utilities.Security.EncDec.Encrypt(userDto.password);

                bool IsDataAdmin = Setting.payloadDto.isDataAdmin;

                if (IsDataAdmin == false)
                    userDto.isDataAdmin = false;

                userDto.userId  = Setting.payloadDto.userId;

                bool IsInserted =await  _UserService.insertUser( userDto);


                if (IsInserted)
                    result.StatusCode = HttpStatusCode.Created;
                // oResult.resultCode = "200"; 
                else
                    result.StatusCode = HttpStatusCode.BadRequest ;
                //oResult.resultCode = "400"; //Repetetive Username

            } 
            return result;

        }

        [HttpPut]
        public async Task<HttpResponseMessage> updateUser(UserDto userDto)
        {
            if (Setting.payloadDto.isItemAdmin == false)
                return null;

            HttpResponseMessage result = new HttpResponseMessage();
            //var oResultDto = new ResultDto();
            if (userDto.password.Trim() == "" || userDto.fullName.Trim() == "")
                result.StatusCode = HttpStatusCode.NotFound;
               // oResultDto.resultCode = "404"; //Sent Data is Invalid
            else
            {
                bool blnIsDataAdmin = Setting.payloadDto.isDataAdmin;

                if (blnIsDataAdmin == false)
                    userDto.isDataAdmin = false;

                userDto.password = Utilities.Security.EncDec.Encrypt(userDto.password);

                userDto .companyId = Setting.payloadDto.companyId;
                userDto.userId  = Setting.payloadDto.userId;

                bool blnUpdated =await  _UserService.updateUser(userDto);

                if (blnUpdated)
                    result.StatusCode = HttpStatusCode.OK;
                // oResultDto.resultCode = "200";
                else
                    result.StatusCode = HttpStatusCode.NotModified;
                  //  oResultDto.resultCode = "404"; //Sent Data is Invalid
            }
             
            return result;


        }
        

    }
}