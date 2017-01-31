using System;

namespace MTFS.Business.Domain.Model
{ 
    public partial class AgentCarrier : Base
    {  
        public int agentId { get; set; }
        public virtual Customer agent { get; set; }

        public int carrierId { get; set; }
        public virtual Customer carrier { get; set; }

        public int locationId { get; set; }
        public virtual Location location { get; set; }

        public bool isActive { get; set; }

    }
}
