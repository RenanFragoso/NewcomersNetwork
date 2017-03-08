using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NewcomersNetworkAPI.Controllers
{
    [RoutePrefix("api/NNAuth")]
    public class NNAuthController : ApiController
    {
        [HttpPost]
        [Authorize]
        public IHttpActionResult TokenProbe([FromBody] string cUserToken)
        {
            return Ok();
        }

    }
}
