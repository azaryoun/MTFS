using System.Collections.Generic;

namespace MTFS.Business.Dtos.DtoClasses
{
    public partial class TransporttypeDto
    {
        public int id { get; set; }
        public string typeName { get; set; }
        public bool isChecked { get; set; }

    }

    public partial class TransporttypesDto
    {

        public List<TransporttypeDto> transporttypeDto { get; set; }

        public TransporttypesDto()
        {

            this.transporttypeDto = new List<TransporttypeDto>();
        }
    }
}
