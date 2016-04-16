using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartup(typeof(nc.Startup))]

namespace nc
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalHost.Configuration.DisconnectTimeout = TimeSpan.FromMinutes(30);
            GlobalHost.Configuration.KeepAlive = TimeSpan.FromMinutes(10);
            app.MapSignalR();
        }
    }
}