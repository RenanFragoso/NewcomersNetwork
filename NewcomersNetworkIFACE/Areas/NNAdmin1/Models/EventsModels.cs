using NewcomersNetworkAPI.Models;
using NewcomersNetworkIFACE.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace NewcomersNetworkIFACE.Areas.NNAdmin1.Models
{
    public class Events
    {
        protected NNAPIClient oNNAPICLient = new NNAPIClient();
        public List<Event> oEventList { get; protected set; }
        public OptionsLists oLists { get; set; }

        public Events()
        {
        }

        // =========== EVENTS =============
        #region EVENT

        public void LoadEvents()
        {
            //Gets the events from API
            this.oEventList = oNNAPICLient.Get<List<Event>>("/Events/All");
        }

        public Event GetEvent(string cEventId)
        {
            Event oEvent;
            this.LoadEvents();
            oEvent = this.oEventList.Find(evt => evt.Id == cEventId);
            if (oEvent != null)
            {
                return oEvent;
            }
            return new Event();
        }

        public HttpResponseMessage UpdateEvent(Event oEvent)
        {
            HttpResponseMessage response;
            if (oEvent != null)
            {

                if (oEvent.Id == "NULLID")
                {
                    //Update Service
                    response = oNNAPICLient.Post<Event>("/Events/Create", oEvent);
                    return response;
                }
                else
                {
                    //New Service
                    response = oNNAPICLient.Put<Event>("/Events/Update", oEvent);
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

        public HttpResponseMessage DeleteEvent(string cEventId)
        {
            HttpResponseMessage response;

            if (cEventId != null && cEventId.Length > 0)
            {
                //Delete Schedule
                response = oNNAPICLient.Delete("/Events/Delete", cEventId);
                return response;
            }
            else
            {
                response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
                response.Content = new StringContent("Invalid data received.");
                return response;
            }
        }

        public HttpResponseMessage ActivateEvent(string cEventId)
        {
            HttpResponseMessage response;

            if (cEventId != null && cEventId.Length > 0)
            {
                //Delete Schedule
                response = oNNAPICLient.Post<string>("/Events/Activate/" + cEventId, "");
                return response;
            }
            else
            {
                response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
                response.Content = new StringContent("Invalid data received.");
                return response;
            }

        }

        public HttpResponseMessage DeactivateEvent(string cEventId)
        {
            HttpResponseMessage response;

            if (cEventId != null && cEventId.Length > 0)
            {
                //Delete Schedule
                response = oNNAPICLient.Post<string>("/Events/Deactivate/" + cEventId, "");
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

        // =========== EVENTS =============

    
        #region REGISTRATIONS
        /*
        public List<EventRegistration> getRegistrations(string cEventId)
        {
            List<EventRegistration> oRegistrations;

            if (cEventId != null && cEventId.Length > 0)
            {
                oRegistrations = this.oNNAPICLient.Get<List<EventRegistration>>("/Events/Registrations/" + cEventId);
                return oRegistrations;
            }

            return new List<EventRegistration>();

        }
        */
        #endregion
    }
}