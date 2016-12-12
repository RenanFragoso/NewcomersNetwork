using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewcomersNetworkAPI.Models;
using NewcomersNetworkAPI.Providers;
using System.Data;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace NewcomersNetworkAPI.Controllers
{
    [RoutePrefix("api/Users")]
    public class UsersController : ApiController
    {
        [Route("")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            DataTable oUsersDB = DBConn.ExecuteCommand("sp_User_GetAll", null).Tables[0];    //Gets all Users
            List<User> oUsers = new List<User>();

            if (oUsersDB.Rows.Count > 0)
            {
                foreach (DataRow row in oUsersDB.Rows)
                {
                    User oUser = new User();
                    oUser.MapFromTableRow(row);
                    oUser.getDetails();
                    oUsers.Add(oUser);
                }
            }

            if (oUsers.Count == 0)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            return Ok(oUsers);
        }

        [Route("{cUserId}", Name = "GetUser")]
        [HttpGet]
        public IHttpActionResult Get(string cUserId)      //Gets a specific User
        {
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            infoParameters.Add("cUserId", cUserId);
            DataTable oUserDB = DBConn.ExecuteCommand("sp_User_Get", infoParameters).Tables[0];

            User oUser = new User();
            if (oUserDB.Rows.Count > 0)
            {
                oUser.MapFromTableRow(oUserDB.Rows[0]);
            }

            if (!oUser.Validate())
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            return Ok(oUser);
        }
        
        [Route("Create")]
        [HttpPost]
        public IHttpActionResult AddUser([FromBody]User oUser)       //Inserts an User
        {
            if (oUser != null && oUser.Save())
            {
                return CreatedAtRoute("GetUser", new { cUserId = oUser.Id }, oUser);
            }
            else
            {
                return BadRequest(string.Join(",", oUser.sMsgError.ToArray()));
            }

        }

        [Route("Activate/{cUserId}", Name = "ActivateUser")]
        [HttpPost]
        public IHttpActionResult ActivateUser(string cUserId)       //Activates an User
        {
            User oUser;

            if (cUserId != null && cUserId.Length > 0)
            {

                oUser = new User(cUserId);

                if (oUser != null && oUser.isValid && oUser.Activate())
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(string.Join(",", oUser.sMsgError.ToArray()));
                }

            }

            return BadRequest("Malformed request.");
        }

        [Route("Block/{cUserId}", Name = "BlockUser")]
        [HttpPost]
        public IHttpActionResult BlockUser(string cUserId)       //Activates an User
        {
            User oUser;

            if (cUserId != null && cUserId.Length > 0)
            {

                oUser = new User(cUserId);

                if (oUser != null && oUser.isValid && oUser.Block())
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(string.Join(",", oUser.sMsgError.ToArray()));
                }

            }

            return BadRequest("Malformed request.");
        }


        [Route("Update")]
        [HttpPut]
        public IHttpActionResult UpdateUser([FromBody]User oUser)       //Updates an User
        {
            if (oUser != null && oUser.Update())
            {
                return CreatedAtRoute("GetUser", new { cUserId = oUser.Id }, oUser);
            }
            else
            {
                return BadRequest(oUser.sMsgError.ToString());
            }

        }

        [Route("Delete/{cUserId}", Name = "DeleteUser")]
        [HttpDelete]
        public IHttpActionResult DeleteUser(string cUserId)       //Deletes an User
        {
            User oUser;

            if (cUserId != null && cUserId.Length > 0)
            {

                oUser = new User(cUserId);

                if (oUser != null && oUser.isValid && oUser.Delete())
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(string.Join(",", oUser.sMsgError.ToArray()));
                }

            }

            return BadRequest("Malformed request.");

        }


        [Route("GetBatch", Name = "GetBatch")]
        [HttpPost]
        public IHttpActionResult GetBatch([FromBody] List<string> oUserIds)
        {

            List<User> oUsers = new List<User>();
            User oUser;

            if (oUserIds != null)
            {

                foreach (string cUserId in oUserIds)
                {

                    if(cUserId != null && cUserId.Length > 0)
                    {
                        oUser = new User(cUserId);
                        oUser.getDetails();
                        oUsers.Add(oUser);
                    }

                }

                return Ok(oUsers);

            }

            return BadRequest("Malformed request.");
        }


    }

}