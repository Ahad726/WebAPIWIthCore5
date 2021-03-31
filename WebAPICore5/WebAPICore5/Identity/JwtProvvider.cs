using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Core;

namespace WebAPICore5.Identity
{
    // this class generate JWT token based on user field
    public class JwtProvvider : IJwtProvider
    {
        private readonly JwtOption jwtOption;

        public JwtProvvider(JwtOption jwtOption)
        {
            this.jwtOption = jwtOption;
        }
        public string GenerateToken(User user)
        {

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.RoleName),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim("DateOfBirth", user.DateOfBirth.ToString())
            };

            var key =new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOption.JwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(jwtOption.JwtExpireDays);

            var tokens = new JwtSecurityToken(

                jwtOption.JwtIssuer,
                jwtOption.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: creds

                );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(tokens);

        }
    }
}
