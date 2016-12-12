using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewcomersNetworkIFACE.Areas.NNAdmin1.Models;
using NewcomersNetworkIFACE.Client;
using NewcomersNetworkAPI.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NewcomersNetworkIFACE.Areas.NNAdmin1.Controllers
{
    [RoutePrefix("NNAdmin1/Events")]
    public class EventsController : NNAPIController
    {

        public Events oEvents { get; set; } = new Events();

        // GET: NNAdmin/Events
        public ActionResult Index()
        {
            
            return View(oEvents);
        }

        [HttpGet]
        public ActionResult EventsList()
        {
            return PartialView("_EventsList", this.oEvents.oEventList);
        }

        [Route("EditEventView/{cEventId}")]
        [HttpGet]
        public ActionResult EditEventView(string cEventId)
        {
            Event oNewEvent = new Event { Id = "NULLID" };
            var oEventEdit = oNewEvent;

            if (cEventId != null && cEventId.Length > 0)
            {
                //Edit user
                oEventEdit = this.oEvents.oEventList.Find( evt => evt.Id == cEventId);

                if (oEventEdit == null)
                {
                    oEventEdit = oNewEvent;
                }

            }
            return PartialView("_EditEventForm", oEventEdit);
        }

        [Route("EventRegistrationList/{cEventId}")]
        [HttpGet]
        public ActionResult EventRegistrationList(string cEventId)
        {
            List<EventRegistration> oRegistration;
            List<User> oUsers = new List<User>();
            List<string> oIds = new List<string>();
          
            //ViewBag.oEvent = this.oEvents.oEventList.Find(e => e.Id == cEventId);

            if (cEventId != null && cEventId.Length > 0)
            {
                oRegistration = oEvents.getRegistrations(cEventId);
                //return PartialView("_EventRegistrationList", oRegistration);
                return Json(oRegistration, JsonRequestBehavior.AllowGet);
            }

            return Json(new List<EventRegistration>(), JsonRequestBehavior.AllowGet);
        }


    }
}