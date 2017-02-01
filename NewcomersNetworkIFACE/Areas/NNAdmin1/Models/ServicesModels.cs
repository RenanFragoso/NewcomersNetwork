using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewcomersNetworkAPI.Models;
using NewcomersNetworkIFACE.Client;
using System.Net.Http;

namespace NewcomersNetworkIFACE.Areas.NNAdmin1.Models
{
    public class Services : NNInterfaceModel
    {

        public List<Service> oServiceList { get; set; } = new List<Service>();
        public OptionsLists oLists { get; set; }

        public Services()
        {
        }

        #region SERVICE

        public void LoadServices()
        {
            //Gets the events from API
            this.oServiceList = oNNAPICLient.Get<List<Service>>("/Services");

            //Get the Lists
            //this.oLists = oNNAPICLient.Get<OptionsLists>("/OptionsLists/Services");
        }

        public Service LoadService(string cServiceId,bool bLoadFull = true)
        {
            //Gets the events from API
            Service oService;
            if (bLoadFull)
            {
                oService = oNNAPICLient.Get<Service>("/Services/GetFull/" + cServiceId);
            }
            else
            {
                oService = oNNAPICLient.Get<Service>("/Services/" + cServiceId);
            }

            if (oService != null)
            {
                return oService;
            }
            else
            {
                return new Service();
            }
        }

        public Service getService(string cServiceId, bool getFull = true)
        {
            Service oService;
            oService = this.oServiceList.Find(s => s.ServiceId == cServiceId);

            if(oService == null)
            {
                oService = new Service();
                oService.sMsgError.Add("Service not found.");
            }
            else
            {
                if (getFull)
                {
                    oService = oNNAPICLient.Get<Service>("/Services/GetFull/" + oService.ServiceId);
                    if (oService == null)
                    {
                        oService = new Service();
                    }
                }
            }
            return oService;
        }

        public HttpResponseMessage UpdateService(Service oService)
        {
            HttpResponseMessage response;

            if (oService != null)
            {

                if (oService.ServiceId == "NULLID")
                {
                    //Update Service
                    response = oNNAPICLient.Post<Service>("/Services/Create", oService);
                    return response;
                }
                else
                {
                    //New Service
                    response = oNNAPICLient.Put<Service>("/Services/Update", oService);
                    return response;
                }

            }
            else
            {
                response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
                response.Content = new StringContent("Invalid data received.");
                return response;
            }
        }

        public HttpResponseMessage DeleteService(string cServiceId)
        {
            HttpResponseMessage response;

            if (cServiceId != null && cServiceId.Length > 0 )
            {
                //Delete Schedule
                response = oNNAPICLient.Delete("/Services/Delete", cServiceId);
                return response;
            }
            else
            {
                response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
                response.Content = new StringContent("Invalid data received.");
                return response;
            }

        }

        public HttpResponseMessage ActivateService(string cServiceId)
        {
            HttpResponseMessage response;

            if (cServiceId != null && cServiceId.Length > 0)
            {
                //Delete Schedule
                response = oNNAPICLient.Post<string>("/Services/Activate/" + cServiceId, "");
                return response;
            }
            else
            {
                response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
                response.Content = new StringContent("Invalid data received.");
                return response;
            }

        }

        public HttpResponseMessage DeactivateService(string cServiceId)
        {
            HttpResponseMessage response;

            if (cServiceId != null && cServiceId.Length > 0)
            {
                //Delete Schedule
                response = oNNAPICLient.Post<string>("/Services/Deactivate/" + cServiceId, "");
                return response;
            }
            else
            {
                response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
                response.Content = new StringContent("Invalid data received.");
                return response;
            }

        }

        #endregion

        // =========== SCHEDULE ===========
        #region SCHEDULE

        public ServicesSchedule getSchedule(string cServiceId, string cScheduleId)
        {
            this.LoadServices();
            Service oService = this.getService(cServiceId);
            if(oService != null)
            {
                if(oService.ServiceSchedule.Count > 0)
                {
                    ServicesSchedule oSchedule = oService.ServiceSchedule.Find(osch => osch.Id == cScheduleId);
                    return oSchedule;
                }
            }
            return new ServicesSchedule();
        }

