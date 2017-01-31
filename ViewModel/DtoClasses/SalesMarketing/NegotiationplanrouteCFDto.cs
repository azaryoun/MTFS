using System.Collections.Generic;

namespace MTFS.Business.Dtos.DtoClasses
{
    public class GetNegotiationplanrouteCFDto : BaseDto
    {
        public int negotiationplanrouteId { get; set; }  
        public double netPrice { get; set; }
          
        public int? carrierId { get; set; }
        public List<DdlWithParentDto> agentCarriers { get; set; } 

        public int? forwarderId { get; set; }
        public List<DdlDto> forwarders { get; set; }
        public bool isAccepted { get; set; }

    }

    public class GetNegotiationplanrouteCFsDto : BaseDto
    {
        public string agentName { get; set; }
        public string carrierrName { get; set; }
        public string forwarderName { get; set; }
        public double netPrice { get; set; }
        public bool isAccepted { get; set; }
        public string acceptText
        {
            get
            {
                if (isAccepted)
                    return "Accepted";
                else
                    return "Not Accepted";
            }
        }

    }

    public class GetNegotiationplanrouteCFsManagementDto : ManagementBaseDto
    {
        public GetNegotiationplanrouteCFsManagementDto()
        {
            getNegotiationplanrouteCFsDto = new List<GetNegotiationplanrouteCFsDto>();
        }
        public List<GetNegotiationplanrouteCFsDto> getNegotiationplanrouteCFsDto { get; set; }
        public string title { get; set; }
        public int parent_ParentId { get; set; }
    }
      
}
