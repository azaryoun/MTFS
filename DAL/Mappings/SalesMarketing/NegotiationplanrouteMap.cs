using MTFS.Business.Domain.Model;
using System.Data.Entity.ModelConfiguration; 

namespace MTFS.DAL.Mapping
{
    public class NegotiationplanrouteMap : EntityTypeConfiguration<Negotiationplanroute>
    {
        public NegotiationplanrouteMap()
        {
            ToTable("Negotiationplanroute", "SalesMarketing"); 

            HasRequired(e => e.negotiationplan)
            .WithMany(e => e.negotiationplanroutes)
            .HasForeignKey(e => e.negotiationplanId)
            .WillCascadeOnDelete(false);

            HasRequired(e => e.fromLocation)
            .WithMany(e => e.asFromLocationNegotiationplanroutes)
            .HasForeignKey(e => e.fromLocationId)
            .WillCascadeOnDelete(false);
            
            HasRequired(e => e.toLocation)
            .WithMany(e => e.asToLocationNegotiationplanroutes)
            .HasForeignKey(e => e.toLocationId)
            .WillCascadeOnDelete(false);

         
            HasRequired(e => e.transporttype)
            .WithMany(e => e.negotiationplanroutes)
            .HasForeignKey(e => e.transporttypeId)
            .WillCascadeOnDelete(false);

            
        }

    }
}
