using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewcomersNetworkAPI.Models;
using NewcomersNetworkAPI.Models.Settings;
using NewcomersNetworkAPI.Models.Widgets;
using NewcomersNetworkAPI.Providers;
using System.Data;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using System.Configuration;
using NewcomersNetworkAPI.Exceptions;

namespace NewcomersNetworkAPI.Controllers
{
    [RoutePrefix("api/widgets")]
    public class WidgetsController : ApiController
    {

        [Route("banners", Name = "GetBannersWidget")]
        [HttpGet]
        public IHttpActionResult GetBannersWidget()
        {
            var banners = new List<NNBanner>();
            DataTable bannersQry = DBConn.ExecuteCommand(   "sp_Widgets_Banners_Get", 
                                                            new Dictionary<string, object> {    { "startDate", DateTime.Now },
                                                                                                { "endDate", DateTime.Now } }).Tables[0];
            foreach(var banner in bannersQry.AsEnumerable())
            {
                banners.Add(new NNBanner(banner["Id"].ToString()));
            }

            return Ok(
                new {
                    banners
                });
        }

        [Route("service-blocks", Name = "GetServiceBlocksWidget")]
        [HttpGet]
        public IHttpActionResult GetServiceBlocksWidget()
        {
            var serviceBlocks = new List<WidgetServiceBlock>();
            //DataTable servicesQry = DBConn.ExecuteCommand("sp_ServicesGroup_Get").Tables[0];
            DataTable servicesQry = DBConn.ExecuteCommand("sp_Widgets_ServiceBlocks").Tables[0];

            foreach (DataRow service in servicesQry.Rows)
            {
                serviceBlocks.Add(
                    new WidgetServiceBlock( service["ServiceGroupId"].ToString(),
                                            0,
                                            service["BlockColor"].ToString(),
                                            service["Text"].ToString(),
                                            service["Icon"].ToString())
                );
            }

            return Ok(
                new
                {
                    serviceBlocks
                });
        }

        [Route("justshare", Name = "GetJustShareWidget")]
        [HttpGet]
        public IHttpActionResult GetJustShareWidget()
        {
            var categories = new Dictionary<string, JustShareCategory>();
            DataSet jsCatQry = DBConn.ExecuteCommand("sp_Widgets_JustShareCat_Get");
            foreach(DataRow cat in jsCatQry.Tables[0].AsEnumerable())
            {
                categories.Add(cat["Id"].ToString(), new JustShareCategory(cat["Id"].ToString(), cat["GroupName"].ToString()));
            }

            foreach (DataRow jsItem in jsCatQry.Tables[1].AsEnumerable())
            {
                JustShareCategory category;
                try
                {
                    categories.TryGetValue(jsItem["CatId"].ToString(), out category);
                    if(category != null)
                    {
                        category.AddWidget(new JustShareClassifiedWidget {
                            Id = jsItem["JsId"].ToString(),
                            Title = jsItem["Title"].ToString()
                        });
                    }
                }
                catch(Exception ex)
                {
                    continue;
                }
            }

            List<JustShareCategory> catList = new List<JustShareCategory>();
            foreach(var catItm in categories)
            {
                catList.Add(catItm.Value);
            }
            var justShare = new WidgetJustShare()
            {
                Categories = catList
            };

            return Ok(
                new
                {
                    justShare
                });
        }

