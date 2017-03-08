using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewcomersNetworkAPI.Models;
using NewcomersNetworkAPI.Providers;
using System.Data;
using System.Web.Http.ModelBinding;

namespace NewcomersNetworkAPI.Controllers
{
    [RoutePrefix("api/Events")]
    public class EventsController : ApiController
    {
        [Route("")]
        [HttpGet]
        public IHttpActionResult Get(bool getFull = false)
        {
            DataTable oEventsDB = DBConn.ExecuteCommand("sp_Events_GetAll", null).Tables[0];    //Gets all the events
            List<Event> oEvents = new List<Event>();

            Event oEvent;
            //EventDetails oDetails;

            if (oEventsDB.Rows.Count > 0)
            {
                foreach (DataRow row in oEventsDB.Rows)
                {
                    oEvent = new Event();
                    oEvent.MapFromTableRow(row);

                    if (oEvent.isValid)
                    {
                        /*
                        if (getFull)
                        {
                            oDetails = new EventDetails(oEvent.Id);
                            if (oDetails.isValid)
                            {
                                oEvent.oDetails = oDetails;
                            }
                        }
                        */
                        oEvents.Add(oEvent);
                    }
                }
            }

            if(oEvents.Count == 0)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            return Ok(oEvents);
        }

        [Route("{cEventId}", Name = "GetEvent")]
        [HttpGet]
        public IHttpActionResult Get(string cEventId, bool getFull = false)      //Gets a specific event
        {

            Event oEvent = new Event(cEventId);
            //EventDetails oDetails;

            if (!oEvent.isValid)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            
            /*
            if (getFull)
            {
                oDetails = new EventDetails(oEvent.Id);
                if (oDetails.isValid)
                {
                    oEvent.oDetails = oDetails;
                }
            }
            */
            return Ok(oEvent);
        }

        [Route("All")]
        [HttpGet]
        public IHttpActionResult GetAllFull()
        {
            return Get(true);
        }

        [Route("All/{cEventID}")]
        [HttpGet]
        public IHttpActionResult GetFull(string cEventId)
        {
            return Get(cEventId, true);
        }


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
                Event oEvent = new Event();
                oEvent.MapFromTableRow(row);
                if (oEvent.isValid)
                {
                    oEvents.Add(oEvent);
                }
            }

            if(oEvents.Count == 0)
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

        [Route("Create")]
        [HttpPost]
        public IHttpActionResult CreateEvent([FromBody]Event oEvent)       //Inserts an event
        {

            if (oEvent != null && oEvent.Save())
            {

                //return CreatedAtRoute("GetEvent", new { cEventId = oEvent.Id }, oEvent);
                return Ok();
            }
            else
            {
                return BadRequest(string.Join(",", oEvent.sMsgError.ToArray()));
            }

        }

        [Route("Update")]
        [HttpPut]
        public IHttpActionResult UpdateEvent([FromBody]Event oEvent)       //Updates an event
        {

            if (oEvent != null && oEvent.Update())
            {
                //return CreatedAtRoute("GetEvent", new { cEventId = oEvent.Id }, oEvent);
                return Ok();
            }
            else
            {
                return BadRequest(string.Join(",", oEvent.sMsgError.ToArray()));
            }

        }

        [Route("Delete/{cEventId}")]
        [HttpDelete]
        public IHttpActionResult DeleteEvent(string cEventId)
        {
            Event oEvent = new Event(cEventId);
            if (oEvent.isValid && oEvent.Delete())
            {
                return Ok();
            }
            else
            {
                return BadRequest(string.Join(",", oEvent.sMsgError.ToArray()));
            }
        }

        [Route("Activate/{cEventId}")]
        [HttpPost]
        public IHttpActionResult ActivateEvent(string cEventId)
        {
            Event oEvent = new Event(cEventId);
            if (oEvent.isValid && oEvent.Activate())
            {
                return Ok();
            }
            else
            {
                return BadRequest(string.Join(",", oEvent.sMsgError.ToArray()));
            }
        }

        [Route("Deactivate/{cEventId}")]
        [HttpPost]
        public IHttpActionResult DeactivateService(string cEventId)
        {
            Event oEvent = new Event(cEventId);
            if (oEvent.isValid && oEvent.Deactivate())
            {
                return Ok();
            }
            else
            {
                return BadRequest(string.Join(",", oEvent.sMsgError.ToArray()));
            }
        }

        /*
        [Route("Registrations/{cEventId}", Name = "GetRegistrations")]
        [HttpGet]
        public IHttpActionResult GetRegistrations(string cEventId)
        {
            List<EventRegistration> oRegistrations = new List<EventRegistration>();
            EventRegistration oRegistration;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            DataTable oRegisterDB;

            if (cEventId != null && cEventId.Length > 0)
            {
                infoParameters.Add("cEventId", cEventId);
                oRegisterDB = DBConn.ExecuteCommand("sp_Events_GetRegistrations", infoParameters).Tables[0];

                if (!oRegisterDB.HasErrors && oRegisterDB.Rows.Count > 0)
                {
                    foreach (DataRow row in oRegisterDB.Rows)
                    {
                        oRegistration = new EventRegistration();
                        oRegistration.MapFromTableRow(row);

                        if (oRegistration.isValid)
                        {
                            oRegistration.LoadUser();
                            oRegistrations.Add(oRegistration);
                        }
                    }
                    return Ok(oRegistrations);
                }

                return BadRequest(oRegisterDB.GetErrors().ToString());
            }

            return BadRequest("Invalid Event ID.");

        }
        */

    }
}
