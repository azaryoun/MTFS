using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTFS.Business.Dtos.DtoClasses
{
    public class BaseDto
    { 
        public int id { get; set; }
        public int userId { get; set; }
        public int companyId { get; set; }
    }
}
