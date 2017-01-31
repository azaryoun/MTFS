using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTFS.Host.dotNetCore.Services
{
    public interface IUser
    {
        UserDto getUser(int id);

    }
}
