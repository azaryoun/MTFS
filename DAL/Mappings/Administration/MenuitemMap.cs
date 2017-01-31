using MTFS.Business.Domain.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MTFS.DAL.Mapping
{
    public class MenuitemMap : EntityTypeConfiguration<Menuitem>
    {
        public MenuitemMap()
        {
            ToTable("Menuitem", "Administration"); 

            Property(p => p.itemText).HasMaxLength(50);
            Property(p => p.itemStyle).HasMaxLength(100);
            Property(p => p.href).HasMaxLength(200);
            Property(p => p.extraInfoStyle).HasMaxLength(100);
            Property(p => p.pageTitle).HasMaxLength(50);

            HasRequired(e => e.menutitle)
                .WithMany(e => e.menuitems)
                .HasForeignKey(e => e.menutitleId);


            Property(r => r.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
             
        }
    }
}
