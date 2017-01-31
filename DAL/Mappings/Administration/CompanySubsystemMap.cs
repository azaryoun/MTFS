using MTFS.Business.Domain.Model;
using System.Data.Entity.ModelConfiguration; 

namespace MTFS.DAL.Mapping
{
    public class CompanySubsystemMap : EntityTypeConfiguration<CompanySubsystem>
    {
        public CompanySubsystemMap()
        {
            ToTable("CompanySubsystem", "Administration"); 

            HasRequired(e => e.subsystem)
                .WithMany(e => e.companySubsystems)
                .HasForeignKey(e => e.subsystemId);


            HasRequired(e => e.company)
                .WithMany(e => e.companySubsystems)
                .HasForeignKey(e => e.companyId);
            

        }
    }
}
