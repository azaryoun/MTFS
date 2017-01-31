
namespace MTFS.Business.Domain.Model
{ 
    public partial class LocationTransporttype : Base
    { 
        public int locationId { get; set; }
        public virtual Location location { get; set; } 
        public int transporttypeId { get; set; }
        public virtual Transporttype transporttype { get; set; } 
    }
}
