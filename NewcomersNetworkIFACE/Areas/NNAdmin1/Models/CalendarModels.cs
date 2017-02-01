using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewcomersNetworkIFACE.Client;
using NewcomersNetworkAPI.Models;
using System.Net.Http;

namespace NewcomersNetworkIFACE.Areas.NNAdmin1.Models
{
    public class Calendar : NNInterfaceModel
    {
        public List<CalendarEvent> oEvents = new List<CalendarEvent>();

        public Calendar()
        {
        }

        public void LoadAllEvents(DateTime dStartDate, DateTime dEndDate)
        {
            if (dStartDate != null && dEndDate != null)
            {
                this.LoadEvents(dStartDate, dEndDate);
                this.LoadServices(dStartDate, dEndDate);
            }
        }

        public void LoadEvents(DateTime dStartDate, DateTime dEndDate, bool bHasSlots = false)
        {
            List<CalendarEvent> oEventList;
            oEventList = oNNAPICLient.Get<List<CalendarEvent>>("/Calendar/Events/" + dStartDate.ToString() + "/" + dEndDate.ToString());
            foreach (CalendarEvent oEvent in oEventList)
            {
                this.oEvents.Add(oEvent);
            }
        }

        public void LoadServices(DateTime dStartDate, DateTime dEndDate, bool bHasSlots = false)
        {
            List<CalendarEvent> oServiceList;
            oServiceList = oNNAPICLient.Get<List<CalendarEvent>>("/Calendar/Services/" + dStartDate.ToString() + "/" + dEndDate.ToString());
            foreach (CalendarEvent oService in oServiceList)
            {
                this.oEvents.Add(oService);
            }
        }

    }
    
}