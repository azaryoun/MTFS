using MTFS.Business.Domain.Model; 
using System.Data.Entity.ModelConfiguration;

namespace MTFS.DAL.Mapping
{
    public class MenutitleMap : EntityTypeConfiguration<Menutitle>
    {
        public MenutitleMap()
        {
            ToTable("Menutitle", "Administration"); 

            Property(p => p.titleText).HasMaxLength(50);
            Property(p => p.titleStyle).HasMaxLength(100);
            Property(p => p.href).HasMaxLength(200);
            Property(p => p.extraInfoStyle).HasMaxLength(100);
            Property(p => p.pageTitle).HasMaxLength(50);

            HasRequired(e => e.subsystem)
                .WithMany(e => e.menutitles)
                .HasForeignKey(e => e.subsystemId);
            
            

        }
    }
}
