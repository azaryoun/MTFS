using System;
using MTFS.DAL.Context;
using Autofac;
using System.Reflection;
using MTFS.Business.Services.Classes;
using Autofac.Integration.WebApi;

namespace MTFS.Business.Bootstrapper
{
    public class IOCConfig
    {
        private static  IContainer _container;

        public static  IContainer InitializeClient(Action<ContainerBuilder> additions = null, Assembly webAssembly = null)
        {
            var builder = new ContainerBuilder();

            if (webAssembly != null)
            {
                builder.RegisterAssemblyTypes(webAssembly).AsImplementedInterfaces();
                builder.RegisterApiControllers(webAssembly);
                //builder.RegisterModelBinders(webAssembly);
                //builder.RegisterModelBinderProvider();

            }

            builder.RegisterAssemblyTypes(typeof(AccountingService).Assembly).AsImplementedInterfaces()
                  .InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(MFTSContext).Assembly).AsImplementedInterfaces()
                .InstancePerRequest();

            //.InstancePerRequest();
            // .InstancePerApiControllerType(typeof(ApiController));
            //builder.Register(c => new MFTSContext()) .As<IContextIOC>();


            // builder.Register(c => new AccountingService(c.Resolve<IContextIOC>())).As<IAccountingService>();

            //    builder.RegisterAssemblyTypes(typeof(MFTSContext).Assembly).AsImplementedInterfaces();
            _container = builder.Build();

            return _container;
        }
    }
    //public static class IOCConfig
    //{
    //    private static readonly Lazy<Container> ContainerBuilder =
    //        new Lazy<Container>(DefaultContainer, LazyThreadSafetyMode.ExecutionAndPublication);

    //    public static IContainer Container
    //    {
    //        get { return ContainerBuilder.Value; }
    //    }

    //    private static Container DefaultContainer()
    //    {
    //        return new Container(x =>
    //        {
    //            x.For<IContextIOC>().HybridHttpOrThreadLocalScoped().Use(() => new MFTSContext());

    //            x.Scan(scan =>
    //            {
    //                scan.AssemblyContainingType<IAccountingService>();
    //                scan.TheCallingAssembly();
    //                scan.WithDefaultConventions();
    //            });
    //        });
    //    }
    //}
}
