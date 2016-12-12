using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using NewcomersNetworkAPI.Models;

namespace NewcomersNetworkAPI.Controllers
{
    [RoutePrefix("api/OptionsLists")]
    public class OptionsListsController : ApiController
    {
        [Route("")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            OptionsLists oOptions = new OptionsLists();
            oOptions.LoadAll();
            return Ok(oOptions);
        }

    }
}
