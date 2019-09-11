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
    [RoutePrefix("api/UsersRoles")]
    [Authorize(Roles = "Administrator")]
    public class UsersRolesController : ApiController
    {
        [Route("")]
        [HttpGet]

        public IHttpActionResult Get()
        {
            DataTable oRolesDB = DBConn.ExecuteCommand("sp_Roles_GetAll", null).Tables[0];    //Gets all Roles
            List<UsersRoles> oRoles = new List<UsersRoles>();

            if (oRolesDB.Rows.Count > 0)
            {
                foreach (DataRow row in oRolesDB.Rows)
                {
                    UsersRoles oUsersRole = new UsersRoles();
                    oUsersRole.MapFromTableRow(row);
                    oRoles.Add(oUsersRole);
                }
            }

            if (oRoles.Count == 0)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            return Ok(oRoles);
        }

        [Route("{cRoleId}", Name = "GetRole")]
        [HttpGet]
        public IHttpActionResult Get(string cRoleId)      //Gets a specific Role
        {
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            infoParameters.Add("cRoleId", cRoleId);
            DataTable oRoleDB = DBConn.ExecuteCommand("sp_User_Get", infoParameters).Tables[0];

            UsersRoles oRole = new UsersRoles();
            if (oRoleDB.Rows.Count > 0)
            {
                oRole.MapFromTableRow(oRoleDB.Rows[0]);
            }

            if (!oRole.Validate())
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            return Ok(oRole);
        }

        [Route("Create")]
        [HttpPost]
        public IHttpActionResult AddService([FromBody]UsersRoles oRole)       //Inserts a Role
        {

            if (oRole != null && oRole.Save())
            {
                return CreatedAtRoute("GetRole", new { cRoleId = oRole.Id }, oRole);
            }
            else
            {
                return BadRequest(oRole.sMsgError.ToString());
            }

        }

        [Route("Update")]
        [HttpPut]
        public IHttpActionResult UpdateEvent([FromBody]UsersRoles oRole)       //Updates a Role
        {

            if (oRole != null && oRole.Update())
            {
                return CreatedAtRoute("GetRole", new { cRoleId = oRole.Id }, oRole);
            }
            else
            {
                return BadRequest(oRole.sMsgError.ToString());
            }

        }

        [Route("Delete")]
        [HttpDelete]
        public IHttpActionResult DeleteEvent([FromBody]UsersRoles oRole)       //Deletes a Role
        {

            if (oRole != null && oRole.Delete())
            {
                return Ok();
            }
            else
            {
                return BadRequest(oRole.sMsgError.ToString());
            }

        }
        
    }
}