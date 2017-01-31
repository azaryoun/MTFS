using System;


namespace MTFS.Web.Angular
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

        }



        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        //public class StructureMapControllerFactory : DefaultControllerFactory
        //{
        //    protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        //    {
        //        if (controllerType == null)
        //            throw new InvalidOperationException(string.Format("Page not found: {0}", requestContext.HttpContext.Request.RawUrl));
        //        return IOCConfig.Container.GetInstance(controllerType) as Controller;

        //        //if (controllerType == null)
        //        //{
        //        //    var url = requestContext.HttpContext.Request.RawUrl;
        //        //    //     string.Format("Page not found: {0}", url).LogException();

        //        //    requestContext.RouteData.Values["controller"] = "Account";
        //        //    requestContext.RouteData.Values["action"] = "Login";
        //        //    requestContext.RouteData.Values["keyword"] = url.Replace("-", " ");
        //        //    requestContext.RouteData.Values["categoryId"] = string.Format("Page not found: {0}", requestContext.HttpContext.Request.RawUrl);
        //        //    requestContext.RouteData.Values["msg"] = string.Format("Page not found: {0}", requestContext.HttpContext.Request.RawUrl);


        //        //    return  IOCConfig.Container.GetInstance(typeof(AccountController)) as Controller;
        //        //    //   throw new InvalidOperationException(string.Format("Page not found: {0}", requestContext.HttpContext.Request.RawUrl));
        //        //}




        //        //return  IOCConfig.Container.GetInstance(controllerType) as Controller;
        //    }

        //}

    }
}