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
            ViewBag.Message = "About Newcomers Network page.";
            base.VerifyCredential();
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Newcomers Network Contact Page";
            base.VerifyCredential();
            return View();
        }

        public ActionResult Legal()
        {
            ViewBag.Message = "Newcomers Network Legal/Privacy";
            base.VerifyCredential();
            return View();
        }

        public ActionResult TermsOfUse()
        {
            ViewBag.Message = "Newcomers Network Terms of Use";
            base.VerifyCredential();
            return View();
        }


    }
}