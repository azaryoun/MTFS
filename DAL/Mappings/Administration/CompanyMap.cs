using MTFS.Business.Domain.Model;
using System.Data.Entity.ModelConfiguration;

namespace MTFS.DAL.Mappings 
{
    public class CompanyMap : EntityTypeConfiguration<Company>
    {
        public CompanyMap()
        {
            ToTable("Company", "Administration");
            Property(p => p.companyName).HasMaxLength(50);
            Property(p => p.address).HasMaxLength(100);
            Property(p => p.telephone).HasMaxLength(50);
            Property(p => p.nationalCode).HasMaxLength(50);
            Property(p => p.logoMime).HasMaxLength(50);
            Property(p => p.fax).HasMaxLength(50);
            Property(p => p.webDomain).HasMaxLength(100);
            Property(p => p.agentName).HasMaxLength(50);
            Property(p => p.email).HasMaxLength(50);
        }
    }
}
