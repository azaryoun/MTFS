namespace MTFS.Business.Domain.Model
{ 
    public partial class AccessgroupMenuitem : Base
    {


        public int accessgroupId { get; set; }

        public int menuitemId { get; set; }

        public virtual Accessgroup accessgroup { get; set; }

        public virtual Menuitem menuitem { get; set; }
    }
}
