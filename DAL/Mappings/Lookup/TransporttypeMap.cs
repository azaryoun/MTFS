using MTFS.Business.Domain.Model;
using System.Data.Entity.ModelConfiguration;

namespace MTFS.DAL.Mappings 
{
    public class TransporttypeMap : EntityTypeConfiguration<Transporttype>
    {
        public TransporttypeMap()
        {
            ToTable("Transporttype", "Lookup"); 

            Property(p => p.typeName).HasMaxLength(50); 
        }
    }
}