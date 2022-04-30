using System;
using System.Collections.Generic;
using NewcomersNetworkAPI.Providers;
using System.Data;
using NewcomersNetworkAPI.Exceptions;
using NewcomersNetworkAPI.Models.NNAdmin;
using System.Threading.Tasks;
using System.Web.Http;

namespace NewcomersNetworkAPI.Controllers
{
    [RoutePrefix("api/usersroles")]
    public partial class UsersRolesController : ApiController
    {
        [Route("")]
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IHttpActionResult Get()
        {
            try
            {
                var roles = UsersRolesServices.GetAllRoles();
                return Ok(roles);
            }
            catch(Exception e)
            {
                return BadRequest();
            }
        }
    }
}