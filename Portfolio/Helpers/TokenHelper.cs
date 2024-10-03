using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Portfolio;
using Portfolio.Entities;
using Portfolio.Entities.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BuscoAPI.Helpers
{
    public class TokenHelper
    {
        public static async Task<UserToken> BuildToken<T>(T userCreation, ApplicationDbContext context, 
            IConfiguration config) where T:IUserDto
        {
            var claims = new List<Claim>(){};

            if(!string.IsNullOrEmpty(userCreation.Email)){
                claims.Add(new Claim(ClaimTypes.Email, userCreation.Email));
            }

            if (!string.IsNullOrEmpty(userCreation.Username)){
                claims.Add(new Claim(ClaimTypes.Name, userCreation.Username));
            }

            claims.Add(new Claim(ClaimTypes.Role, userCreation.Role));

            var user = await context.Users.FirstAsync(x => x.Email == userCreation.Email || x.Username == userCreation.Username);
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.Username.ToString()));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Authentication:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddYears(1);

            JwtSecurityToken token = new(
                issuer: config["Authentication:Issuer"],
                audience: config["Authentication:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
