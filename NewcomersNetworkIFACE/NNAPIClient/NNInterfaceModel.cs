using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewcomersNetworkIFACE.Client;
using NewcomersNetworkAPI.Models;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace NewcomersNetworkIFACE.Client
{
    /// <summary>
    /// Base for NNInterface ViewModels
    /// </summary>
    public class NNInterfaceModel
    {
        [JsonIgnore]
        public Dictionary<string, SelectList> oSelectLists = new Dictionary<string,SelectList>(); 
        protected NNAPIClient oNNAPICLient = new NNAPIClient();

        public virtual void loadList(string cListName)
        {
            //Get specific List
            OptionsLists oLists = this.oNNAPICLient.Get<OptionsLists>("/OptionsLists/" + cListName);
            if(oLists != null)
            {
                this.oSelectLists.Add(cListName, oLists.getSelectList(cListName));
            }
        }

        public virtual void loadListsGroup(string cGroupName)
        {
            //Get the group Select Lists
            OptionsLists oLists = this.oNNAPICLient.Get<OptionsLists>("/OptionsLists/Group/" + cGroupName);
            if(oLists != null)
            {
                foreach (OptionList oList in oLists.oOptions)
                {
                    this.oSelectLists.Add(oList.cListName, oLists.getSelectList(oList.cListName));
                }
            }
        }
        
        public virtual SelectList getSelectList(string cListName)
        {

            SelectList oList;
            this.oSelectLists.TryGetValue(cListName, out oList);
            if(oList == null)
            {
                return new SelectList(new List<SelectListItem>());  //Empty select list
            }
            return oList;
        }

        public virtual string ToJson()
        {
            return new JavaScriptSerializer().Serialize(this);
        }

    }
}