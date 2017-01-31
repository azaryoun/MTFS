using MTFS.DAL.Context;
using MTFS.DAL.Migrations;
using System.Data.Entity;

namespace MTFS.DAL
{
    public class SchemaSynchronizer
    {
        public void Execute()
        {
            var initializer = new MigrateDatabaseToLatestVersion<MFTSContext, Configuration>();
            Database.SetInitializer(initializer);
        }
    }
}
