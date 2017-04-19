using NewcomersNetworkAPI.Models;
using NewcomersNetworkAPI.Providers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewcomersNetworkAPI.Models
{
    public class OptionsLists : NNAPIModel
    {
        public List<OptionList> oOptions = new List<OptionList>();

        //protected Dictionary<string, string> oAvailabeLists = new Dictionary<string, string>();
        protected List<AvailabeList> oAvailabeLists = new List<AvailabeList>();

        public OptionsLists()
        {
            this.LoadParams();
        }

        public OptionsLists(string cListName)
        {
            this.LoadParams();
            this.isValid = this.LoadList(cListName);
        }

        protected void LoadParams()
        {

            DataTable oLists = DBConn.ExecuteCommand("sp_GetChoices", null).Tables[0];
            for( int nX = 0; nX < oLists.Rows.Count; nX++)
            {
                this.oAvailabeLists.Add( new AvailabeList( oLists.Rows[nX]["ChoiceName"].ToString(), oLists.Rows[nX]["ChoiceGroup"].ToString(), oLists.Rows[nX]["ChoiceSP"].ToString()));
            }
        }

        public bool LoadList(string cListName)
        {
            if (cListName != null && cListName.Length > 0)
            {
                AvailabeList oAvailable;
                //string cSPList;
                //if (this.oAvailabeLists.TryGetValue(cListName, out cSPList))
                oAvailable = this.oAvailabeLists.Find(itm => itm.cListName == cListName);
                if (oAvailable != null)
                {
                    this.oOptions.Add( new OptionList(oAvailable.cListName, oAvailable.cListSP));
                    return true;
                }
                else
                {
                    this.sMsgError.Add("List not found.");
                    return false;
                }
            }

            this.sMsgError.Add("Invalid Parameter.");
            return false;

        }

        public bool LoadListGroup(string cGroupName)
        {
            if (cGroupName != null && cGroupName.Length > 0)
            {
                //if (this.oAvailabeLists.TryGetValue(out cListName, out cSPList))
                foreach (AvailabeList oAvailable in oAvailabeLists.FindAll(itm => itm.cListGroup == cGroupName))
                {
                    this.oOptions.Add(new OptionList(oAvailable.cListName, oAvailable.cListSP));
                }
                return true;
            }

            this.sMsgError.Add("Invalid Parameter.");
            return false;

        }

        public bool LoadAll()
        {
            //foreach (KeyValuePair<string, string> entry in this.oAvailabeLists)
            foreach (AvailabeList oList in this.oAvailabeLists)
            {
                this.oOptions.Add(new OptionList(oList.cListName, oList.cListSP));
            }

            return true;
        }

        public IEnumerable<object> getList(string cListName)
        {
            OptionList oListRet;
            if (cListName != null && cListName.Length > 0)
            {
                AvailabeList oAvailable;
                string cSPList;
                //if (this.oAvailabeLists.TryGetValue(cListName, out cSPList))
                oAvailable = this.oAvailabeLists.Find(itm => itm.cListName == cListName);
                if(oAvailable != null)
                {
                    oListRet = new OptionList(oAvailable.cListName, oAvailable.cListSP);
                }
                else
                {
                    this.sMsgError.Add("List not found.");
                    oListRet = new OptionList();
                }
            }
            else
            {
                oListRet = new OptionList();
            }

            return oListRet.oItens.AsEnumerable();
        }

        public SelectList getSelectList(string cListName, object selectedValue)
        {
            return new SelectList(this.getList(cListName), "id", "text", selectedValue);
        }

        public string getValue(string cListName, string cId)
        {
            OptionList oOption;
            OptionItem oItem;

            if ( (cListName != null && cListName.Length >0 ) && (cId != null && cId.Length > 0))
            {
                oOption = this.oOptions.Find( list => list.cListName == cListName );
                if (oOption != null)
                {
                    oItem = oOption.oItens.Find( item => item.id == cId);
                    if (oItem != null)
                    {
                        return oItem.text;
                    }
                }
            }
            return "";
        }

    }

    public class AvailabeList
    {
        public string cListName { get; set; } = "";
        public string cListGroup { get; set; } = "";
        public string cListSP { get; set; } = "";

        public AvailabeList(string cListName, string cListGroup, string cListSP)
        {
            this.cListName = cListName;
            this.cListGroup = cListGroup;
            this.cListSP = cListSP;
        }

    }


}