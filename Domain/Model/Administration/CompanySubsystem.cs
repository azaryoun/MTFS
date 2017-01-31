namespace MTFS.Business.Domain.Model
{  
    public partial class CompanySubsystem : Base
    {
        public CompanySubsystem()
        {
            return;
        }

        public int subsystemId { get; set; }
        public virtual Subsystem subsystem { get; set; }

        public int companyId { get; set; }
        public virtual Company company { get; set; }


    }
}
