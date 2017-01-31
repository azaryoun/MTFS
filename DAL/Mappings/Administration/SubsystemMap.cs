using MTFS.Business.Domain.Model;
using System.Data.Entity.ModelConfiguration;

namespace MTFS.DAL.Mappings
{
    public class SubsystemMap : EntityTypeConfiguration<Subsystem>
    {
        public SubsystemMap()
        {
            ToTable("Subsystem", "Administration");

            Property(p => p.subsyatemName).HasMaxLength(50);
            Property(p => p.subsyatemDescription).HasMaxLength(100); 

        }
    }
}
