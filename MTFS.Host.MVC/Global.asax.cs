using MTFS.Business.Bootstrapper.BootstrapTask;
using MTFS.Business.Bootstrapper;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using Autofac.Integration.WebApi;
using System;

namespace MTFS.Host.MVC
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ///
            ConfigureAutoMapper.Execute();
            ConfigureSchema.Execute();

            //Set IOC config
            var container = IOCConfig.InitializeClient(webAssembly: typeof(MTFS.Host.MVC.WebApiApplication).Assembly);
            GlobalConfiguration.Configuration.DependencyResolver =
                 new AutofacWebApiDependencyResolver(container);

            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

        }
    }
}
