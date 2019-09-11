using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NewcomersNetworkAPI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "api/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "NewcomersNetworkAPI.Controllers" }
            );

            routes.MapRoute(
                name: "AdminUI",
                url: "nnadmin/{*url}",
                defaults: new { controller = "Home", action = "NNAdmin" }
            );

            routes.MapRoute(
                name: "UI",
                url: "{*url}",
                defaults: new { controller = "Home", action = "Index" }
            );

        }
    }
}
