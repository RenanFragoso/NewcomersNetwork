using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewcomersNetworkAPI.Models.Settings;

namespace NewcomersNetworkAPI.Controllers.Settings
{
    [RoutePrefix("api/Settings/Banners")]
    public class BannersSettingsController : ApiController
    {
        [Route("", Name = "Banners")]
        [HttpGet]
        public IHttpActionResult Get(string startDate = "", string endDate = "")
        {
            DateTime startDt = startDate.Length > 0 ? Convert.ToDateTime(startDate) : DateTime.Now ;
            DateTime endDt = endDate.Length > 0 ? Convert.ToDateTime(endDate) : DateTime.Now.AddYears(10);
            SettingsBanners oBanners = new SettingsBanners(startDt, endDt);
            return Ok(oBanners.oBanners);
        }

    }
}
