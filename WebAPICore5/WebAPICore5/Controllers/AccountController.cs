
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPICore5.Controllers
{
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        [HttpPost("register")]
        public IActionResult Register()
        {

        }
    }
}
