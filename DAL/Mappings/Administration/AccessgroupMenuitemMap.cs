using MTFS.Business.Domain.Model;
using System.Data.Entity.ModelConfiguration;

namespace MTFS.DAL.Mapping
{
    public class AccessgroupMenuitemMap : EntityTypeConfiguration<AccessgroupMenuitem>
    {
        public AccessgroupMenuitemMap()
        {
            ToTable("AccessgroupMenuitem", "Administration"); 

            HasRequired(e => e.accessgroup)
            .WithMany(e => e.accessgroupMenuitems)
            .HasForeignKey(e => e.accessgroupId);
       

            HasRequired(e => e.menuitem)
            .WithMany(e => e.accessgroupMenuitems)
            .HasForeignKey(e => e.menuitemId);





        }
    }
}
