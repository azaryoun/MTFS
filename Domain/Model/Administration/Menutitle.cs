using System.Collections.Generic;

namespace MTFS.Business.Domain.Model
{  
    public partial class Menutitle : Base
    {
        public Menutitle()
        {
            menuitems = new HashSet<Menuitem>();
        }
         
        public string titleText { get; set; }

        public int? displayOrder { get; set; }
         
        public string titleStyle { get; set; }
         
        public string href { get; set; }

        public bool? hasExtraInfo { get; set; }
         
        public string extraInfoStyle { get; set; }
         
        public string pageTitle { get; set; }

        public int subsystemId { get; set; }
        public virtual Subsystem subsystem { get; set; }

        public virtual ICollection<Menuitem> menuitems { get; set; }
    }
}
