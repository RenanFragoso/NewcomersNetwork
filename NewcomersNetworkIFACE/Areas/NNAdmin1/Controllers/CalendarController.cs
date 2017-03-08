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
using NewcomersNetworkIFACE.Filters;
using NewcomersNetworkIFACE.Util;

namespace NewcomersNetworkIFACE.Areas.NNAdmin1.Controllers
{
    [NNAuthorize(NNRoles.Administrator, NNRoles.Assistant)]
    [RoutePrefix("NNAdmin1/Calendar")]
    public class CalendarController : NNAPIController
    {
        protected Calendar oCalendar = new Calendar();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetEvents(long start, long end)
        {
            DateTime dStartDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime dEndDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            dStartDate = dStartDate.AddSeconds(start);
            dEndDate = dEndDate.AddSeconds(end);
            //this.oCalendar.LoadServices(dStartDate, dEndDate);
            this.oCalendar.LoadAllEvents(dStartDate, dEndDate);
            //this.oCalendar.LoadServices(start, end);
            return Json(new
            {
                success = true,
                statuscode = 200,
                response = "",
                odata = this.oCalendar.oEvents
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetNewEvent()
        {
            CalendarEvent oEvent = new CalendarEvent();
            return Json(new
            {
                success = true,
                statuscode = 200,
                response = "",
                odata = oEvent
            }, JsonRequestBehavior.AllowGet);
        }

    }
}
