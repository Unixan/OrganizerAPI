using Dapper;
using OrganizerAPI.Contracts.Users.v1;
using OrganizerAPI.Controllers.Users.v1.Model;
using System.Data.SqlClient;
using System.Reflection;

namespace OrganizerAPI.Controllers.Users.v1;

public class UserData : IUserData
{
    private string _connstring =
        @"Data Source=(localdb)\local;Initial Catalog=OrganizerApp;Integrated Security=True";


    public async Task<User> GetUser(AuthData login)
    {
        if (IsNotValid(login)) return null;
        var conn = new SqlConnection(_connstring);
        const string sql = "SELECT * FROM [User] WHERE Email = @email AND Password = @password";
        var res = await conn.QueryFirstOrDefaultAsync<User>(sql, new { Email = login.AccountName, Password = login.Password });
        Console.WriteLine(res);
        return res;
    }

    public async Task<User> CreateUser(NewUser user)
    {

        if (IsNotValid(user)) return null;
        if (await CheckIfExists(user.Email)) return null;
        user.UserId = Guid.NewGuid();
        var conn = new SqlConnection(_connstring);
        const string sql =
              "INSERT [User] (UserId, ScreenName, Email, Password) VALUES (@UserId, @ScreenName, @Email, @Password)";
        var affectedRows = await conn.ExecuteAsync(sql, user);
        if (affectedRows < 1) return null;
        var res = await GetUser(new AuthData(user.Email, user.Password));
        return res;
    }

    private async Task<bool> CheckIfExists(string email)
    {
        var conn = new SqlConnection(_connstring);
        const string sql = "SELECT * FROM [User] WHERE Email = @email";
        var res = await conn.QueryFirstOrDefaultAsync<User>(sql, new { email });
        if (res == null) return false;
        return true;
    }

    private bool IsNotValid(object obj)
    {
        PropertyInfo[] properties = obj.GetType().GetProperties();
        foreach (var prop in properties)
        {
            object value = prop.GetValue(obj);
            if (value == null || (value is string && string.IsNullOrWhiteSpace((string)value))) return true;
        }
        return false;
    }
}