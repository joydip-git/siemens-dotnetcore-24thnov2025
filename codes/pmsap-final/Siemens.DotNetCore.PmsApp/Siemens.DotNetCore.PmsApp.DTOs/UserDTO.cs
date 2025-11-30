namespace Siemens.DotNetCore.PmsApp.DTOs
{
    public class UserDTO
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public long? Mobile { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
