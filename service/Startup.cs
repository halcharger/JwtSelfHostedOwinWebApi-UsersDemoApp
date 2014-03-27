﻿using System.Web.Http;
using Owin;

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
            config.EnsureInitialized();

            appBuilder
                .UseWelcomePage("/test")
                .UseWebApi(config);

        }  
    }
}