using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace NewcomersNetworkIFACE
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
                
        protected void Application_EndRequest(object Sender,EventArgs evt)
        {
            Regex r = new Regex(@"(\/Error\/Error)");
            Match m = r.Match(Request.CurrentExecutionFilePath);
            if (!m.Success)
            {
                //Do not test if from Error Controller
                switch (Response.StatusCode)
                {
                    case 400:
                        Response.StatusCode = 200;
                        Response.Redirect("/Error/Error400");
                        break;
                    case 401:
                        Response.StatusCode = 200;
                        Response.Redirect("/Error/Error401");
                        break;
                    case 402:
                        Response.StatusCode = 200;
                        Response.Redirect("/Error/Error402");
                        break;
                    case 403:
                        Response.StatusCode = 200;
                        Response.Redirect("/Error/Error403");
                        break;
                    case 404:
                        Response.StatusCode = 200;
                        Response.Redirect("/Error/Error404");
                        break;
                    case 500:
                        Response.ClearContent();
                        Response.Redirect("/Error/Error500");
                        break;
                    case 501:
                        Response.StatusCode = 200;
                        Response.Redirect("/Error/Error500");
                        break;
                    case 502:
                        Response.StatusCode = 200;
                        Response.Redirect("/Error/Error500");
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
