using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Core;

namespace WebAPICore5.Identity
{
    public interface IJwtProvider
    {
        string GenerateToken(User user);
    }
}
