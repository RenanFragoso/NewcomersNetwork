using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewcomersNetworkAPI.Models;
using NewcomersNetworkAPI.Providers;
using System.Data;

namespace NewcomersNetworkAPI.Controllers
{
    [RoutePrefix("api/Calendar")]
    public class CalendarController : ApiController
    {

        public CalendarApi oCalendar = new CalendarApi();

        [Route("{dStartDate}/{dEndDate}", Name = "GetAllEvents")]
        [HttpGet]
        public IHttpActionResult GetAllEvents(DateTime dStartDate, DateTime dEndDate)
        {
            /*
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            infoParameters.Add("cServiceId", cServiceId);
            DataTable oServicesDB = DBConn.ExecuteCommand("sp_Services_GetByID", infoParameters).Tables[0];
            //return Ok(oService); */
            return StatusCode(HttpStatusCode.NoContent);
            
        }
        
        [Route("Services/{dStartDate}/{dEndDate}", Name = "GetServices")]
        [HttpGet]
        public IHttpActionResult GetServices(DateTime dStartDate, DateTime dEndDate)
        {
            this.oCalendar.setDates(dStartDate, dEndDate);
            this.oCalendar.LoadServices();
            return Ok(this.oCalendar.oEvents);
        }

        [Route("Events/{dStartDate}/{dEndDate}", Name = "GetEvents")]
        [HttpGet]
        public IHttpActionResult GetEvents(DateTime dStartDate, DateTime dEndDate)
        {
            this.oCalendar.setDates(dStartDate, dEndDate);
            this.oCalendar.LoadServices();
           
            //return Ok(oService);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}