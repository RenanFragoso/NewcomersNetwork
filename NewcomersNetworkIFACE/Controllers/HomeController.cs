using NewcomersNetworkIFACE.Client;
using NewcomersNetworkIFACE.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewcomersNetworkIFACE.Controllers
{
    [NNLoginPersistence]
    public class HomeController : NNAPIController
    {
        public ActionResult Index()
        {
            base.VerifyCredential();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            base.VerifyCredential();
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            base.VerifyCredential();
            return View();
        }
    }
}