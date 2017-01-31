using MTFS.Business.Domain.Model;
using System.Data.Entity.ModelConfiguration;

namespace MTFS.DAL.Mappings 
{
    public class CountryMap : EntityTypeConfiguration<Country>
    {
        public CountryMap()
        {
            ToTable("Country", "Lookup"); 

            Property(p => p.countryName).HasMaxLength(50).IsRequired();
        }
    }
}
