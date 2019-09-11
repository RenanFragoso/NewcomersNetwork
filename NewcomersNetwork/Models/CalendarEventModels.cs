using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewcomersNetworkAPI.Models
{
    public class CalendarEvent : NNAPIModel
    {
        public string id { get; set; } = "";
        public string title { get; set; } = "";
        public bool allDay { get; set; } = false;
        public string start { set; get; } = DateTime.Now.ToString();
        public string end { set; get; } = DateTime.Now.ToString();
        public string url { get; set; } = "";
        public List<string> className { get; set; } = new List<string>();
        public bool editable { get; set; } = false;
        public bool startEditable { get; set; } = false;
        public bool durationEditable { get; set; } = false;
        public bool resourceEditable { get; set; } = false;
        public string rendering { get; set; } = ""; //empty, "background", or "inverse-background"
        public bool overlap { get; set; } = true;
        public string constraint { get; set; } = ""; //event ID, "businessHours", object
        public string color { get; set; } = "";
        public string backgroundColor { get; set; } = "";
        public string borderColor { get; set; } = "";
        public string textColor { get; set; } = "";
        public string Type { get; set; } = ""; // "S" - Service, "E" - Event

        //Specific Attributes
        public int Slots { get; set; } = 0;
        public int MaxSlots { get; set; } = 0;
        public string GroupIcon { get; set; } = "";

        public CalendarEvent()
        {
        }

    }
}