using MTFS.Business.Domain.Model;
using System.Data.Entity.ModelConfiguration;

namespace MTFS.DAL.Mapping
{
    public class CustomerMap : EntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        { 
            ToTable("Customer", "BasicInfo");
            Property(p => p.fullName).HasMaxLength(250).IsRequired();
            Property(p => p.IdentityNo).HasMaxLength(250).IsRequired();
            Property(p => p.telephone).HasMaxLength(50);
            Property(p => p.mobile).HasMaxLength(50);
            Property(p => p.fax).HasMaxLength(50);
            Property(p => p.email1).HasMaxLength(50).IsRequired();
            Property(p => p.email2).HasMaxLength(50);
            Property(p => p.email3).HasMaxLength(50);
            Property(p => p.address).HasMaxLength(500);
            Property(p => p.webSite).HasMaxLength(500);

         
        }
    }
}
