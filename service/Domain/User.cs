using System.Security.Claims;

namespace service.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

    }

    public static class UserExtensions
    {
        public static ClaimsIdentity ToClaimsIdentity(this User user)
        {
            return new ClaimsIdentity(new []{
                new Claim(ClaimTypes.Email, user.Email), 
                new Claim(ClaimTypes.Role, user.Role), 
                new Claim(ClaimTypes.Sid, user.Id.ToString()), 
                new Claim(ClaimTypes.Upn, user.Username), 
                new Claim(ClaimTypes.Name, user.Name)
            }, 
            "JSON Web token authentication");
        }
    }
}