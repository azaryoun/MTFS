using System.Collections.Generic;

namespace MTFS.Business.Dtos.DtoClasses
{
    public class GetNegotiationplanrouteDto : BaseDto
    {
        public int negotiationplanId { get; set; } 

        public int fromLocationId { get; set; }
        public List<DdlDto> fromLocations { get; set; }

        public int toLocationId { get; set; }
        public List<DdlDto> toLocations { get; set; }

        public int transporttypeId { get; set; }
        public List<DdlDto> transporttypes { get; set; }

    }

    public class GetNegotiationplanroutesDto : BaseDto
    {
        public string fromLocationName { get; set; }
        public string toLocationName { get; set; }
        public string transporttypeName { get; set; }

    }

    public class GetNegotiationplanroutesManagementDto : ManagementBaseDto
    {
        public GetNegotiationplanroutesManagementDto()
        {
            getNegotiationplanroutesDto = new List<GetNegotiationplanroutesDto>();
        }
        public List<GetNegotiationplanroutesDto> getNegotiationplanroutesDto { get; set; }
        public string title { get; set; }
        public int parent_ParentId { get; set; }
    }

}
