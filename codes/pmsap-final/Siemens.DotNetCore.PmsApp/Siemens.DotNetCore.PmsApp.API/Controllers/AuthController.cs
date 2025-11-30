using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Siemens.DotNetCore.PmsApp.API.Models;
using Siemens.DotNetCore.PmsApp.DTOs;
using Siemens.DotNetCore.PmsApp.ServiceManager;

namespace Siemens.DotNetCore.PmsApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAsyncAuthServiceManager authServiceManager, ITokenManager tokenManager) : ControllerBase
    {
        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public async Task<ActionResult<UserDTO>> RegisterUser([FromBody] UserDTO user)
        {
            try
            {
                return await authServiceManager.RegisterAsync(user) ? CreatedAtAction(nameof(RegisterUser), user) : BadRequest("user already exists");
            }
            catch (Exception ex)
            {
                return Problem(ex.ToString());
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<ActionResult<string>> AuthenticateUser([FromBody] UserDTO user)
        {
            try
            {                
                var validated = await authServiceManager.AuthenticateAsync(user);
                if (validated)
                {
                    var token = tokenManager.GenerateJSONWebToken(user);
                    return Ok(token);
                }
                else
                    return BadRequest("user does not exist");
            }
            catch (Exception ex)
            {
                return Problem(ex.ToString());
            }
        }
    }
}
