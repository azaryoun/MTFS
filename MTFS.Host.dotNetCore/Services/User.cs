using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTFS.Host.dotNetCore.Services
{
    public class User : IUser
    {
     

        public UserDto getUser(int id)
        {
            if (id % 2 == 0)
                return (new UserDto {id=id,fullName="Donald Trump",password="1234",username="Username"});
            return (null);
        }
    }
}
