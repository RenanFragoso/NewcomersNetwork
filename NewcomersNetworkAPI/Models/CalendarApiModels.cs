using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using NewcomersNetworkAPI.Providers;

namespace NewcomersNetworkAPI.Models
{
    public class CalendarApi : NNAPIModel
    {

        public List<CalendarEvent> oEvents { get; set; } = new List<CalendarEvent>();
        public DateTime dStartDate { get; set; } = DateTime.Now;
        public DateTime dEndDate { get; set; } = DateTime.Now;

        public CalendarApi()
        {
        }

        public CalendarApi(DateTime dStartDate, DateTime dEndDate)
        {
            this.dStartDate = dStartDate;
            this.dEndDate = dEndDate;
        }

        public void setDates(DateTime dStartDate, DateTime dEndDate)
        {
            this.dStartDate = dStartDate;
            this.dEndDate = dEndDate;
        }

        public void LoadServices()
        {
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            infoParameters.Add("dStartDate", dStartDate);
            infoParameters.Add("dEndDate", dEndDate);
            DataTable oServicesDB = DBConn.ExecuteCommand("sp_Calendar_GetServices", infoParameters).Tables[0];

            CalendarEvent oEvent;

            foreach (DataRow row in oServicesDB.Rows)
            {
                oEvent = new CalendarEvent();
                oEvent.id = row["ScheduleId"].ToString() + "#" + row["ItemId"].ToString();
                oEvent.title = row["ServiceName"].ToString();
                oEvent.start = ((DateTime)row["StartDate"]).ToString("yyyy-MM-ddTHH:mm:ssZ");
                oEvent.end = ((DateTime)row["EndDate"]).ToString("yyyy-MM-ddTHH:mm:ssZ");
                oEvent.color = row["GroupColor"].ToString();
                if(oEvent.color == "#ffffff" || oEvent.color == "#b6d554") //White or Lime
                {
                    oEvent.textColor = "#000000";
                }

                //Specific Attributes
                oEvent.GroupIcon = row["GroupIcon"].ToString();
                oEvent.Slots = (int)row["Slots"];
                oEvent.MaxSlots = (int)row["MaxSlots"];

                this.oEvents.Add(oEvent);
            }
        }

        public void LoadEvents()
        {
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            infoParameters.Add("dStartDate", dStartDate);
            infoParameters.Add("dEndDate", dEndDate);
            DataTable oEventsDB = DBConn.ExecuteCommand("sp_Calendar_GetEvents", infoParameters).Tables[0];

            CalendarEvent oEvent;

            foreach (DataRow row in oEventsDB.Rows)
            {
                oEvent = new CalendarEvent();
                oEvent.id = row["Id"].ToString();
                oEvent.title = row["Name"].ToString();
                oEvent.start = ((DateTime)row["StartDate"]).ToString("yyyy-MM-ddTHH:mm:ssZ");
                oEvent.end = ((DateTime)row["EndDate"]).ToString("yyyy-MM-ddTHH:mm:ssZ");

                oEvent.color = "#ba0f6b"; //Magenta
                oEvent.textColor = "#fff";
                oEvent.url = "/Events?cEventId=" + row["Id"];

                //Specific Attributes

                this.oEvents.Add(oEvent);
            }
        }


    }
}