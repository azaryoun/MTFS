
using MTFS.Business.Domain.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;


namespace MTFS.DAL.Mapping
{
    public class LocationMap : EntityTypeConfiguration<Location>
    {
        public LocationMap()
        {
            ToTable("Location", "BasicInfo");

            Property(p => p.locationName).HasMaxLength(50).IsRequired();
            Property(p => p.locationCode).HasMaxLength(50);

            HasRequired(e => e.country)
                .WithMany(e => e.locations)
                .HasForeignKey(e => e.countryId)
                .WillCascadeOnDelete(false); 

        }
    }
}
