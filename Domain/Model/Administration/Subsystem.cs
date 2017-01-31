using System.Collections.Generic;

namespace MTFS.Business.Domain.Model
{  
    public partial class Subsystem : Base
    {
        public Subsystem()
        {
            companySubsystems = new HashSet<CompanySubsystem>();
            menutitles = new HashSet<Menutitle>();
        }
         
        public string subsyatemName { get; set; }
         
        public string subsyatemDescription { get; set; }

        public bool? isActive { get; set; }

        public virtual ICollection<CompanySubsystem> companySubsystems { get; set; }

        public virtual ICollection<Menutitle> menutitles { get; set; }
    }
}
