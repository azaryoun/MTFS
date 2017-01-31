using MTFS.Business.Dtos.DtoClasses;
using MTFS.Business.Services.Interfaces;
using MTFS.Utilities.Security;
using System.Web.Http;
using System.Web.Script.Serialization;
using System;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;

namespace MTFS.Host.MVC.Controllers
{
    public class AccountController : ApiController
    {

        private readonly IAccountingService _AccountingService;

        public AccountController(IAccountingService accountingService)

        {
            _AccountingService = accountingService;
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<JasonWebTokenDto> Login(UserLoginDto userLoginDto)
        { 
            string strPassword = EncDec.Encrypt(userLoginDto.password);
            string  strUsername = userLoginDto.username;

            var oUserDto =await  _AccountingService.getAndCheckLoginUser(strUsername, strPassword);

            if (oUserDto == null)
                return (null);
            //  oResult.resultCode = "404";
            //  oResult.userInfo = null;
            //  oResult.menutitlesDto = null;
            //  oResult.resultCode = "200";
            // oResult.userInfo = oUserDto;
            //  Session["UserInfo"] = oUserDto;

            var oPayloadDto = new PayloadDto()
            {
                expireDate = DateTime.Now.AddMinutes(Setting.JWT_TIMEOUT_MINUTE).Ticks,
                companyId = oUserDto.companyId,
                isDataAdmin = oUserDto.isDataAdmin,
                isItemAdmin = oUserDto.isItemAdmin,
                userId = oUserDto.id,
            };

            var strJsonSecurityToken = new JavaScriptSerializer().Serialize(oPayloadDto);
            var strEncryptedJson = EncDec.Encrypt(strJsonSecurityToken);
            return (new JasonWebTokenDto() { JWT = strEncryptedJson, });

            //var oMenutitlesDto = _AccountingService.getMenutitles(oUserDto.id, oUserDto.isItemAdmin);
            // Session["Menutitles"] = oMenutitlesDto;

            //         oResult.resultCode = "200";


        }

        //[HttpPost]
        //public ResultDto GetMenutitles()
        //{
        //    int intUserId = Setting.payloadDto.userId;
        //    bool bolisItemAdmin = Setting.payloadDto.isItemAdmin;


        //    var oMenutitlesDto = _AccountingService.getMenutitles(intUserId, bolisItemAdmin);


        //    var oResult = new ResultDto();

        //    //if (IsLoggin() == false)
        //    //{
        //    //    oResult.resultCode = "404";
        //    //    oResult.userInfo = null;
        //    //    oResult.menutitlesDto = null;
        //    //}
        //    //else
        //    //{
        //    //   oResult.resultCode = "200";
        //    //     oResult.userInfo = (UserDto)Session["UserInfo"];
        //    //    oResult.menutitlesDto = (List<MenutitleDto>)Session["Menutitles"];

        //    //}

        //    return (oResult);
        //}


        [HttpGet]
        public async Task<ResultDto> getUserInfo()
        {

            var oResult = new ResultDto();

            try
            {
                var intUserId = Setting.payloadDto.userId;
                var blnIsItemAdmin = Setting.payloadDto.isItemAdmin;

                oResult.resultCode = "200";
                oResult.userInfoDto =await  _AccountingService.getUserInfo(new BaseDto { userId= intUserId });
                oResult.menutitlesDto =await  _AccountingService.getMenutitles(intUserId, blnIsItemAdmin);
            }
            catch
            {
                oResult.resultCode = "404";
                oResult.userInfoDto = null;
                oResult.menutitlesDto = null;
            }

            return (oResult);
        }

        [HttpPost]
        public async Task<ResultDto> CheckPageAccess(int menuitemId)
        {

            int intUserId = Setting.payloadDto.userId;
            bool bolisItemAdmin = Setting.payloadDto.isItemAdmin;


            var oResult = new ResultDto();
            oResult.menutitlesDto = null;
            oResult.userInfoDto = null;

            //if (IsLoggin() == false)
            //    oResult.resultCode = "404";
            //else
            //{
            if (bolisItemAdmin == true)
                oResult.resultCode = "200";
            else
            { 
                bool blnHasAccess =await  _AccountingService.checkUserMenuitemAccess(new BaseDto { userId = intUserId, id = menuitemId });
                 
                if (blnHasAccess == true)
                    oResult.resultCode = "200";
                else
                    oResult.resultCode = "404"; 
            }
            if (oResult.resultCode == "200")
            {
                string  strPageTitle =await  _AccountingService.getMenuitemPageTitle(new BaseDto { id = menuitemId });
                oResult.pageTitle = strPageTitle;
            }

            return (oResult);
        }

        [HttpPut]
        public async Task<HttpResponseMessage> ChangePassword(UserChangePassDto userChangePassDto)
        {

            //var oResultDto = new ResultDto();
            var intUserID = Setting.payloadDto.userId;
            var strPassword = EncDec.Encrypt(userChangePassDto.currentPassword);
            var strNewPassword = EncDec.Encrypt(userChangePassDto.newPassword);

            bool blnChangedPassword =await  _AccountingService.checkAndUpdateUserPassword(intUserID, strPassword, strNewPassword);

            if (blnChangedPassword)
                return new HttpResponseMessage(HttpStatusCode.OK);
            //oResultDto.resultCode = "200"; 
            else
                return new HttpResponseMessage(HttpStatusCode.NotModified);
            //oResultDto.resultCode = "404";

        }


        [HttpPost]
        public async Task<HttpResponseMessage> UpdateCompanyLogo(ComapnyLogoDto companyLogoDto)
        {

           // var oResultDto = new ResultDto();
            companyLogoDto.id = Setting.payloadDto.companyId;
            bool blnUpdateLogo =await  _AccountingService.updateComapnyLogo(companyLogoDto);

            if (blnUpdateLogo)
                return new HttpResponseMessage(HttpStatusCode.OK);
            // oResultDto.resultCode = "200"; 
            else
                return new HttpResponseMessage(HttpStatusCode.NotModified);
            // oResultDto.resultCode = "404";
             
        }



        #region "Non Actions"


        //[NonAction]
        //private bool IsLoggin()
        //{

        //    bool bolIsLogin = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;

        //    if (!bolIsLogin || Session["UserInfo"] == null || Session["UserInfo"].ToString() == string.Empty)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;

        //    }

        //}

        #endregion


    }
}