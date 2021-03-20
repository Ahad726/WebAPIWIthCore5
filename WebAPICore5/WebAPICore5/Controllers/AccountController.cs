
using Microsoft.AspNetCore.Identity;
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
        private readonly Microsoft.AspNetCore.Identity.IPasswordHasher<User> passwordHasher;

        public AccountController(IUserService userService, IPasswordHasher<User> passwordHasher)
        {
            this.userService = userService;
            this.passwordHasher = passwordHasher;
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
                DateOfBirth = model.DateOfBirth,
                RoleId = model.RoleId
            };
            var passwordHash = passwordHasher.HashPassword(newUser, model.password);
            newUser.passwordHash = passwordHash;

            userService.Register(newUser);

            return Ok();
        }
    }
}
