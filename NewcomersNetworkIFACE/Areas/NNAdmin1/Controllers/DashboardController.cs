using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewcomersNetworkIFACE.Areas.NNAdmin1.Models;
using NewcomersNetworkIFACE.Util;
using NewcomersNetworkIFACE.Filters;

namespace NewcomersNetworkIFACE.Areas.NNAdmin1.Controllers
{
    [NNLoginPersistence]
    [NNAuthorize(NNRoles.Administrator, NNRoles.Assistant)]
    public class DashboardController : Controller
    {
        // GET: NNAdmin/Dashboard
        public ActionResult Index()
        {
            DashBoard oDashboard = new DashBoard();   
            return View( oDashboard );
        }
    }
}