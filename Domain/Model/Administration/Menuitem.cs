using System.Collections.Generic;

namespace MTFS.Business.Domain.Model
{  
    public partial class Menuitem:Base
    { 
        public Menuitem()
        {
            accessgroupMenuitems = new HashSet<AccessgroupMenuitem>();
        } 

        public int menutitleId { get; set; }
        public virtual Menutitle menutitle { get; set; }
         
        public string itemText { get; set; }

        public int? displayOrder { get; set; }
         
        public string itemStyle { get; set; }
         
        public string href { get; set; }

        public bool? hasExtraInfo { get; set; }
         
        public string extraInfoStyle { get; set; }
         
        public string pageTitle { get; set; }

        public virtual ICollection<AccessgroupMenuitem> accessgroupMenuitems { get; set; }


    }
}
