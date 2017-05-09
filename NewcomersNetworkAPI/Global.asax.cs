using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NewcomersNetworkAPI.Providers;
using MultipartDataMediaFormatter;
using MultipartDataMediaFormatter.Infrastructure;

namespace NewcomersNetworkAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
            //Media formatter to send/receive byte arrays
            GlobalConfiguration.Configuration.Formatters.Add(new FormMultipartEncodedMediaTypeFormatter(new MultipartFormatterSettings()));

            //Blob storage Setup
            NNAPIBlobServices.SetConfiguration();

            //Email Sender Initialization
            NNSMTPSender.Init();

        }
    }
}
