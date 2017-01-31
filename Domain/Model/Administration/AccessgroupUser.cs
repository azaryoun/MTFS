namespace MTFS.Business.Domain.Model
{
    public partial class AccessgroupUser : Base
    {
   
        public int accessgroupId { get; set; }
        public virtual Accessgroup accessgroup { get; set; }

        
        public int userId { get; set; }
        public virtual User user { get; set; }

      
    }
}
