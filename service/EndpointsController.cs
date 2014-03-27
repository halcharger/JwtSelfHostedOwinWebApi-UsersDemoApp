using System.Web.Http;

namespace service
{
    public class EndpointsController : ApiController
    {
        [HttpGet, Route("api/test")]
        public string Test()
        {
            return "this is the test endpoint";
        }

        [HttpPost, Route("api/register")]
        public string Register()
        {
            return "this is the register endpoint";
        }

        [HttpPost, Route("api/authenticate")]
        public string Authenticate()
        {
            return "this is the authenticate endpoint";
        }

        [HttpPost, Route("api/secure/users")]
        public string Users()
        {
            return "this is the users endpoint";
        }

        [HttpPost, Route("api/secure/saveuser")]
        public string SaveUser()
        {
            return "this is the save user endpoint";
        }

        [HttpPost, Route("api/secure/udpatepassword")]
        public string UpdatePassword()
        {
            return "this is the update password endpoint";
        }

        [HttpPost, Route("api/secure/updaterole")]
        public string UpdateRole()
        {
            return "this is the update role endpoint";
        }
    }
}