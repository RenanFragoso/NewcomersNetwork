using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewcomersNetworkIFACE.Areas.NNAdmin.Models
{
    public class Event : NewcomersNetworkAPI.Models.Event
    {

        public bool CreateEvent()
        {
            //POST New event using API
            return true;
        }

        public bool UpdateEvent()
        {
            //PUT event using API
            return true;
        }

        public bool DeleteEvent()
        {
            //DELETE event using API
            return true;
        }

    }
}