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

        [Route("{cListName}")]
        [HttpGet]
        public IHttpActionResult GetList(string cListName)
        {
            OptionsLists oOptions = new OptionsLists(cListName);
            if (oOptions.isValid)
            {
                return Ok(oOptions);
            }
            return BadRequest(string.Join(",", oOptions.sMsgError.ToArray()));
        }
        
        
        [Route("Group/{cGroupName}")]
        [HttpGet]
        public IHttpActionResult GetListGroup(string cGroupName)
        {
            OptionsLists oOptions = new OptionsLists();
            oOptions.LoadListGroup(cGroupName);
            return Ok(oOptions);
        }
    }
}
