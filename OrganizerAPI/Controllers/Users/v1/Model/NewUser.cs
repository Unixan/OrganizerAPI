namespace OrganizerAPI.Controllers.Users.v1.Model;

public class NewUser
{
    public Guid UserId { get; set; }
    public string ScreenName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}