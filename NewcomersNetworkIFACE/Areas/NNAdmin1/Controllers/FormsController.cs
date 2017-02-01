using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewcomersNetworkIFACE.Areas.NNAdmin1.Models;

namespace NewcomersNetworkIFACE.Areas.NNAdmin1.Controllers
{
    public class FormsController : Controller
    {
        // GET: NNAdmin/Dashboard
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewcomersForm()
        {
            return View();
        }

    }
}