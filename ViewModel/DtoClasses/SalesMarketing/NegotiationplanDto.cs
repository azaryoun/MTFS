using System.Collections.Generic;

namespace MTFS.Business.Dtos.DtoClasses
{
    public class GetNegotiationplanDto : BaseDto
    {
        public int negotiationId { get; set; }
        public string planName { get; set; }
        public bool isAccepted { get; set; }

    }

    public class GetNegotiationplansDto : BaseDto
    {
        public int negotiationId { get; set; }
        public string planName { get; set; }
        public bool isAccepted { get; set; } = false;
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

    public class GetNegotiationplansManagementDto : ManagementBaseDto
    {
        public GetNegotiationplansManagementDto()
        {
            getNegotiationplansDto = new List<GetNegotiationplansDto>();
        }
        public List<GetNegotiationplansDto> getNegotiationplansDto { get; set; }
        public string title { get; set; }
        public int parent_ParentId { get; set; }
    }

}
