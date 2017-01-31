using System.Collections.Generic;

namespace MTFS.Business.Dtos.DtoClasses 
{
    public class GetPackagetypeDto : BaseDto
    {
        public string packageCode { get; set; }
        public string packageName { get; set; }
        public string packageDesc { get; set; }
        public bool isActive { get; set; } = true; 
         
    }
    public class GetPackagetypesDto : BaseDto
    {
        public string packageCode { get; set; }
        public string packageName { get; set; } 
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
    public class GetPackagetypesManagementDto : ManagementBaseDto
    {
        public List<GetPackagetypesDto> getPackagetypesDto { get; set; }

        public GetPackagetypesManagementDto()
        {
            getPackagetypesDto = new List<GetPackagetypesDto>();
        }
    }
}
