
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Core;
using WebAPI.Store.IServices;
using WebAPICore5.Identity;
using WebAPICore5.Models;

namespace WebAPICore5.Controllers
{
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly Microsoft.AspNetCore.Identity.IPasswordHasher<User> passwordHasher;
        private readonly IJwtProvider jwtProvider;

        public AccountController(IUserService userService, IPasswordHasher<User> passwordHasher, IJwtProvider jwtProvider)
        {
            this.userService = userService;
            this.passwordHasher = passwordHasher;
            this.jwtProvider = jwtProvider;
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
                Name = model.Name,
                Email = model.Email,
                DateOfBirth = model.DateOfBirth,
                Nationality = model.Nationality,
                RoleId = model.RoleId
            };
            var passwordHash = passwordHasher.HashPassword(newUser, model.Password);
            newUser.passwordHash = passwordHash;

            userService.Register(newUser);

            return Ok();
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginModel loginModel)
        {
            var user = userService.GetUser(loginModel.Email);

            if (user == null)
            {
                return BadRequest("Invalid username or password");

            }

            var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.passwordHash, loginModel.Password);
            if (passwordVerificationResult == PasswordVerificationResult.Failed)
            {
                return BadRequest("Invalid username or password");

            }
            var token = jwtProvider.GenerateToken(user);
            return Ok(token);

        }
    }
}
