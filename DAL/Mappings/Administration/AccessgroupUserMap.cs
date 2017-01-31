using MTFS.Business.Domain.Model;
using System.Data.Entity.ModelConfiguration; 

namespace MTFS.DAL.Mapping
{
    public class AccessgroupUserMap : EntityTypeConfiguration<AccessgroupUser>
    {
        public AccessgroupUserMap()
        { 

            ToTable("AccessgroupUser", "Administration"); 

            HasRequired(e => e.user)
                .WithMany(e => e.accessgroupUsers)
                .HasForeignKey(e => e.userId)
                 .WillCascadeOnDelete(false)
                ;


            HasRequired(e => e.accessgroup)
                .WithMany(e => e.accessgroupUsers)
                .HasForeignKey(e => e.accessgroupId)
                 .WillCascadeOnDelete(false)
                ;



        }
    }
}
