using Siemens.DotNetCore.PmsApp.DTOs;

namespace Siemens.DotNetCore.PmsApp.API.Models;

public interface ITokenManager
{
    string GenerateJSONWebToken(UserDTO user);
}