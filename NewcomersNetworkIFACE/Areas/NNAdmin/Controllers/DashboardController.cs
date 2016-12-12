using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewcomersNetworkIFACE.Areas.NNAdmin.Models;

namespace NewcomersNetworkIFACE.Areas.NNAdmin.Controllers
{
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