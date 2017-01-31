using System.Collections.Generic;

namespace MTFS.Business.Domain.Model
{  
    public partial class User:Base
    {
    
        public User()
        {
            accessgroupUsers = new HashSet<AccessgroupUser>();
        }
         
        public string username { get; set; } 
        public string password { get; set; } 
        public string fullName { get; set; } 
        public bool isItemAdmin { get; set; } 
        public bool isDataAdmin { get; set; } 
        public string nationalCode { get; set; } 
        public string mobile { get; set; } 
        public string telephone { get; set; } 
        public string email { get; set; } 
        public string address { get; set; } 
        public bool isActive { get; set; } 
        public int companyId { get; set; }
        public virtual Company company { get; set; } 
        public virtual ICollection<AccessgroupUser> accessgroupUsers { get; set; }
        //[InverseProperty("cc")]
        //public virtual ICollection<Base> createdUser { get; set; }
        //[InverseProperty("mm")]
        //public virtual ICollection<Base> ModifiedUser { get; set; }
        //public ICollection<Base> ModelBases { get; set; }
    }
}
