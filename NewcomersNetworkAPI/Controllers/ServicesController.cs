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
        //=========== SERVICE ===========
        #region SERVICE

        [Route("")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            DataTable oServicesDB = DBConn.ExecuteCommand("sp_Services_GetAll", null).Tables[0];    //Gets all Services
            List<Service> oServices = new List<Service>();

            if (!oServicesDB.HasErrors && oServicesDB.Rows.Count > 0)
            {
                foreach (DataRow row in oServicesDB.Rows)
                {
                    Service oService = new Service();
                    oService.MapFromTableRow(row);
                    oService.LoadGroup();
                    oServices.Add(oService);
                }
            }

            if (oServices.Count == 0)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            return Ok(oServices);
        }
        
        [Route("{cServiceId}", Name = "GetService")]
        [HttpGet]
        public IHttpActionResult Get(string cServiceId, bool bGetFull = false)      //Gets a specific event
        {
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            infoParameters.Add("cServiceId", cServiceId);
            DataTable oServicesDB = DBConn.ExecuteCommand("sp_Services_GetByID", infoParameters).Tables[0];

            Service oService = new Service();
            if (!oServicesDB.HasErrors && oServicesDB.Rows.Count > 0)
            {
                oService.MapFromTableRow(oServicesDB.Rows[0]);
            }

            if (oService.isValid)
            {
                if (bGetFull)
                {
                    oService.LoadScheduleFull();
                    oService.LoadGroup();
                }

                return Ok(oService);
            }

            return StatusCode(HttpStatusCode.NoContent);
            
        }

        [Route("GetFull/{cServiceId}", Name = "GetFullService")]
        [HttpGet]
        public IHttpActionResult GetFull(string cServiceId)      //Gets a specific event
        {
            return this.Get(cServiceId,true);
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
        public IHttpActionResult CreateService([FromBody]Service oService)       //Inserts a Service
        {

            if (oService != null && oService.Save())
            {

                //return CreatedAtRoute("GetService", new { nServiceID = oService.ServiceId }, oService);
                return Ok();
            }
            else
            {
                return BadRequest(string.Join(",", oService.sMsgError.ToArray()));
            }

        }

        [Route("Update")]
        [HttpPut]
        public IHttpActionResult UpdateService([FromBody]Service oService)       //Updates a Service
        {

            if (oService != null && oService.Update())
            {
                return Ok();
            }
            else
            {
                return BadRequest(string.Join(",", oService.sMsgError.ToArray()));
            }

        }

        [Route("Delete/{cServiceId}")]
        [HttpDelete]
        public IHttpActionResult DeleteService(string cServiceId)
        {
            Service oService = new Service(cServiceId);
            if (oService.isValid && oService.Delete())
            {
                return Ok();
            }
            else
            {
                return BadRequest(string.Join(",", oService.sMsgError.ToArray()));
            }
        }

        [Route("Deactivate/{cServiceId}")]
        [HttpPost]
        public IHttpActionResult DeactivateService(string cServiceId)
        {
            Service oService = new Service(cServiceId);
            if (oService.isValid && oService.Deactivate())
            {
                return Ok();
            }
            else
            {
                return BadRequest(string.Join(",", oService.sMsgError.ToArray()));
            }
        }

        [Route("Activate/{cServiceId}")]
        [HttpPost]
        public IHttpActionResult ActivateService(string cServiceId)
        {
            Service oService = new Service(cServiceId);
            if (oService.isValid && oService.Activate())
            {
                return Ok();
            }
            else
            {
                return BadRequest(string.Join(",", oService.sMsgError.ToArray()));
            }
        }
        #endregion

        //=========== GROUPS ===========
        #region GROUPS

        [Route("Groups/{cActiveOnly?}", Name = "GetGroups")]
        [HttpGet]
        public IHttpActionResult GetGroups(string cActiveOnly = "")
        {
            List<ServiceGroup> oGroups = new List<ServiceGroup>();
            DataTable oServicesDB;

            if (cActiveOnly != null && cActiveOnly.Length > 0)
            {
                oServicesDB = DBConn.ExecuteCommand("sp_ServicesGroup_Get", null).Tables[0];
            }
            else
            {
                oServicesDB = DBConn.ExecuteCommand("sp_ServicesGroup_GetAll", null).Tables[0];
            }

            if (!oServicesDB.HasErrors && oServicesDB.Rows.Count > 0)
            {
                foreach (DataRow row in oServicesDB.Rows)
                {
                    ServiceGroup oGroup = new ServiceGroup();
                    oGroup.MapFromTableRow(row);
                    if (oGroup.isValid)
                    {
                        oGroups.Add(oGroup);
                    }
                }
            }

            return Ok(oGroups);
        }

        [Route("Groups/{cGroupId}", Name = "GetGroup")]
        [HttpGet]
        public IHttpActionResult GetGroup(string cGroupId)
        {
            if (cGroupId != null && cGroupId.Length > 0)
            {
                ServiceGroup oGroup = new ServiceGroup(cGroupId);
                if (oGroup.isValid)
                {
                    return Ok(oGroup);
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("Groups/Create")]
        [HttpPost]
        public IHttpActionResult CreateGroup([FromBody]ServiceGroup oGroup)
        {
            if (oGroup != null && oGroup.Save())
            {
                return CreatedAtRoute("GetGroup", new { cGroupId = oGroup.GroupId }, oGroup);
            }
            return BadRequest(string.Join(",", oGroup.sMsgError.ToArray()));
        }

        [Route("Groups/Update")]
        [HttpPut]
        public IHttpActionResult UpdateGroup([FromBody]ServiceGroup oGroup)
        {
            if (oGroup != null && oGroup.Update())
            {
                return CreatedAtRoute("GetGroup", new { cGroupId = oGroup.GroupId}, oGroup);
            }
            return BadRequest(string.Join(",", oGroup.sMsgError.ToArray()));
        }
        
        [Route("Groups/Delete/{cGroupId}")]
        [HttpDelete]
        public IHttpActionResult DeleteGroup(string cGroupId)
        {
            if (cGroupId != null && cGroupId.Length > 0)
            {
                ServiceGroup oGroup = new ServiceGroup(cGroupId);
                if (oGroup.isValid)
                {
                    if (oGroup.Delete()) {
                        return Ok();
                    }
                }
            }
            return StatusCode(HttpStatusCode.BadRequest);
        }

        [Route("Groups/Deactivate")]
        [HttpPost]
        public IHttpActionResult Deactivate([FromBody]ServiceGroup oGroup)
        {
            if (oGroup != null && oGroup.GroupId.Length > 0)
            {
                ServiceGroup oGroupDeactivate = new ServiceGroup(oGroup.GroupId);
                if (oGroupDeactivate.isValid)
                {
                    if (oGroupDeactivate.Deactivate())
                    {
                        return Ok();
                    }
                }
            }
            return StatusCode(HttpStatusCode.BadRequest);
        }

        [Route("Groups/Activate")]
        [HttpPost]
        public IHttpActionResult Activate([FromBody]ServiceGroup oGroup)
        {
            if (oGroup != null && oGroup.GroupId.Length > 0)
            {
                ServiceGroup oGroupActivate = new ServiceGroup(oGroup.GroupId);
                if (oGroupActivate.isValid)
                {
                    if (oGroupActivate.Activate())
                    {
                        return Ok();
                    }
                }
            }
            return StatusCode(HttpStatusCode.BadRequest);
        }
        #endregion

        //=========== SCHEDULE ===========
        #region SCHEDULE

        [Route("Schedule/Create")]
        [HttpPost]
        public IHttpActionResult CreateSchedule([FromBody]ServicesSchedule oSchedule)
        {
            if (oSchedule != null && oSchedule.Save())
            {
                return Ok();
            }
            return BadRequest(string.Join(",", oSchedule.sMsgError.ToArray()));
        }

        [Route("Schedule/Update")]
        [HttpPut]
        public IHttpActionResult UpdateSchedule([FromBody]ServicesSchedule oSchedule)
        {
            if (oSchedule != null && oSchedule.Update())
            {
                return Ok();
            }
            return BadRequest(string.Join(",", oSchedule.sMsgError.ToArray()));
        }

        [Route("{cServiceId}/Schedule/Delete/{cScheduleId}")]
        [HttpDelete]
        public IHttpActionResult DeleteSchedule(string cServiceId, string cScheduleId)
        {
            ServicesSchedule oSchedule = new ServicesSchedule(cServiceId, cScheduleId);
            if(oSchedule.isValid && oSchedule.Delete())
            {
                return Ok();
            }
            else
            {
                return BadRequest(string.Join(",", oSchedule.sMsgError.ToArray()));
            }
        }

        [Route("{cServiceId}/Schedule/Deactivate/{cScheduleId}")]
        [HttpPost]
        public IHttpActionResult DeactivateSchedule(string cServiceId, string cScheduleId)
        {
            ServicesSchedule oSchedule = new ServicesSchedule(cServiceId, cScheduleId);
            if (oSchedule.isValid && oSchedule.Deactivate())
            {
                return Ok();
            }
            else
            {
                return BadRequest(string.Join(",", oSchedule.sMsgError.ToArray()));
            }
        }

        [Route("{cServiceId}/Schedule/Activate/{cScheduleId}")]
        [HttpPost]
        public IHttpActionResult ActivateSchedule(string cServiceId, string cScheduleId)
        {
            ServicesSchedule oSchedule = new ServicesSchedule(cServiceId, cScheduleId);
            if (oSchedule.isValid && oSchedule.Activate())
            {
                return Ok();
            }
            else
            {
                return BadRequest(string.Join(",", oSchedule.sMsgError.ToArray()));
            }
        }


        #endregion

        //=========== SCHEDULE ITENS ===========
        #region SCHEDULE ITENS

        [Route("ScheduleItens/Create")]
        [HttpPost]
        public IHttpActionResult CreateItem([FromBody]ScheduleItem oItem)
        {
            if (oItem != null && oItem.Save())
            {
                return Ok();
            }
            return BadRequest(string.Join(",", oItem.sMsgError.ToArray()));
        }

        [Route("ScheduleItens/Update")]
        [HttpPut]
        public IHttpActionResult UpdateItem([FromBody]ScheduleItem oItem)
        {
            if (oItem != null && oItem.Update())
            {
                return Ok();
            }
            return BadRequest(oItem.sMsgError.ToString());
        }

        [Route("ScheduleItens/Delete/{cShceduleId}/{cItemId}")]
        [HttpDelete]
        public IHttpActionResult DeleteItem(string cShceduleId, string cItemId)
        {

            int nItemId;
            if ((cShceduleId != null && cShceduleId.Length > 0) && (cItemId != null && cItemId.Length > 0))
            {
                if (Int32.TryParse(cItemId, out nItemId))
                {
                    ScheduleItem oItem = new ScheduleItem(cShceduleId, nItemId);
                    if (oItem.isValid)
                    {
                        if (oItem.Delete())
                        {
                            return Ok();
                        }
                    }
                }
            }

            return StatusCode(HttpStatusCode.BadRequest);
        }

        #endregion

    }
}
