using MTFS.Business.Domain.Model;
using System.Data.Entity.ModelConfiguration;


namespace MTFS.DAL.Mapping
{
    public class AgentCarrierMap : EntityTypeConfiguration<AgentCarrier>
    {
        public AgentCarrierMap()
        {
            ToTable("AgentCarrier", "BasicInfo"); 

            HasRequired(e => e.agent)
           .WithMany(e => e.agentCarrierAgents)
           .HasForeignKey(e => e.agentId);

            HasRequired(e => e.carrier)
            .WithMany(e => e.agentCarrierCarriers)
            .HasForeignKey(e => e.carrierId)
            .WillCascadeOnDelete(false)
            ;
            

            HasRequired(e => e.location)
            .WithMany(e => e.agentCarriers)
            .HasForeignKey(e => e.locationId)
            ;


        }
    }
}
