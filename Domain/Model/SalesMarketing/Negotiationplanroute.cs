using System.Collections.Generic;

namespace MTFS.Business.Domain.Model
{  
    public partial class Negotiationplanroute : Base
    {

        public Negotiationplanroute()
        {
            negotiationplanrouteCFs = new HashSet<NegotiationplanrouteCF>();
        }


        public int negotiationplanId { get; set; }
        public virtual Negotiationplan negotiationplan { get; set; }

        public int fromLocationId { get; set; }
        public virtual Location fromLocation { get; set; }

        public int toLocationId { get; set; }
        public virtual Location toLocation { get; set; }
        
        public int transporttypeId { get; set; }
        public virtual Transporttype transporttype { get; set; }
                
        public virtual ICollection<NegotiationplanrouteCF> negotiationplanrouteCFs { get; set; }
    }

}
