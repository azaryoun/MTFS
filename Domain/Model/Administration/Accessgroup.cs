using System.Collections.Generic;

namespace MTFS.Business.Domain.Model
{

    public partial class Accessgroup:Base
    {
         public Accessgroup()
        {
            accessgroupMenuitems = new HashSet<AccessgroupMenuitem>();
            accessgroupUsers = new HashSet<AccessgroupUser>();
        }

   
        public string groupName { get; set; }

      
        public int ownerCompanyId { get; set; }
        public virtual Company ownerCompany { get; set; }

        public virtual ICollection<AccessgroupMenuitem> accessgroupMenuitems { get; set; }

        public virtual ICollection<AccessgroupUser> accessgroupUsers { get; set; }
    }
}
