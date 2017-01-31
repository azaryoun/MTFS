using System.Collections.Generic;
namespace MTFS.Business.Domain.Model
{  
    public partial class Transporttype : Base
    {

        public Transporttype()
        {
            locationTransporttypes = new HashSet<LocationTransporttype>();
            negotiationplanroutes = new HashSet<Negotiationplanroute>();
        }
   
        public string typeName { get; set; }

        public virtual ICollection<LocationTransporttype> locationTransporttypes { get; set; }
        public virtual ICollection<Negotiationplanroute> negotiationplanroutes { get; set; }

    }
}
