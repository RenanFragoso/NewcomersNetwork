using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using NewcomersNetworkAPI.Models;

namespace NewcomersNetworkAPI.Controllers
{
    [RoutePrefix("api/nnauth")]
    public class NNAuthController : ApiController
    {
        [HttpGet]
        [Authorize]
        [Route("userinfo")]
        public IHttpActionResult UserInfo()
        {
            var Principal = (ClaimsPrincipal)Request.GetRequestContext().Principal;
            var userId = Principal.Identity.GetUserId();

            var userInfo = new UserInfo(userId);
            if (userInfo.IsValid)
            {
                return Ok(
                    new {
                        userInfo
                    });
            }

            return BadRequest();
        }

    }
}