        public List<ServicesSchedule> getSchedules(string cServiceId)
        {
            this.LoadServices();
            Service oService = this.getService(cServiceId);
            if (oService != null)
            {
                if (oService.ServiceSchedule.Count > 0)
                {
                    return oService.ServiceSchedule;
                }
            }
            return new List<ServicesSchedule>();
        }

        public HttpResponseMessage UpdateSchedule(ServicesSchedule oSchedule)
        {
            HttpResponseMessage response;

            if (oSchedule != null)
            {

                if (oSchedule.Id != "NULLID")
                {
                    //Update Schedule
                    response = oNNAPICLient.Put<ServicesSchedule>("/Services/Schedule/Update", oSchedule);
                    return response;
                }
                else
                {
                    //New Schedule
                    response = oNNAPICLient.Post<ServicesSchedule>("/Services/Schedule/Create", oSchedule);
                    return response;
                }

            }
            else
            {
                response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
                response.Content = new StringContent("Invalid data received.");
                return response;
            }

        }

        public HttpResponseMessage DeleteSchedule(string cServiceId, string cScheduleId)
        {
            HttpResponseMessage response;

            if ((cServiceId != null && cScheduleId != null) && (cServiceId.Length > 0 && cScheduleId.Length > 0))
            {
                //Delete Schedule
                response = oNNAPICLient.Delete("/Services/" + cServiceId + "/Schedule/Delete", cScheduleId);
                return response;
            }
            else
            {
                response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
                response.Content = new StringContent("Invalid data received.");
                return response;
            }

        }

        public HttpResponseMessage ActivateSchedule(string cServiceId, string cScheduleId)
        {
            HttpResponseMessage response;

            if ((cServiceId != null && cScheduleId != null) && (cServiceId.Length > 0 && cScheduleId.Length > 0))
            {
                //Delete Schedule
                response = oNNAPICLient.Post<string>("/Services/" + cServiceId + "/Schedule/Activate/" + cScheduleId, "");
                return response;
            }
            else
            {
                response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
                response.Content = new StringContent("Invalid data received.");
                return response;
            }

        }

        public HttpResponseMessage DeactivateSchedule(string cServiceId, string cScheduleId)
        {
            HttpResponseMessage response;

            if ((cServiceId != null && cScheduleId != null) && (cServiceId.Length > 0 && cScheduleId.Length > 0))
            {
                //Delete Schedule
                response = oNNAPICLient.Post<string>("/Services/" + cServiceId + "/Schedule/Deactivate/" + cScheduleId, "" );
                return response;
            }
            else
            {
                response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
                response.Content = new StringContent("Invalid data received.");
                return response;
            }

        }


        #endregion

        // =========== GROUPS ===========
        #region GROUPS

        public ServiceGroup getGroup(string cGroupId)
        {
            ServiceGroup oGroup;
            if (cGroupId != null && cGroupId.Length > 0)
            {
                oGroup = oNNAPICLient.Get<ServiceGroup>("/Services/Group", cGroupId);
                if (oGroup != null)
                {
                    return oGroup;
                }
            }

            oGroup = new ServiceGroup();
            return oGroup;

        }

        public List<ServiceGroup> getGroups(bool bOnlyActive = false)
        {
            List<ServiceGroup> oGroups;

            if (bOnlyActive)
            {
                oGroups = oNNAPICLient.Get<List<ServiceGroup>>("/Services/Groups/Active");
            }
            else
            {
                oGroups = oNNAPICLient.Get<List<ServiceGroup>>("/Services/Groups");
            }

            if (oGroups != null)
            {
                return oGroups;
            }
            else
            {
                oGroups = new List<ServiceGroup>();
            }

            return oGroups;
        }
                
