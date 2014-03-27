using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using service.Domain;
using service.Handlers;

namespace service
{
    public class EndpointsController : ApiController
    {
        //not thread safe I know!!! for demo purposes!!!
        protected static List<FullUser> UsersStore = new List<FullUser>(new[]
        {
            new FullUser
            {
                Id = 1,
                Name = "Name1",
                Username = "Username1",
                Email = "email1@gmail.com",
                Role = "user",
                Password = "password1"
            },
            new FullUser
            {
                Id = 2,
                Name = "Name2",
                Username = "Username2",
                Email = "email2@gmail.com",
                Role = "admin",
                Password = "password2"
            },
            new FullUser
            {
                Id = 3,
                Name = "Name3",
                Username = "Username3",
                Email = "email3@gmail.com",
                Role = "user",
                Password = "password3"
            }
        });

        [HttpPost, Route("api/register")]
        public string Register()
        {
            return "this is the register endpoint";
        }

        [HttpPost, Route("api/authenticate")]
        public IHttpActionResult Authenticate(AuthenticateUser user)
        {
            var usr = (User)UsersStore.SingleOrDefault(u => u.Username == user.Username && u.Password == user.Password);

            if (usr == null) return new UnauthorizedResult();

            var jwt = JwtAuthenticationMessageHandler.CreateJWT(usr);
            return Ok(new Authenticated {user = usr, token = jwt});
        }

        [HttpGet, 
        Route("api/secure/users"), 
        Authorize]
        public IHttpActionResult Users()
        {
            return Ok(UsersStore.Cast<User>().ToList());
        }

        [HttpPost, 
        Route("api/secure/saveuser"),
        Authorize]
        public string SaveUser()
        {
            return "this is the save user endpoint";
        }

        [HttpPost, 
        Route("api/secure/udpatepassword"),
        Authorize(Roles = "admin")]
        public string UpdatePassword()
        {
            return "this is the update password endpoint";
        }

        [HttpPost, 
        Route("api/secure/updaterole"),
        Authorize(Roles = "admin")]
        public string UpdateRole()
        {
            return "this is the update role endpoint";
        }
    }

    public class UnauthorizedResult : ResponseMessageResult
    {
        public UnauthorizedResult() : base(new HttpResponseMessage(HttpStatusCode.Unauthorized))
        {
        }
    }
} ;