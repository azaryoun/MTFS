using System.Collections.Generic;

namespace MTFS.Business.Dtos.DtoClasses
{
    public class GetCustomerDto : GetPersonDto
    {
        public bool isShipper { get; set; }

        public bool isConsignee { get; set; }

        public bool isNotify { get; set; }

    }

    public class GetCustomersDto : GetPersonsDto
    {

    }

    public class GetCustomersManagementDto : ManagementBaseDto
    {
        public GetCustomersManagementDto()
        {
            getCustomersDto = new List<GetCustomersDto>();
        }
        public List<GetCustomersDto> getCustomersDto { get; set; }
    }
    
    public class GetCustomerTypeDto
    {
        public int id { get; set; }
        public bool isReal { get; set; }
        public bool isShipper { get; set; } 
        public bool isConsignee { get; set; } 
        public bool isNotify { get; set; }

    }

}
