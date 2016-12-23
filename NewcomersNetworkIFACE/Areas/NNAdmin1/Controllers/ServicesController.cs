using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewcomersNetworkAPI.Models;
using NewcomersNetworkIFACE.Client;
using NewcomersNetworkIFACE.Areas.NNAdmin1.Models;

namespace NewcomersNetworkIFACE.Areas.NNAdmin1.Controllers
{
    public class ServicesController : Controller
    {

        public Services oServices { get; set; } = new Services();

        // GET: NNAdmin1/Services
        public ActionResult Index()
        {
            //ListPopulate(null);
            return View(oServices);
        }

        public ActionResult ServiceView(string cServiceId)
        {
            Service oService = this.oServices.oServiceList.Find(svc => svc.ServiceId == cServiceId);
            if(oService != null)
            {
                return View(oService);
            }
            else
            {
                return View();
            }
        }

        public ActionResult ServicesGroups()
        {
            ListPopulate(null);
            return PartialView("_ServicesGroupsList");
        }

        protected void ListPopulate(Service oSelectedService)
        {
            ViewBag.oLists = this.oServices.oLists;
            ViewBag.ServicesGroup_List = this.oServices.oLists.getSelectList("ServicesGroup", oSelectedService != null ? oSelectedService.ServiceGroup : null);
        }


    }
}