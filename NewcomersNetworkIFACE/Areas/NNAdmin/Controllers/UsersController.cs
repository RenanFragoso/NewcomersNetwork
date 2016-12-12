using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewcomersNetworkIFACE.Areas.NNAdmin.Controllers
{
    public class UsersController : Controller
    {
        // GET: NNAdmin/Users
        public ActionResult Index()
        {
            return View();
        }
    }
}