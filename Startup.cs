using Microsoft.Owin;
using Owin;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

[assembly: OwinStartup(typeof(WebApp.Startup))]

namespace WebApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var hubConfiguration = new HubConfiguration();
            hubConfiguration.EnableDetailedErrors = true;
            app.MapSignalR(hubConfiguration);
        }
    }
}
