
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Core;
using WebAPI.Store.IServices;
using WebAPICore5.Models;

namespace WebAPICore5.Controllers
{
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }

            var newUser = new User()
            {
                Email = model.Email,
                passwordHash = model.password,
                DateOfBirth = model.DateOfBirth,
                RoleId = model.RoleId
            };

            userService.Register(newUser);

            return Ok();
        }
    }
}
