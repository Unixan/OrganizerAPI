using OrganizerAPI.Controllers.Users.v1.Model;

namespace OrganizerAPI.Contracts.Users.v1;

public interface IUserData
{
    public Task<User> GetUser(AuthData login);
    public Task<User> CreateUser(NewUser user);

}