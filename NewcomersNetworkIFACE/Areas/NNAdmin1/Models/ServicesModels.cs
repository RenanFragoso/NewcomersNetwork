using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewcomersNetworkAPI.Models;
using NewcomersNetworkIFACE.Client;

namespace NewcomersNetworkIFACE.Areas.NNAdmin1.Models
{
    public class Services
    {

        protected NNAPIClient oNNAPICLient = new NNAPIClient();
        public List<Service> oServiceList { get; set; } = new List<Service>();
        public OptionsLists oLists { get; set; }

        public Services()
        {
            //Gets the events from API
            this.oServiceList = oNNAPICLient.Get<List<Service>>("/Services");

            //Get the Lists
            //this.oLists = oNNAPICLient.Get<OptionsLists>("/OptionsLists/Services");
        }

        public Service getService(string cServiceId)
        {
            Service oService;
            oService = this.oServiceList.Find(s => s.ServiceId == cServiceId);

            if(oService == null)
            {
                oService = new Service();
            }

            return oService;
        }

    }
}