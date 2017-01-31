
using System.Collections.Generic;

namespace MTFS.Business.Domain.Model
{
    public class Customer : Base
    {
        // Table per Concrete class (TPC)

        public Customer()
        {
            negotiationContractors = new HashSet<Negotiation>();
            negotiationConsignees = new HashSet<Negotiation>();
            negotiationMarketers = new HashSet<Negotiation>();
            negotiationNotify1s = new HashSet<Negotiation>();
            negotiationNotify2s = new HashSet<Negotiation>();
            negotiationShippers = new HashSet<Negotiation>();
            negotiationplanrouteCFMarketersAgent = new HashSet<NegotiationplanrouteCF>();
            negotiationplanrouteCFMarketersCarrier = new HashSet<NegotiationplanrouteCF>();
            negotiationplanrouteCFMarketersForwarder = new HashSet<NegotiationplanrouteCF>();
            agentCarrierAgents = new HashSet<AgentCarrier>();
            agentCarrierCarriers = new HashSet<AgentCarrier>();

        }
        public string fullName { get; set; }
        public string IdentityNo { get; set; }
        public string telephone { get; set; }
        public string mobile { get; set; }
        public string fax { get; set; }
        public string email1 { get; set; }
        public string email2 { get; set; }
        public string email3 { get; set; }
        public string address { get; set; }
        public string webSite { get; set; }
        public bool isReal { get; set; }
        public bool isActive { get; set; }
        public int countryId { get; set; }
        public virtual Country country { get; set; }

        public bool isAgent { get; set; }
        public bool isCarrier { get; set; }
        public bool isForwarder { get; set; }
        public bool isMarketer { get; set; }


        public bool isShipper { get; set; }
        public bool isConsignee { get; set; }
        public bool isNotify { get; set; }
      

        public virtual ICollection<Negotiation> negotiationContractors { get; set; }
        public virtual ICollection<Negotiation> negotiationShippers { get; set; }
        public virtual ICollection<Negotiation> negotiationConsignees { get; set; }
        public virtual ICollection<Negotiation> negotiationNotify1s { get; set; }
        public virtual ICollection<Negotiation> negotiationNotify2s { get; set; }
        public virtual ICollection<Negotiation> negotiationMarketers { get; set; }
        public virtual ICollection<NegotiationplanrouteCF> negotiationplanrouteCFMarketersCarrier { get; set; }
        public virtual ICollection<NegotiationplanrouteCF> negotiationplanrouteCFMarketersAgent { get; set; }
        public virtual ICollection<NegotiationplanrouteCF> negotiationplanrouteCFMarketersForwarder { get; set; }

        public virtual ICollection<AgentCarrier> agentCarrierAgents { get; set; }
        public virtual ICollection<AgentCarrier> agentCarrierCarriers { get; set; }
        

    }
}
