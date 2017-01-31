using MTFS.Business.Domain.Model;
using System.Data.Entity.ModelConfiguration; 

namespace MTFS.DAL.Mapping
{
    public class AccessgroupMap : EntityTypeConfiguration<Accessgroup>
    {
        public AccessgroupMap()
        {  
            ToTable("Accessgroup", "Administration"); 

            Property(p => p.groupName).HasMaxLength(50);
            HasRequired(e => e.ownerCompany)
                .WithMany(e => e.ownedAccessgroups)
                .HasForeignKey(e => e.ownerCompanyId)
                 .WillCascadeOnDelete(false)
                ;
             

        }
    }
}
