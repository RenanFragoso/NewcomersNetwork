using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewcomersNetworkIFACE.Areas.NNAdmin.Models;

namespace NewcomersNetworkIFACE.Areas.NNAdmin.Controllers
{
    public class EventsController : Controller
    {

        // GET: NNAdmin/Events
        public ActionResult Index()
        {
            Events oEvents = new Events(); 
            return View(oEvents);
        }
    }
}