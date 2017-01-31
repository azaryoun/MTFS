using System.Collections.Generic;

namespace MTFS.Business.Dtos.DtoClasses
{
    public class GetLocationDto : BaseDto
    {
        public string locationCode { get; set; }

        public string locationName { get; set; }

        public bool isActive { get; set; } = true;
         
        public int countryId { get; set; }  

        public List<DdlDto > countries { get; set; }

        public List<TransporttypeDto> transporttypes { get; set; }

        public GetLocationDto()
        {
             transporttypes = new List<TransporttypeDto>();
        }

    }
    public class GetLocationsDto : BaseDto
    {
        public string locationCode { get; set; }

        public string locationName { get; set; }

        public string  countryName { get; set; }

        public bool isActive { get; set; }

        public string isActiveText
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
    public class GetLocationsManagementDto : ManagementBaseDto
    {
        public List<GetLocationsDto> getLocationsDto { get; set; }

        public GetLocationsManagementDto()
        {
            getLocationsDto = new List<GetLocationsDto>();
        }
    }
  

}
