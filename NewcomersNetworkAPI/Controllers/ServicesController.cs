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
    [RoutePrefix("api/Services")]
    public class ServicesController : ApiController
    {

        [Route("")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            DataTable oServicesDB = DBConn.ExecuteCommand("sp_Services_GetAll", null).Tables[0];    //Gets all Services
            List<Service> oServices = new List<Service>();

            if (oServicesDB.Rows.Count > 0)
            {
                foreach (DataRow row in oServicesDB.Rows)
                {
                    Service oService = new Service();
                    oService.MapFromTableRow(row);
                    oServices.Add(oService);
                }
            }

            if (oServices.Count == 0)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            return Ok(oServices);
        }
        
        [Route("{nServiceID:int}", Name = "GetService")]
        [HttpGet]
        public IHttpActionResult Get(int nServiceID)      //Gets a specific event
        {
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            infoParameters.Add("ServiceIDIn", nServiceID);
            DataTable oServices = DBConn.ExecuteCommand("sp_Services_GetByID", infoParameters).Tables[0];

            Service oService = new Service();
            if (oServices.Rows.Count > 0)
            {
                oService.MapFromTableRow(oServices.Rows[0]);
            }

            if (!oService.Validate())
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            return Ok(oService);
        }

        /*
        [Route("Alltodate/{sDate}")]
        [HttpGet]
        public IHttpActionResult GetAllToDate(string sDate, bool isPublishDt = false)     //Gets all the events registered for a specific date
        {

            List<Event> oEvents = new List<Event>();
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            infoParameters.Add("dateToFind", Convert.ToDateTime(sDate));
            DataTable oEventsDB;

            if (isPublishDt)
            {
                oEventsDB = DBConn.ExecuteCommand("sp_Events_GetToPublishDate", infoParameters).Tables[0];
            }
            else
            {
                oEventsDB = DBConn.ExecuteCommand("sp_Events_GetToDate", infoParameters).Tables[0];
            }

            foreach (DataRow row in oEventsDB.Rows)
            {
                Models.Event oEvent = new Event();
                oEvent.MapFromTableRow(row);
                oEvents.Add(oEvent);
            }

            if (oEvents.Count == 0)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            return Ok(oEvents);
        }

        [Route("Publish/{sDate}")]
        [HttpGet]
        public IHttpActionResult GetPublishToDate(string sDate)       //Gets all the published events for a specific date
        {
            return GetAllToDate(sDate, true);
        }
        */

        [Route("Create")]
        [HttpPost]
        public IHttpActionResult AddService([FromBody]Service oService)       //Inserts a Service
        {

            if (oService != null && oService.Save())
            {

                return CreatedAtRoute("GetService", new { nServiceID = oService.ServiceID }, oService);
                //return Ok();
            }
            else
            {
                return BadRequest(oService.sMsgError.ToString());
            }

        }

        [Route("Update")]
        [HttpPut]
        public IHttpActionResult UpdateEvent([FromBody]Service oService)       //Updates a Service
        {

            if (oService != null && oService.Update())
            {
                return CreatedAtRoute("GetService", new { nServiceID = oService.ServiceID }, oService);
            }
            else
            {
                return BadRequest(oService.sMsgError.ToString());
            }

        }

        [Route("Delete")]
        [HttpDelete]
        public IHttpActionResult DeleteEvent([FromBody]Service oService)       //Deletes a Service
        {

            if (oService != null && oService.Delete())
            {
                return Ok();
            }
            else
            {
                return BadRequest(oService.sMsgError.ToString());
            }

        }

    }
}
