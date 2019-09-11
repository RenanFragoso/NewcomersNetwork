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
    [RoutePrefix("api/Events/Details")]
    public class EventDetailsController : ApiController
    {

        [Route("{cEventId}", Name = "GetDetails")]
        [HttpGet]
        public IHttpActionResult Get(string cEventId)      //Gets an event with details
        {
            Event oEvent = new Event(cEventId);
            if (oEvent.isValid)
            {
                if (oEvent.LoadDetails())
                {
                    return Ok(oEvent);
                }
                else
                {
                    return StatusCode(HttpStatusCode.NoContent);
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("Create")]
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IHttpActionResult CreateDetail([FromBody] EventDetails oDetail)      //Creates a detail
        {
            if (oDetail != null && oDetail.Save())
            {
                return CreatedAtRoute("GetDetails", new { nEventID = oDetail.EventId }, oDetail);
            }

            return BadRequest(oDetail.sMsgError.ToString());
        }

        [Route("Update")]
        [HttpPut]
        [Authorize(Roles = "Administrator")]
        public IHttpActionResult UpdateDetail([FromBody]EventDetails oDetail)       //Updates a Detail
        {

            if (oDetail != null && oDetail.Update())
            {
                return CreatedAtRoute("GetDetails", new { nEventID = oDetail.EventId }, oDetail);
            }
            else
            {
                return BadRequest(oDetail.sMsgError.ToString());
            }

        }

        [Route("Delete")]
        [HttpDelete]
        [Authorize(Roles = "Administrator")]
        public IHttpActionResult DeleteEvent([FromBody]EventDetails oDetail)       //Deletes a Service
        {

            if (oDetail != null && oDetail.Delete())
            {
                return Ok();
            }
            else
            {
                return BadRequest(oDetail.sMsgError.ToString());
            }

        }


    }
}
