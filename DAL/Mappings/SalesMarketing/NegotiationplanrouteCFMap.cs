using MTFS.Business.Domain.Model;
using System.Data.Entity.ModelConfiguration;

namespace MTFS.DAL.Mapping
{
    public class NegotiationplanrouteCFMap : EntityTypeConfiguration<NegotiationplanrouteCF>
    {
        public NegotiationplanrouteCFMap()
        {
            ToTable("NegotiationplanrouteCF", "SalesMarketing"); 
              
            HasRequired(e => e.negotiationplanroute)
            .WithMany(e => e.negotiationplanrouteCFs)
            .HasForeignKey(e => e.negotiationplanrouteId)
            .WillCascadeOnDelete(false);

            //HasRequired(e => e.currency)
            //.WithMany(e => e.negotiationplanrouteCFs)
            //.HasForeignKey(e => e.currencyId)
            //.WillCascadeOnDelete(false);
            
            HasOptional(e => e.carrier)
            .WithMany(e => e.negotiationplanrouteCFMarketersCarrier)
            .HasForeignKey(e => e.carrierId)
            .WillCascadeOnDelete(false);


            HasOptional(e => e.agent)
            .WithMany(e => e.negotiationplanrouteCFMarketersAgent)
            .HasForeignKey(e => e.agentId)
            .WillCascadeOnDelete(false);


            HasOptional(e => e.forwarder)
            .WithMany(e => e.negotiationplanrouteCFMarketersForwarder)
            .HasForeignKey(e => e.forwarderId)
            .WillCascadeOnDelete(false);



        }

    }
}