        [Route("services", Name = "GetServicesWidget")]
        [HttpGet]
        public IHttpActionResult GetServicesWidget()
        {
            var services = new List<WidgetServices>
            {
                new WidgetServices("ESL Class 1", "English as a second language", ConfigurationManager.AppSettings["NNWebsiteURL"].ToString() + "/Content/images/square-image.png", "#"),
                new WidgetServices("ESL Class 2", "English as a second language", ConfigurationManager.AppSettings["NNWebsiteURL"].ToString() + "/Content/images/square-image.png", "#"),
                new WidgetServices("ESL Class 3", "English as a second language", ConfigurationManager.AppSettings["NNWebsiteURL"].ToString() + "/Content/images/square-image.png", "#"),
                new WidgetServices("ESL Class 4", "English as a second language", ConfigurationManager.AppSettings["NNWebsiteURL"].ToString() + "/Content/images/square-image.png", "#"),
                new WidgetServices("ESL Class 5", "English as a second language", ConfigurationManager.AppSettings["NNWebsiteURL"].ToString() + "/Content/images/square-image.png", "#"),
                new WidgetServices("ESL Class 6", "English as a second language", ConfigurationManager.AppSettings["NNWebsiteURL"].ToString() + "/Content/images/square-image.png", "#"),
                new WidgetServices("ESL Class 7", "English as a second language", ConfigurationManager.AppSettings["NNWebsiteURL"].ToString() + "/Content/images/square-image.png", "#")
            };

            return Ok(
                new
                {
                    services
                });
        }

        [Route("events", Name = "GetEventsWidget")]
        [HttpGet]
        public IHttpActionResult GetEventsWidget()
        {
            try
            {
                var events = EventsService.GetWidgetEvents(DateTime.Today, DateTime.Today.AddDays(1));
                return Ok(new { events });
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [Route("testimonials", Name = "GetTestimonialsWidget")]
        [HttpGet]
        public IHttpActionResult GetTestimonialsWidget()
        {
            var testimonials = new List<WidgetTestimonials>
            {
                new WidgetTestimonials("Pr. Nestor Abdon", "Newcomers Network Pastor and Organizer", ConfigurationManager.AppSettings["NNWebsiteURL"].ToString() + "/Content/images/square-image.png", "Incidunt deleniti blanditiis quas aperiam recusandae consequatur ullam quibusdam cum libero illo rerum repellendus!", "#"),
                new WidgetTestimonials("Muhamed Ahmed", "Sirian Refugee", ConfigurationManager.AppSettings["NNWebsiteURL"].ToString() + "/Content/images/square-image.png", "Natus voluptatum enim quod necessitatibus quis expedita harum provident eos obcaecati id culpa corporis molestias.","#"),
                new WidgetTestimonials("Dr. Norman Last Name", "Volunteer Doctor", ConfigurationManager.AppSettings["NNWebsiteURL"].ToString() + "/Content/images/square-image.png", "Fugit officia dolor sed harum excepturi ex iusto magnam asperiores molestiae qui natus obcaecati facere sint amet.", "#"),
                new WidgetTestimonials("User Name", "User Title", ConfigurationManager.AppSettings["NNWebsiteURL"].ToString() + "/Content/images/square-image.png", "Similique fugit repellendus expedita excepturi iure perferendis provident quia eaque. Repellendus, vero numquam?", "#"),
                new WidgetTestimonials("User Name", "User Title", ConfigurationManager.AppSettings["NNWebsiteURL"].ToString() + "/Content/images/square-image.png", "Similique fugit repellendus expedita excepturi iure perferendis provident quia eaque. Repellendus, vero numquam?", "#"),
            };

            return Ok(
                new
                {
                    testimonials
                });
        }

        [Route("userjustshare/{page:int}/{offset:int}", Name = "GetUserJustShareWidget")]
        [HttpGet]
        [Authorize]
        public IHttpActionResult GetUserJustShareWidget(int page, int offset)
        {
            ClaimsPrincipal claimsPrincipal = (ClaimsPrincipal)Request.GetRequestContext().Principal;

            page = (page == 0 ? 1 : page);
            offset = (offset == 0 ? 10 : offset);

            try
            {
                var widgetUserJustShare = new WidgetUserJustShare(claimsPrincipal.Identity.GetUserId(), page, offset);
                widgetUserJustShare.ValidOrBreak();
                return Ok(widgetUserJustShare);
            }
            catch (InvalidModelException e)
            {
                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}