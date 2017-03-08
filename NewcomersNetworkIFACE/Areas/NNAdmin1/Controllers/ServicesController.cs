using System.Collections.Generic;
using System.Web.Mvc;
using NewcomersNetworkIFACE.Areas.NNAdmin1.Models;
using NewcomersNetworkIFACE.Client;
using NewcomersNetworkAPI.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace NewcomersNetworkIFACE.Areas.NNAdmin1.Controllers
{
    [RoutePrefix("NNAdmin1/Services")]
    public class ServicesController : NNAPIController
    {

        public Services oServices { get; set; } = new Services();

        // =========== VIEWS =============
        #region VIEWS

        // GET: NNAdmin1/Services
        public ActionResult Index()
        {
            //ListPopulate(null);
            //this.oServices.LoadServices();
            //return View(oServices);
            return View();
        }

        public ActionResult ServiceView(string cServiceId)
        {
            if (cServiceId != null && cServiceId.Length > 0)
            {
                this.oServices.LoadServices();
                Service oService = this.oServices.getService(cServiceId);
                if (oService.ServiceId.Length > 0)
                {
                    return View(oService);
                }
            }
            return RedirectToAction("Index", "Services");
        }

        public ActionResult ServicesGroups()
        {
            this.oServices.LoadServices();
            ListPopulate(null);
            return PartialView("_ServicesGroupsList");
        }
        #endregion

        // =========== SERVICES =============
        #region SERVICES

        [HttpGet]
        public ActionResult GetServices()
        {
            this.oServices.LoadServices();
            return Json(this.oServices.oServiceList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetService(string cServiceId)
        {
            Service oService = this.oServices.LoadService(cServiceId,false); //Do not load full service
            return Json(oService, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetNewService(string cServiceId = "")
        {
            Service oService = new Service();
            if (cServiceId.Length == 0)
            {
                oService.ServiceId = "NULLID";
                oService.ServiceName = "New Service";
            }
            else
            {
                oService.ServiceId = cServiceId;
            }
            return Json(new
            {
                success = true,
                statuscode = 200,
                response = "",
                odata = oService
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateService(Service oService)
        {
            HttpResponseMessage oResponse;
            if (ModelState.IsValid)
            {
                oResponse = this.oServices.UpdateService(oService);
                string cMessage = await oResponse.Content.ReadAsStringAsync();
                return Json(new
                {
                    success = oResponse.IsSuccessStatusCode,
                    statuscode = (int)oResponse.StatusCode,
                    response = cMessage
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                success = false,
                statuscode = 400,
                response = "{ \"Message\": \"Bad Request.\"}"
            }, JsonRequestBehavior.AllowGet);

        }

        [HttpDelete]
        public async Task<ActionResult> DeleteService(string cServiceId)
        {
            HttpResponseMessage oResponse;
            if (ModelState.IsValid)
            {
                oResponse = this.oServices.DeleteService(cServiceId);
                string cMessage = await oResponse.Content.ReadAsStringAsync();
                return Json(new
                {
                    success = oResponse.IsSuccessStatusCode,
                    statuscode = (int)oResponse.StatusCode,
                    response = cMessage
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                success = false,
                statuscode = 400,
                response = "{ \"Message\": \"Bad Request.\"}"
            }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public async Task<ActionResult> DeactivateService(string cServiceId)
        {
            HttpResponseMessage oResponse;
            if (ModelState.IsValid)
            {
                oResponse = this.oServices.DeactivateService(cServiceId);
                string cMessage = await oResponse.Content.ReadAsStringAsync();
                return Json(new
                {
                    success = oResponse.IsSuccessStatusCode,
                    statuscode = (int)oResponse.StatusCode,
                    response = cMessage
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                success = false,
                statuscode = 400,
                response = "{ \"Message\": \"Bad Request.\"}"
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> ActivateService(string cServiceId)
        {
            HttpResponseMessage oResponse;
            if (ModelState.IsValid)
            {
                oResponse = this.oServices.ActivateService(cServiceId);
                string cMessage = await oResponse.Content.ReadAsStringAsync();
                return Json(new
                {
                    success = oResponse.IsSuccessStatusCode,
                    statuscode = (int)oResponse.StatusCode,
                    response = cMessage
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                success = false,
                statuscode = 400,
                response = "{ \"Message\": \"Bad Request.\"}"
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        // =========== SCHEDULE =============
        #region SCHEDULE

        public ActionResult ScheduleView(string cServiceId, string cScheduleId)
        {
            if (cScheduleId != null && cScheduleId.Length > 0)
            {
                this.oServices.LoadServices();
                ServicesSchedule oSchedule = this.oServices.getSchedule(cServiceId,cScheduleId);
                ViewBag.oService = this.oServices.getService(cServiceId,false); //Faster getService
                return View(oSchedule);
            }
            else
            {
                return RedirectToAction("Index", "Services");
            }
        }
        
        [HttpGet]
        public ActionResult GetSchedules(string cServiceId)
        {
            List<ServicesSchedule> oScheduleList = this.oServices.getSchedules(cServiceId);
            return Json(oScheduleList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetSchedule(string cServiceId, string cScheduleId)
        {
            ServicesSchedule oSchedule = this.oServices.getSchedule(cServiceId, cScheduleId);
            return Json(oSchedule, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetNewSchedule(string cServiceId)
        {
            ServicesSchedule oSchedule = new ServicesSchedule();
            oSchedule.ServiceId = cServiceId;
            oSchedule.Name = "New Schedule";
            oSchedule.Id = "NULLID";
            return Json(new
            {
                success = true,
                statuscode = 200,
                response = "",
                odata = oSchedule
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ScheduleAddView(string cServiceId)
        {
            if (cServiceId != null && cServiceId.Length > 0)
            {
                Service oService = this.oServices.LoadService(cServiceId);
                if (oService != null)
                {
                    ServicesSchedule oNewSchedule = new ServicesSchedule { Id = "NULLID" };
                    oNewSchedule.ServiceId = oService.ServiceId;
                    return PartialView("_ServiceScheduleAddForm", oNewSchedule);
                }
            }

            return RedirectToAction("Index", "Services");
        }
        
        [HttpPost]
        public async Task<ActionResult> UpdateSchedule(ServicesSchedule oSchedule)
        {
            HttpResponseMessage oResponse;
            if (ModelState.IsValid)
            {
                oResponse = this.oServices.UpdateSchedule(oSchedule);
                string cMessage = await oResponse.Content.ReadAsStringAsync();
                return Json(new
                {
                    success = oResponse.IsSuccessStatusCode,
                    statuscode = (int)oResponse.StatusCode,
                    response = cMessage
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                success = false,
                statuscode = 400,
                response = "{ \"Message\": \"Bad Request.\"}"
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteSchedule(string cServiceId, string cScheduleId)
        {
            HttpResponseMessage oResponse;
            if (ModelState.IsValid)
            {
                oResponse = this.oServices.DeleteSchedule(cServiceId, cScheduleId);
                string cMessage = await oResponse.Content.ReadAsStringAsync();
                return Json(new
                {
                    success = oResponse.IsSuccessStatusCode,
                    statuscode = (int)oResponse.StatusCode,
                    response = cMessage
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                success = false,
                statuscode = 400,
                response = "{ \"Message\": \"Bad Request.\"}"
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> DeactivateSchedule(string cServiceId, string cScheduleId)
        {
            HttpResponseMessage oResponse;
            if (ModelState.IsValid)
            {
                oResponse = this.oServices.DeactivateSchedule(cServiceId, cScheduleId);
                string cMessage = await oResponse.Content.ReadAsStringAsync();
                return Json(new
                {
                    success = oResponse.IsSuccessStatusCode,
                    statuscode = (int)oResponse.StatusCode,
                    response = cMessage
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                success = false,
                statuscode = 400,
                response = "{ \"Message\": \"Bad Request.\"}"
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> ActivateSchedule(string cServiceId, string cScheduleId)
        {
            HttpResponseMessage oResponse;
            if (ModelState.IsValid)
            {
                oResponse = this.oServices.ActivateSchedule(cServiceId, cScheduleId);
                string cMessage = await oResponse.Content.ReadAsStringAsync();
                return Json(new
                {
                    success = oResponse.IsSuccessStatusCode,
                    statuscode = (int)oResponse.StatusCode,
                    response = cMessage
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                success = false,
                statuscode = 400,
                response = "{ \"Message\": \"Bad Request.\"}"
            }, JsonRequestBehavior.AllowGet);
        }
        
        #endregion

        // =========== SCHEDULE ITEM =============
        #region SCHEDULE ITEM

        public ActionResult ScheduleItmAddView(string cServiceId, string cScheduleId)
        {
            if (cServiceId != null && cServiceId.Length > 0)
            {
                Service oService = this.oServices.LoadService(cServiceId);
                if (oService != null)
                {

                    ServicesSchedule oSchedule = oService.ServiceSchedule.Find(schd => schd.Id == cScheduleId);
                    if (oSchedule != null)
                    {
                        ScheduleItem oNewScheduleItm = new ScheduleItem { Id = -1 };
                        oNewScheduleItm.ScheduleId = oSchedule.Id;
                        return PartialView("_ScheduleItmAddForm", oNewScheduleItm);
                    }
                }
            }

            return RedirectToAction("Index", "Services");
        }

        [HttpGet]
        public ActionResult GetNewScheduleItem(string cScheduleId)
        {
            ScheduleItem oItem = new ScheduleItem();
            oItem.ScheduleId = cScheduleId;
            oItem.Id = -1;
            //return Json(oItem, JsonRequestBehavior.AllowGet); 
            return Json(new
            {
                success = true,
                statuscode = 200,
                response = "",
                odata = oItem
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetScheduleItem(string cServiceId, string cScheduleId, int nItemId)
        {
            ServicesSchedule oSchedule = this.oServices.getSchedule(cServiceId, cScheduleId);
            ScheduleItem oItem = oSchedule.ScheduleItens.Find(itm => itm.Id == nItemId);
            if(oItem != null)
            {
                //return Json(oItem, JsonRequestBehavior.AllowGet);
                return Json(new
                {
                    success = true,
                    statuscode = 200,
                    response = "",
                    odata = oItem
                }, JsonRequestBehavior.AllowGet);
            }

            oItem = new ScheduleItem();
            oItem.sMsgError.Add("Item not found.");
            //return Json(oItem, JsonRequestBehavior.AllowGet);
            return Json(new
            {
                success = true,
                statuscode = 200,
                response = "",
                odata = oItem
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetScheduleItens(string cServiceId, string cScheduleId)
        {
            ServicesSchedule oSchedule = this.oServices.getSchedule(cServiceId, cScheduleId);
            //return Json(oSchedule.ScheduleItens, JsonRequestBehavior.AllowGet);
            return Json(new
            {
                success = true,
                statuscode = 200,
                response = "",
                odata = oSchedule.ScheduleItens
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateItem(ScheduleItem oItem)
        {
            HttpResponseMessage oResponse;
            if (ModelState.IsValid)
            {
                oResponse = this.oServices.UpdateItem(oItem);
                string cMessage = await oResponse.Content.ReadAsStringAsync();
                return Json(new
                {
                    success = oResponse.IsSuccessStatusCode,
                    statuscode = (int)oResponse.StatusCode,
                    response = cMessage
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                success = false,
                statuscode = 400,
                response = "{ \"Message\": \"Bad Request.\"}"
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteItem(string cScheduleId, int nItemId)
        {
            HttpResponseMessage oResponse;
            if ((cScheduleId != null && cScheduleId.Length > 0) && nItemId > 0)
            {
                oResponse = this.oServices.DeleteItem(cScheduleId, nItemId);
                string cMessage = await oResponse.Content.ReadAsStringAsync();
                return Json(new
                {
                    success = oResponse.IsSuccessStatusCode,
                    statuscode = (int)oResponse.StatusCode,
                    response = cMessage
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                success = false,
                statuscode = 400,
                response = "{ \"Message\": \"Bad Request.\"}"
            }, JsonRequestBehavior.AllowGet);
        }


        #endregion

        // =========== GROUPS =============
        #region GROUPS

        public ActionResult ServiceGroups()
        {
            List<ServiceGroup> oGroups = oServices.getGroups();
            return View(oGroups);
        }

        //Gets All the Groups (Active/Inactive)
        [HttpGet]
        public ActionResult GetAllGroups()
        {
            List<ServiceGroup> oGroups = oServices.getGroups();
            return Json(oGroups, JsonRequestBehavior.AllowGet);
        }

        //Gets all the Active Groups
        [HttpGet]
        public ActionResult GetGroups()
        {
            List<ServiceGroup> oGroups = oServices.getGroups(true);
            return Json(oGroups, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetNewGroup()
        {
            ServiceGroup oGroup;
            oGroup = new ServiceGroup();
            oGroup.GroupId = "NULLID";
            oGroup.GroupName = "New Group";
            return Json(oGroup, JsonRequestBehavior.AllowGet);
            //return oGroup;
        }

        [HttpPost]
        public async Task<ActionResult> GroupUpdate(ServiceGroup oGroup)
        {
            HttpResponseMessage oResponse;
            if(ModelState.IsValid)
            {
                oResponse = this.oServices.UpdateGroup(oGroup);
                string cMessage = await oResponse.Content.ReadAsStringAsync();
                return Json(new
                {   success = oResponse.IsSuccessStatusCode,
                    statuscode = (int)oResponse.StatusCode,
                    response = cMessage
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                success = false,
                statuscode = 400,
                response = "{ \"Message\": \"Bad Request.\"}"
            }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public async Task<ActionResult> GroupActivate(string cGroupId)
        {
            HttpResponseMessage oResponse;
            
            if (cGroupId != null && cGroupId.Length > 0)
            {
                oResponse = this.oServices.ActivateGroup(cGroupId);
                string cMessage = await oResponse.Content.ReadAsStringAsync();
                return Json(new {
                                    success = oResponse.IsSuccessStatusCode,
                                    statuscode = (int)oResponse.StatusCode,
                                    response = "{ \"Message\": \"" + cMessage + "\"}"
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                success = false,
                statuscode = 400,
                response = "{ \"Message\": \"Bad Request.\"}"
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> GroupDeactivate(string cGroupId)
        {
            HttpResponseMessage oResponse; 

            if (cGroupId != null && cGroupId.Length > 0)
            {
                oResponse = this.oServices.DeactivateGroup(cGroupId);
                string cMessage = await oResponse.Content.ReadAsStringAsync();
                return Json(new {   success = oResponse.IsSuccessStatusCode,
                                    statuscode = (int)oResponse.StatusCode,
                                    response = "{ \"Message\": \"" + cMessage + "\"}"
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                success = false,
                statuscode = 400,
                response = "{ \"Message\": \"Bad Request.\"}"
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        public async Task<ActionResult> GroupDelete(string cGroupId)
        {
            HttpResponseMessage oResponse;
            if (cGroupId != null && cGroupId.Length > 0)
            {
                oResponse = this.oServices.DeleteGroup(cGroupId);
                string cMessage = await oResponse.Content.ReadAsStringAsync();
                return Json(new
                {
                    success = oResponse.IsSuccessStatusCode,
                    statuscode = (int)oResponse.StatusCode,
                    response = "{ \"Message\": \"" + cMessage + "\"}"
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                success = false,
                statuscode = 400,
                response = "{ \"Message\": \"Bad Request.\"}"
            }, JsonRequestBehavior.AllowGet);

        }
        #endregion

        // =========== LISTS =============
        #region LISTS

        protected void ListPopulate(Service oSelectedService)
        {
            ViewBag.oLists = this.oServices.oLists;
            ViewBag.ServicesGroup_List = this.oServices.oLists.getSelectList("ServicesGroup", oSelectedService != null ? oSelectedService.ServiceGroup : null);
        }
        #endregion
    }
}