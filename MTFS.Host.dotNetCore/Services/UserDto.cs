using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTFS.Host.dotNetCore.Services
{
    public class UserDto
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string fullName { get; set; }
    }
}
