namespace Siemens.DotNetCore.PmsApp.Repository;

public class User
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public long? Mobile { get; set; }
    public DateOnly? DateOfBirth { get; set; }
}

