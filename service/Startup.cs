using System.Web.Http;
using Microsoft.Owin.Cors;
using Owin;
using service.Handlers;

namespace service
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.MessageHandlers.Add(new JwtAuthenticationMessageHandler());
            config.EnsureInitialized();

            appBuilder
                .UseCors(CorsOptions.AllowAll)
                .UseWelcomePage("/test")
                .UseWebApi(config);

        }  
    }
}