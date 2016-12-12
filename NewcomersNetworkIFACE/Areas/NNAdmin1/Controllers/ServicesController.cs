using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewcomersNetworkAPI.Models;
using NewcomersNetworkIFACE.Client;

namespace NewcomersNetworkIFACE.Areas.NNAdmin1.Controllers
{
    public class ServicesController : NNAPIController
    {

        public List<Service> oServices { get; protected set; }
        //protected NNAPIClient oNNAPIClient = new NNAPIClient();

        // GET: NNAdmin1/Services
        public ActionResult Index()
        {

            oServices = oNNAPIClient.Get<List<Service>>("/Services");
            return View(oServices);
        }
                
    }
}