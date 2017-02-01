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
    [RoutePrefix("NNAdmin1/Calendar")]
    public class CalendarController : NNAPIController
    {

        protected Calendar oCalendar = new Calendar();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetEvents(DateTime dStartDate, DateTime dEndDate)
        {
            if (dStartDate != null && dEndDate != null)
            {
                this.oCalendar.LoadAllEvents(dStartDate, dEndDate);
                return Json(new
                {
                    success = true,
                    statuscode = 200,
                    response = "",
                    odata = this.oCalendar.oEvents
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                success = false,
                statuscode = 400,
                response = "{ \"Message\": \"Bad Request.\"}"
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
