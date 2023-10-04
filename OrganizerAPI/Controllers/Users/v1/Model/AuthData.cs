using Microsoft.AspNetCore.Authentication;

namespace OrganizerAPI.Controllers.Users.v1.Model;

public class AuthData
{
    public string AccountName { get; set; }
    public string Password { get; set; }

    public AuthData(string accountName, string password)
    {
        AccountName = accountName;
        Password = password;
    }
}