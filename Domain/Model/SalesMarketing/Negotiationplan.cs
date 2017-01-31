using System.Collections.Generic;

namespace MTFS.Business.Domain.Model
{ 
    public partial class Negotiationplan : Base
    {

        public Negotiationplan()
        {
            negotiationplanroutes = new HashSet<Negotiationplanroute>();
        }
        
        public int negotiationId { get; set; }
        public virtual Negotiation negotiation { get; set; }

        public string planName { get; set; }

        public bool isAccepted { get; set; }

        public virtual ICollection<Negotiationplanroute> negotiationplanroutes { get; set; }
    }

}
