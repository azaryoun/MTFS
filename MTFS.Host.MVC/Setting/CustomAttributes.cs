using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Script.Serialization;
using MTFS.Utilities.Security;
using MTFS.Business.Dtos.DtoClasses;


namespace MTFS.Host.MVC
{
    public class JWTAuthenticationAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {


            HttpContext context = HttpContext.Current;

            var authorizationAttributes = actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>();

            if (authorizationAttributes.Count() > 0)
            {
                base.OnActionExecuting(actionContext);
                 return;
            }


            try
            {
                var strAuthorization = actionContext.Request.Headers.GetValues("Authorization").FirstOrDefault();
                var strJsonSecurityToken = EncDec.Decrypt(strAuthorization.Substring(7));

                PayloadDto oPayloadDto = null;
                oPayloadDto = new JavaScriptSerializer().Deserialize<PayloadDto>(strJsonSecurityToken);
                DateTime dteExpireDate = new DateTime(oPayloadDto.expireDate);
                if (dteExpireDate <= DateTime.Now)
                    throw new Exception();

                Setting.payloadDto= oPayloadDto;


            }

            catch (Exception ex)
            {
                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }

            

        }
    }
}