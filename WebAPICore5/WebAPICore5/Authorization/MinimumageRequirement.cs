using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPICore5.Authorization
{
    public class MinimumageRequirement : IAuthorizationRequirement
    {
        public int MinimumAge { get; }
        public MinimumageRequirement(int minimumAge)
        {
            MinimumAge = minimumAge;
        }

    }
}
