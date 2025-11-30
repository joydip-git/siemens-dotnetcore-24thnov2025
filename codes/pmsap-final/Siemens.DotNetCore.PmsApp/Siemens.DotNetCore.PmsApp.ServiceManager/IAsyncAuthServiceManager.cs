using Siemens.DotNetCore.PmsApp.DTOs;

namespace Siemens.DotNetCore.PmsApp.ServiceManager
{
    public interface IAsyncAuthServiceManager
    {
        Task<bool> RegisterAsync(UserDTO user);
        Task<bool> AuthenticateAsync(UserDTO user);
    }
}
