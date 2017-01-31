using MTFS.Business.Domain.Model;
using System.Data.Entity.ModelConfiguration; 

namespace MTFS.DAL.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            ToTable("User", "Administration"); 

            Property(p => p.username).HasMaxLength(50);
            Property(p => p.password).HasMaxLength(50);
            Property(p => p.fullName).HasMaxLength(50);
            Property(p => p.nationalCode).HasMaxLength(50);
            Property(p => p.mobile).HasMaxLength(50);
            Property(p => p.telephone).HasMaxLength(50);
            Property(p => p.email).HasMaxLength(50);
            Property(p => p.address).HasMaxLength(99); 

            HasRequired(e => e.company)
                .WithMany(e => e.users)
                .HasForeignKey(e => e.companyId);

        }
    }
}
