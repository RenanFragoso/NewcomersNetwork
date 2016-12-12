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
            //Gets the events from API
            this.oEventList = oNNAPICLient.Get<List<Event>>("/Events/All");
        }

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

    }
}