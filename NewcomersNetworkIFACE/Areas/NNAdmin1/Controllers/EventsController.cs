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
using System.Net.Http;
using NewcomersNetworkIFACE.Util;
using NewcomersNetworkIFACE.Filters;

namespace NewcomersNetworkIFACE.Areas.NNAdmin1.Controllers
{
    [RoutePrefix("NNAdmin1/Events")]
    public class EventsController : NNAPIController
    {

        public Events oEvents { get; set; } = new Events();

        #region VIEWS

        // GET: NNAdmin/Events
        public ActionResult Index()
        {
            return View();
        }
        /*
        [HttpGet]
        public ActionResult EventsList()
        {
            return PartialView("_EventsList", this.oEvents.oEventList);
        }
        */

        [HttpGet]
        public ActionResult EventView(string cEventId)
        {
            Event oEvent;
            if (cEventId != null && cEventId.Length > 0) { 
                oEvent = this.oEvents.GetEvent(cEventId);
                if (oEvent.Id.Length > 0) {
                    return View(oEvent);
                }
            }
            return RedirectToAction("Index", "Events");
        }
        #endregion
        // =========== EVENT =============
        #region EVENT
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

        [HttpGet]
        public ActionResult GetNewEvent(string cEventId = "")
        {
            Event oEvent = new Event();
            if (cEventId != null && cEventId.Length > 0)
            {
                oEvent.Id = cEventId;
            }
            else
            {
                oEvent.Id = "NULLID";
                oEvent.Name = "New Event";
            }

            return Json(new
            {
                success = true,
                statuscode = 200,
                response = "",
                odata = oEvent
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateEvent(Event oEvent)
        {
            HttpResponseMessage oResponse;
            if (ModelState.IsValid)
            {
                oResponse = this.oEvents.UpdateEvent(oEvent);
                string cMessage = await oResponse.Content.ReadAsStringAsync();
                return Json(new
                {
                    success = oResponse.IsSuccessStatusCode,
                    statuscode = (int)oResponse.StatusCode,
                    response = cMessage
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                success = false,
                statuscode = 400,
                response = "{ \"Message\": \"Bad Request.\"}"
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteEvent(string cEventId)
        {
            HttpResponseMessage oResponse;
            if (ModelState.IsValid)
            {
                oResponse = this.oEvents.DeleteEvent(cEventId);
                string cMessage = await oResponse.Content.ReadAsStringAsync();
                return Json(new
                {
                    success = oResponse.IsSuccessStatusCode,
                    statuscode = (int)oResponse.StatusCode,
                    response = cMessage
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                success = false,
                statuscode = 400,
                response = "{ \"Message\": \"Bad Request.\"}"
            }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public async Task<ActionResult> DeactivateEvent(string cEventId)
        {
            HttpResponseMessage oResponse;
            if (ModelState.IsValid)
            {
                oResponse = this.oEvents.DeactivateEvent(cEventId);
                string cMessage = await oResponse.Content.ReadAsStringAsync();
                return Json(new
                {
                    success = oResponse.IsSuccessStatusCode,
                    statuscode = (int)oResponse.StatusCode,
                    response = cMessage
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                success = false,
                statuscode = 400,
                response = "{ \"Message\": \"Bad Request.\"}"
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> ActivateEvent(string cEventId)
        {
            HttpResponseMessage oResponse;
            if (ModelState.IsValid)
            {
                oResponse = this.oEvents.ActivateEvent(cEventId);
                string cMessage = await oResponse.Content.ReadAsStringAsync();
                return Json(new
                {
                    success = oResponse.IsSuccessStatusCode,
                    statuscode = (int)oResponse.StatusCode,
                    response = cMessage
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                success = false,
                statuscode = 400,
                response = "{ \"Message\": \"Bad Request.\"}"
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}