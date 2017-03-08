using NewcomersNetworkAPI.Models;
using NewcomersNetworkIFACE.Areas.NNAdmin1.Models;
using NewcomersNetworkIFACE.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;

namespace NewcomersNetworkIFACE.Controllers
{
    public class EventsController : NNAPIController
    {

        protected Events oEvents { get; set; } = new Events();

        #region VIEWS

        public ActionResult Index()
        {
            this.oEvents.LoadEvents();
            ViewBag.Encoder = Encoding.ASCII;
            base.VerifyCredential();
            return View(this.oEvents.oEventList);
        }

        [HttpGet]
        public ActionResult Details(string Id)
        {
            string cRealId = Encoding.ASCII.GetString(Convert.FromBase64String(Id));
            if(cRealId != null && cRealId.Length > 0)
            {
                Event oEvent = this.oEvents.GetEvent(cRealId);
                if(oEvent.Id.Length > 0)
                {
                    base.VerifyCredential();
                    return View(oEvent);
                }
                else
                {
                    return RedirectToAction("Error404", "Error");
                }
            }
            else
            {
                return RedirectToAction("Error404", "Error");
            }
        }

        #endregion

        [HttpGet]
        public ActionResult GetEvents()
        {
            this.oEvents.LoadEvents();
            return Json(new
            {
                success = true,
                statuscode = 200,
                response = "",
                odata = this.oEvents.oEventList
            }, JsonRequestBehavior.AllowGet);
        }


    }
}
