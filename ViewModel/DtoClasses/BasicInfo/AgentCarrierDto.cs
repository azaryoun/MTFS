using System;
using System.Collections.Generic;

namespace MTFS.Business.Dtos.DtoClasses
{
    public class GetAgentCarrierDto : BaseDto
    {
        public int agentId { get; set; }
        public int carrierId { get; set; }
        public int locationId { get; set; }
        public bool isActive { get; set; }
        public List<DdlDto> carriers { get; set; }
        public List<DdlDto> locations { get; set; }
          
    }

    public class GetAgentCarriersDto : BaseDto
    {
        public string carrierName { get; set; }
        public string locationName { get; set; }
        public bool isActive { get; set; } 
        public string isActivetext
        {
            get
            {
                if (isActive)
                    return "Active";
                else
                    return "In Active";
            } 
        }

    }

    public class GetAgentCarriersManagementDto : ManagementBaseDto
    {
        public GetAgentCarriersManagementDto()
        {
            getAgentCarriersDto = new List<GetAgentCarriersDto>();
        }
        public List<GetAgentCarriersDto> getAgentCarriersDto { get; set; }
        public string title { get; set; }
    }

}
