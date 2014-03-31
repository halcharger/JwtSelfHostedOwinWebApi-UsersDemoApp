using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Newtonsoft.Json;
using service.Domain;
using service.Handlers;

namespace service
{
    public class EndpointsController : ApiController
    {
        [HttpPost, Route("api/register")]
        public string Register()
        {
            return "this is the register endpoint";
        }

        [HttpPost, Route("api/authenticate")]
        public IHttpActionResult Authenticate(AuthenticateUser user)
        {
            var usr = (User)Storage.Users.SingleOrDefault(u => u.Username == user.Username && u.Password == user.Password);

            if (usr == null) return new InternalServerErrorResult("Invalid username or password.");

            var jwt = JwtAuthenticationMessageHandler.CreateJWT(usr);
            return Ok(new Authenticated {user = usr, token = jwt});
        }

        [HttpGet, 
        Route("api/secure/users"), 
        Authorize]
        public IHttpActionResult Users()
        {
            return Ok(Storage.Users.Cast<User>().ToList());
        }

        [HttpPost, 
        Route("api/secure/saveuser"),
        Authorize]
        public IHttpActionResult SaveUser(User user)
        {
            var errors = new UserValidator().ValidateUser(user);

            if (errors.Any())
                return new InternalServerErrorResult(errors);

            var userToUpdate = Storage.Users.SingleOrDefault(u => u.Id == user.Id);
            
            if (userToUpdate == null) return Ok();
            
            userToUpdate.Name = user.Name;
            userToUpdate.Username = user.Username;
            userToUpdate.Email = user.Email;

            return Ok();
        }

        [HttpPost, 
        Route("api/secure/udpatepassword"),
        Authorize(Roles = "admin")]
        public string UpdatePassword(PasswordUpdate passwordUpdate)
        {
            return "this is the update password endpoint";
        }

        [HttpPost, 
        Route("api/secure/updaterole"),
        Authorize(Roles = "admin")]
        public string UpdateRole(RoleUpdate roleUpdate)
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

    public class InternalServerErrorResult : ResponseMessageResult
    {
        public InternalServerErrorResult(string error) : base(new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(JsonConvert.SerializeObject(new{error}))}) { }
        public InternalServerErrorResult(IEnumerable<Error> errors) : base(new HttpResponseMessage(HttpStatusCode.InternalServerError){Content = new StringContent(JsonConvert.SerializeObject(errors))}) { }
    }
} ;