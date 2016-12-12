using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewcomersNetworkAPI.Models;

namespace NewcomersNetworkIFACE.Areas.NNAdmin1.Models
{
    public class DashBoard
    {
        public List<int> oSquares;
        public List<DashGraph1> oGraph1;
        public List<DashGraph2> oGraph2;
        public List<DashGraph3> oGraph3;
        public List<Service> oServices;
        public List<Event> oEvents;

        public DashBoard()
        {
            oSquares = new List<int>();
            oSquares.Add(100);
            oSquares.Add(2);
            oSquares.Add(50);
            oSquares.Add(83);
        }
    }

    public class DashGraph1     //Users Registrations
    {

        string cMonthYear { set; get; }
        int nNewRegistrations { set; get; }
        int nNeedsRegistered { set; get; }
        int nHelpRegistered { set; get; }

    }

    public class DashGraph2     //Needs
    {
        string cMonthYear { set; get; }
        int nNeedsRegistered { set; get; }
        int nNeedsMet { set; get; }
        int nNeedsTotal { set; get; }

    }

    public class DashGraph3     //Needs x Category
    {
        string cMonthYear { set; get; }
        int nHousingNeeds { set; get; }
        int nSettlementNeeds { set; get; }
        int nEmploymentNeeds { set; get; }
        int nAccessibilityNeeds { set; get; }

    }

    public class ImportantInfo
    {
        int nClassifieds { get; set; } = 0;
        int nEvents { get; set; } = 0;
        int nReports { get; set; } = 0;
        int nServices { get; set; } = 0;
        int nUsers { get; set; } = 0;
        int nCalendar { get; set; } = 0;
        int nMailing { get; set; } = 0;
    }
    
}