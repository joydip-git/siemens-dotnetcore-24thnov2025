using Microsoft.IdentityModel.Tokens;
using Siemens.DotNetCore.PmsApp.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Siemens.DotNetCore.PmsApp.API.Models
{
    public class JwtTokenManager(IConfiguration configuration, ILogger<JwtTokenManager> logger) : ITokenManager
    {
        public string GenerateJSONWebToken(UserDTO user)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? "apiy1Wx2Pe5oFkrs68y0iTyUTGFNxwvdY8eekFfYXCm4lm4iwgF2FoogxAjeF3PTH4FNEMw5YXwTHetcJCXTOQuWiiiIUR30wPBJR0L0oC5wBzhZ35LpmlWTPcIyURXl"));

                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim(
                        type:JwtRegisteredClaimNames.Sub,
                        value:user.UserName),
                    //new Claim(type:JwtRegisteredClaimNames.Birthdate, value:user.DateOfBirth!=null?user.DateOfBirth.Value.ToString("yyyy-MM-dd"):""),
                    //new Claim(type:"Mobile", value:user.Mobile!=null? user.Mobile.Value.ToString():""),
                    //new Claim(type:JwtRegisteredClaimNames.Sub,value:new Guid().ToString())
                };

                var token = new JwtSecurityToken(
                    issuer: configuration["Jwt:Issuer"],
                    audience: configuration["Jwt:Audience"],
                    expires: DateTime.Now.AddMinutes(20),
                    signingCredentials: credentials,
                    claims: claims);

                var x = new JwtSecurityTokenHandler().WriteToken(token);
                logger.LogInformation(x.ToString());
                return x;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
