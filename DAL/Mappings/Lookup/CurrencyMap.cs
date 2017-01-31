using MTFS.Business.Domain.Model;
using System.Data.Entity.ModelConfiguration;

namespace MTFS.DAL.Mappings 
{
    public  class CurrencyMap : EntityTypeConfiguration<Currency>
    {
        public CurrencyMap()
        {
            ToTable("Currency", "Lookup"); 

            Property(p => p.name).HasMaxLength(50);
            Property(p => p.code).HasMaxLength(50);
            Property(p => p.symbol).HasMaxLength(10);
        }
    }
}
