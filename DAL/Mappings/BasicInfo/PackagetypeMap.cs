using MTFS.Business.Domain.Model;
using System.Data.Entity.ModelConfiguration;

namespace MTFS.DAL.Mappings
{
    public class PackagetypeMap : EntityTypeConfiguration<Packagetype>
    {
        public PackagetypeMap()
        {
            ToTable("Packagetype", "BasicInfo");

            Property(p => p.packageName ).HasMaxLength(50);
            Property(p => p.packageCode).HasMaxLength(50);
            Property(p => p.packageDesc).HasMaxLength(500);

        }
    }
}
