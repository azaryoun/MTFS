using System.Collections.Generic;

namespace MTFS.Business.Domain.Model
{  
    public partial class Negotiation : Base
    {

        public Negotiation()
        {
            negotiationplans = new HashSet<Negotiationplan>();
        }

        public int placeofReceiptLocationId { get; set; }
        public virtual Location placeofReceiptLocation { get; set; }

        public int placeofDeliveryLocationId { get; set; }
        public virtual Location placeofDeliveryLocation { get; set; }

        public bool isInboundProcess { get; set; }

        public bool isCostplus { get; set; } 
        public string referenceNo { get; set; } 
        public string goodsDesc { get; set; } 
        public double? costplusPercent { get; set; } 
        public double? totalNetPrice { get; set; } 
        public double totalPayablePrice { get; set; } 
        public int currencyId { get; set; }
        public virtual Currency currency { get; set; }
        public NegotiationStates state { get; set; } = NegotiationStates.UnderNegotiation;
        public int contractorId { get; set; }
        public virtual Customer contractor { get; set; } //Contractor in UI 
        public int? shipperId { get; set; }
        public virtual Customer shipper { get; set; } 
        public int? consigneeId { get; set; }
        public virtual Customer consignee { get; set; } 
        public int? notify1Id { get; set; }
        public virtual Customer notify1 { get; set; } 
        public int? notify2Id { get; set; }
        public virtual Customer notify2 { get; set; } 
        public int? marketerId { get; set; }
        public virtual Customer marketer { get; set; } 
        public virtual ICollection<Negotiationplan> negotiationplans { get; set; }
    }


}
