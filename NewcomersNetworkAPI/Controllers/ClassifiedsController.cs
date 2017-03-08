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

    [RoutePrefix("api/Classifieds")]
    public class ClassifiedsController : ApiController
    {
        public IHttpActionResult Get()
        {
            DataTable oClassDB = DBConn.ExecuteCommand("sp_Classifieds_GetAll", null).Tables[0];
            List<Classified> oClassifieds = new List<Classified>();

            Classified oClassified;

            if (oClassDB.Rows.Count > 0)
            {
                foreach (DataRow row in oClassDB.Rows)
                {
                    oClassified = new Classified();
                    oClassified.MapFromTableRow(row);

                    if (oClassified.isValid)
                    {
                        oClassifieds.Add(oClassified);
                    }
                }
            }
            return Ok(oClassifieds);
        }

        [Route("{cClassifiedId}")]
        public IHttpActionResult GetClassified(string cClassifiedId)
        {
            List<Classified> oClassifieds = new List<Classified>();
            Classified oClassified = new Classified(cClassifiedId);

            if (oClassified.isValid)
            {
                oClassifieds.Add(oClassified);
            }

            return Ok(oClassifieds);
        }

        [Route("GetFiltered")]
        [HttpPost]
        public IHttpActionResult GetFiltered([FromBody] NNClassifiedsFilter oFilter)
        {
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            List<Classified> oClassifieds = new List<Classified>();
            Classified oClassified;

            infoParameters.Add("nPage", oFilter.nPage);
            infoParameters.Add("nPageSize", oFilter.nPageSize);
            infoParameters.Add("cCategory", oFilter.aCategory);
            infoParameters.Add("cType", string.Join("|",oFilter.cType));
            infoParameters.Add("cWord", oFilter.cWord);
            DataTable oClassDB = DBConn.ExecuteCommand("sp_Classifieds_GetFiltered", null).Tables[0];

            if (oClassDB.Rows.Count > 0)
            {
                foreach (DataRow row in oClassDB.Rows)
                {
                    oClassified = new Classified();
                    oClassified.MapFromTableRow(row);

                    if (oClassified.isValid)
                    {
                        oClassifieds.Add(oClassified);
                    }
                }
            }
            return Ok(oClassifieds);
        }

        [Route("Pending")]
        [HttpGet]
        public IHttpActionResult GetPending()
        {
            DataTable oClassDB = DBConn.ExecuteCommand("sp_Classifieds_Pending", null).Tables[0];
            List<Classified> oClassifieds = new List<Classified>();

            Classified oClassified;

            if (oClassDB.Rows.Count > 0)
            {
                foreach (DataRow row in oClassDB.Rows)
                {
                    oClassified = new Classified();
                    oClassified.MapFromTableRow(row);

                    if (oClassified.isValid)
                    {
                        oClassifieds.Add(oClassified);
                    }
                }
            }
            return Ok(oClassifieds);
        }

        [Route("PendingNum")]
        [HttpGet]
        public IHttpActionResult GetPendingNum()
        {
            return Ok(Classified.GetPendingNum());
        }
        
        [Route("Create")]
        [HttpPost]
        public IHttpActionResult CreateClassified([FromBody]Classified oClassified)
        {

            if (oClassified != null && oClassified.Save())
            {
                return Ok();
            }
            else
            {
                return BadRequest(string.Join(",", oClassified.sMsgError.ToArray()));
            }

        }

        [Route("Update")]
        [HttpPut]
        public IHttpActionResult UpdateService([FromBody]Classified oClassified)
        {

            if (oClassified != null && oClassified.Update())
            {
                return Ok();
            }
            else
            {
                return BadRequest(string.Join(",", oClassified.sMsgError.ToArray()));
            }

        }

        [Route("Approve/{cClassifiedId}")]
        [HttpPost]
        public IHttpActionResult ApproveClassified(string cClassifiedId)
        {
            Classified oCLassified = new Classified(cClassifiedId);
            if (oCLassified.isValid && oCLassified.Approve())
            {
                return Ok();
            }
            else
            {
                return BadRequest(string.Join(",", oCLassified.sMsgError.ToArray()));
            }
        }

        [Route("Reject/{cClassifiedId}")]
        [HttpPost]
        public IHttpActionResult RejectClassified(string cClassifiedId)
        {
            Classified oCLassified = new Classified(cClassifiedId);
            if (oCLassified.isValid && oCLassified.Reject())
            {
                return Ok();
            }
            else
            {
                return BadRequest(string.Join(",", oCLassified.sMsgError.ToArray()));
            }
        }



    }
}
