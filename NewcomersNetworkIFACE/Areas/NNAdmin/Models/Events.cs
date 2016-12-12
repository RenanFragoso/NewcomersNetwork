using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewcomersNetworkIFACE.Areas.NNAdmin.Models
{
    public class Events
    {

        public const string cApiEvent = "Events";

        IList<Event> oEventsAll;
        IList<Event> oEventsActive;
        IList<Event> oEventsCanceled;
        IList<Event> oEventsDone;

        public Events()
        {
            //Constructor from API

        }

    }
}