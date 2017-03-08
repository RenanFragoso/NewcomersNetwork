using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace NewcomersNetworkIFACE.Controllers
{
    public class ErrorController : Controller
    {

        public ActionResult Index()
        {
            Response.StatusCode = 500;
            return View();
        }
        public ActionResult Error401()
        {
            Response.StatusCode = 401;
            return View("401");
        }
        public ActionResult Error402()
        {
            Response.StatusCode = 402;
            return View("402");
        }
        public ActionResult Error403()
        {
            Response.StatusCode = 403;
            return View("403");
        }
        public ActionResult Error404()
        {
            Response.StatusCode = 404;
            return View("404");
        }
        public ActionResult Error500()
        {
            Response.StatusCode = 500;
            return View("500");
        }

    }
}
