using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewcomersNetworkIFACE.Client;
using NewcomersNetworkAPI.Models;
using System.Web.Mvc;

namespace NewcomersNetworkIFACE.Client
{
    public class NNInterfaceModel
    {
        protected NNAPIClient oNNAPICLient = new NNAPIClient();

        public virtual OptionsLists getSelectList(string cListName)
        {
            OptionsLists oList = this.oNNAPICLient.Get<OptionsLists>("/OptionsLists/" + cListName);
            if (oList != null)
            {
                return oList;
            }
            else
            {
                oList = new OptionsLists();
            }

            return oList;
        }
    }
}