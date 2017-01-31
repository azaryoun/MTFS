using System.Collections.Generic;
namespace MTFS.Business.Domain.Model
{  
    public partial class Location : Base
    { 
        public Location()
        {
            locationTransporttypes = new HashSet<LocationTransporttype>();
            asPlaceofDeliveryNegotiations = new HashSet<Negotiation>();
            asPlaceofReceiptNegotiations = new HashSet<Negotiation>();
            asFromLocationNegotiationplanroutes = new HashSet<Negotiationplanroute>();
            asToLocationNegotiationplanroutes = new HashSet<Negotiationplanroute>();
            agentCarriers = new HashSet<AgentCarrier>(); 

        }
         
        public string locationCode { get; set; } 
        public string locationName { get; set; } 
        public bool isActive { get; set; } 
        public int countryId { get; set; }
        public virtual Country country { get; set; } 
        public virtual ICollection<LocationTransporttype> locationTransporttypes { get; set; }
        public virtual ICollection<Negotiation> asPlaceofDeliveryNegotiations { get; set; }
        public virtual ICollection<Negotiation> asPlaceofReceiptNegotiations { get; set; }
        public virtual ICollection<Negotiationplanroute> asFromLocationNegotiationplanroutes { get; set; }
        public virtual ICollection<Negotiationplanroute> asToLocationNegotiationplanroutes { get; set; }
        public virtual ICollection<AgentCarrier> agentCarriers { get; set; } 
    }
}
