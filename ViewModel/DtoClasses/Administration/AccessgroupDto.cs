using System.Collections.Generic;

namespace MTFS.Business.Dtos.DtoClasses
{

    public class AccessgroupDto:BaseDto 
    { 
        public string groupName { get; set; }
        public bool? isChecked { get; set; }
        public int ownerCompanyId { get; set; }
        public List<MenuitemDto> menuItemsDto { get; set; }

        public AccessgroupDto()
        {
             menuItemsDto = new List<MenuitemDto>();
        }
    }

    public class AccessgroupsManagementDto : ManagementBaseDto
    {
        public List<AccessgroupDto> accessgroupsDto { get; set; }

        public AccessgroupsManagementDto()
        {
            this.accessgroupsDto = new List<AccessgroupDto>();
        }
         
    }

}