        public HttpResponseMessage UpdateGroup(ServiceGroup oGroup)
        {
            HttpResponseMessage response;

            if (oGroup != null)
            {

                if (oGroup.GroupId != "NULLID")
                {
                    //Update Group
                    response = oNNAPICLient.Put<ServiceGroup>("/Services/Groups/Update", oGroup);
                    return response;
                }
                else
                {
                    //New Group
                    response = oNNAPICLient.Post<ServiceGroup>("/Services/Groups/Create", oGroup);
                    return response;
                }

            }
            else
            {
                response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
                response.Content = new StringContent("Invalid data received.");
                return response;
            }

        }

            
        public HttpResponseMessage DeleteGroup(string cGroupId)
        {
            HttpResponseMessage response;

            if (cGroupId != null && cGroupId.Length > 0)
            {

                //Update API
                response = oNNAPICLient.Delete("/Services/Groups/Delete", cGroupId);
                return response;

            }
            else
            {
                response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
                response.Content = new StringContent("Invalid Request.");
                return response;
            }
        }

        public HttpResponseMessage ActivateGroup(string cGroupId)
        {
            HttpResponseMessage response;
            ServiceGroup oGroup = new ServiceGroup();
            if (cGroupId != null && cGroupId.Length > 0)
            {
                oGroup.GroupId = cGroupId;
                response = oNNAPICLient.Post<ServiceGroup>("/Services/Groups/Activate", oGroup);
                return response;
            }
            else
            {
                response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
                response.Content = new StringContent("Invalid Request.");
                return response;
            }
        }

        public HttpResponseMessage DeactivateGroup(string cGroupId)
        {
            HttpResponseMessage response;
            ServiceGroup oGroup = new ServiceGroup();
            if (cGroupId != null && cGroupId.Length > 0)
            {
                oGroup.GroupId = cGroupId;
                response = oNNAPICLient.Post<ServiceGroup>("/Services/Groups/Deactivate", oGroup);
                return response;
            }
            else
            {
                response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
                response.Content = new StringContent("Invalid Request.");
                return response;
            }
        }

        #endregion

        // =========== ITENS ===========
        #region SCHEDULE ITENS

        public HttpResponseMessage UpdateItem(ScheduleItem oItem)
        {
            HttpResponseMessage response;

            if (oItem != null)
            {

                if (oItem.Id > -1)
                {
                    //Update Item
                    response = oNNAPICLient.Put<ScheduleItem>("/Services/ScheduleItens/Update", oItem);
                    return response;
                }
                else
                {
                    //New Item
                    response = oNNAPICLient.Post<ScheduleItem>("/Services/ScheduleItens/Create", oItem);
                    return response;
                }

            }
            else
            {
                response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
                response.Content = new StringContent("Invalid data received.");
                return response;
            }

        }

        public HttpResponseMessage DeleteItem(string cScheduleId, int nItemId)
        {
            HttpResponseMessage response;

            if ((cScheduleId != null && cScheduleId.Length > 0) && nItemId > 0 )
            {
                response = oNNAPICLient.Delete("/Services/ScheduleItens/Delete", cScheduleId + "/" + nItemId.ToString());
                return response;
            }
            else
            {
                response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
                response.Content = new StringContent("Invalid Request.");
                return response;
            }
        }
        
        /*
        public HttpResponseMessage ActivateGroup(string cGroupId)
        {
            HttpResponseMessage response;
            ServiceGroup oGroup = new ServiceGroup();
            if (cGroupId != null && cGroupId.Length > 0)
            {
                oGroup.GroupId = cGroupId;
                response = oNNAPICLient.Post<ServiceGroup>("/Services/Groups/Activate", oGroup);
                return response;
            }
            else
            {
                response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
                response.Content = new StringContent("Invalid Request.");
                return response;
            }
        }

        public HttpResponseMessage DeactivateGroup(string cGroupId)
        {
            HttpResponseMessage response;
            ServiceGroup oGroup = new ServiceGroup();
            if (cGroupId != null && cGroupId.Length > 0)
            {
                oGroup.GroupId = cGroupId;
                response = oNNAPICLient.Post<ServiceGroup>("/Services/Groups/Deactivate", oGroup);
                return response;
            }
            else
            {
                response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
                response.Content = new StringContent("Invalid Request.");
                return response;
            }
        }
        */

        #endregion


    }
}