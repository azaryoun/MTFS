
using MTFS.Business.Domain.Model;
using System.Data.Entity.ModelConfiguration;

namespace MTFS.DAL.Mapping
{
    public class LocationTransporttypeMap : EntityTypeConfiguration<LocationTransporttype>
    {
        public LocationTransporttypeMap()
        {
            ToTable("LocationTransporttype", "BasicInfo"); 

            HasRequired(e => e.location)
              .WithMany(e => e.locationTransporttypes)
              .HasForeignKey(e => e.locationId)
              .WillCascadeOnDelete(true)
              ;

            HasRequired(e => e.transporttype)
               .WithMany(e => e.locationTransporttypes)
               .HasForeignKey(e => e.transporttypeId)
               .WillCascadeOnDelete(false)
               ;


        }
    }
}
