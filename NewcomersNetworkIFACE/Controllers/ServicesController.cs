using NewcomersNetworkIFACE.Areas.NNAdmin1.Models;
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
    public class ServicesController : NNAPIController
    {

        protected Services oServices = new Services();

        public ActionResult Index()
        {
            oServices.LoadServices();
            base.VerifyCredential();
            return View(oServices.oServiceList);
        }
    }
}