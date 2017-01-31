using System.Collections.Generic;

namespace MTFS.Business.Domain.Model
{ 
     
    public partial class Company:Base
    {
    
        public Company()
        {
            users = new HashSet<User>();
            companySubsystems = new HashSet<CompanySubsystem>();
            ownedAccessgroups = new HashSet<Accessgroup>();
            return;
        }

         
        public string companyName { get; set; }
         
        public string address { get; set; }
         
        public string telephone { get; set; }
         
        public string nationalCode { get; set; }

        public byte[] logo { get; set; }
         
        public string logoMime { get; set; }
         
        public string fax { get; set; }
         
        public string webDomain { get; set; }
         
        public string agentName { get; set; }
         
        public string email { get; set; }

        public bool isActive { get; set; }

        public virtual ICollection<User> users { get; set; }

        public virtual ICollection<CompanySubsystem> companySubsystems { get; set; }

        public virtual ICollection<Accessgroup> ownedAccessgroups { get; set; }


    }
}
