using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrganizerAPI.Contracts.Users.v1;
using OrganizerAPI.Controllers.Users.v1.Model;

namespace OrganizerAPI.Controllers.Users.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserData _userData;

        public UserController(IUserData userData)
        {
            _userData = userData;
        }

        [HttpPut("getUser")]
        public async Task<User> GetUser([FromBody] AuthData login)
        {
            
            return await _userData.GetUser(login);
        }

        [HttpPost("createUser")]
        public async Task<object> CreatNewUser([FromBody] NewUser newUser)
        {
            return await _userData.CreateUser(newUser);
        }
    }
}
