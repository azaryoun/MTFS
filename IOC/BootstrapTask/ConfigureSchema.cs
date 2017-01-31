using MTFS.DAL;

namespace MTFS.Business.Bootstrapper.BootstrapTask
{
    public static  class ConfigureSchema
    {
        public static void Execute()
        {
            new SchemaSynchronizer().Execute();
        }
    }
}
