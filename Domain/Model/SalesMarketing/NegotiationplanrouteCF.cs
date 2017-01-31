
namespace MTFS.Business.Domain.Model
{ 
    public partial class NegotiationplanrouteCF : Base
    {

        public NegotiationplanrouteCF()
        {

        } 
        public int negotiationplanrouteId { get; set; }
        public virtual Negotiationplanroute negotiationplanroute { get; set; }

      
        public double netPrice { get; set; }
              
        //public int currencyId { get; set; }
        //public virtual Currency currency { get; set; }

        public int? carrierId { get; set; }
        public virtual Customer carrier { get; set; }

        public int? agentId { get; set; }
        public virtual Customer agent { get; set; }

        public int? forwarderId { get; set; }
        public virtual Customer forwarder { get; set; } 

        public bool isAccepted { get; set; }

    }

}
