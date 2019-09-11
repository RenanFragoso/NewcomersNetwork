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
    [RoutePrefix("api/values")]
    public class ValuesController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            OptionsLists oOptions = new OptionsLists();
            oOptions.LoadAll();
            return Ok(oOptions);
        }

        [Route("list/{listName}")]
        [HttpGet]
        public IHttpActionResult GetListByName(string listName)
        {
            OptionsLists Options = new OptionsLists(listName);
            if (Options.isValid)
            {
                return Ok(Options.ValueLists);
            }
            return BadRequest(string.Join(",", Options.sMsgError.ToArray()));
        }
        
        
        [Route("group/{groupName}")]
        [HttpGet]
        public IHttpActionResult GetListByGroup(string groupName)
        {
            OptionsLists Options = new OptionsLists();
            Options.LoadListGroup(groupName);
            return Ok(Options.ValueLists);
        }
    }
}
