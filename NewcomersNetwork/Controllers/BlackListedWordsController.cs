using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewcomersNetworkAPI.Providers;

namespace NewcomersNetworkAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/BlackListedWords")]
    public class BlackListedWordsController : ApiController
    {

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult Get()
        {
            return Ok(BlackListWordChecker.Instance.BlackListedWords);
        }

        [HttpPost]
        public IHttpActionResult Post(string word)
        {
            BlackListWordChecker BLKList = BlackListWordChecker.Instance;
            if (!BLKList.AddWord(word))
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(string word)
        {
            BlackListWordChecker BLKList = BlackListWordChecker.Instance;
            if (!BLKList.DeleteWord(word))
            {
                return BadRequest();
            }
            return Ok();
        }

    }
}
