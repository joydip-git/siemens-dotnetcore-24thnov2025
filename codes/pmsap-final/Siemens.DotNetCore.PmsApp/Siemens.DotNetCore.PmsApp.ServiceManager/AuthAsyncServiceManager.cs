using Siemens.DotNetCore.PmsApp.Repository;
using Siemens.DotNetCore.PmsApp.DTOs;

namespace Siemens.DotNetCore.PmsApp.ServiceManager
{
    public class AuthAsyncServiceManager(SiemensDbContext context) : IAsyncAuthServiceManager
    {

        public Task<bool> AuthenticateAsync(UserDTO user)
        {
            try
            {
                bool exists = context.Users.Any(u => u.UserName == user.UserName && u.Password == user.Password);
                return Task.FromResult<bool>(exists);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> RegisterAsync(UserDTO user)
        {
            try
            {
                var found = context.Users.Any(u => u.UserName == user.UserName && u.Password == user.Password);
                if (!found)
                {
                    context.Users.Add(Mapper.Map<UserDTO,User>(user));
                    int res = await context.SaveChangesAsync();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
