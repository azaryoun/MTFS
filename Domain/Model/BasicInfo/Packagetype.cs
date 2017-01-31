
namespace MTFS.Business.Domain.Model
{ 
    public partial class Packagetype : Base
    { 
        public string packageCode { get; set; } 
        public string packageName { get; set; } 
        public string packageDesc { get; set; } 
        public bool isActive { get; set; }
        
    }
}
