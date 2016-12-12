using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using NewcomersNetworkAPI.Models;

[assembly: OwinStartup(typeof(NewcomersNetworkAPI.Startup))]

namespace NewcomersNetworkAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
