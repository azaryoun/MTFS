using MTFS.Business.Domain.Model;
using System.Data.Entity.ModelConfiguration; 

namespace MTFS.DAL.Mapping
{
    public class NegotiationplanMap : EntityTypeConfiguration<Negotiationplan>
    {
        public NegotiationplanMap()
        {
            ToTable("Negotiationplan", "SalesMarketing"); 

            HasRequired(e => e.negotiation)
               .WithMany(e => e.negotiationplans)
               .HasForeignKey(e => e.negotiationId)
               .WillCascadeOnDelete(false)
               ;
             
        }
    }
}
