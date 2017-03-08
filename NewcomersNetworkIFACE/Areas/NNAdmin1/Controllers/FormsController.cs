using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewcomersNetworkIFACE.Areas.NNAdmin1.Models;
using NewcomersNetworkIFACE.Filters;
using NewcomersNetworkIFACE.Util;

namespace NewcomersNetworkIFACE.Areas.NNAdmin1.Controllers
{
    [NNAuthorize(NNRoles.Administrator, NNRoles.Assistant)]
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