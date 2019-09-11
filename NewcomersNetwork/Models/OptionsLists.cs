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
        public List<OptionList> ValueLists = new List<OptionList>();

        //protected Dictionary<string, string> oAvailabeLists = new Dictionary<string, string>();
        protected List<AvailabeList> AvailabeLists = new List<AvailabeList>();

        public OptionsLists()
        {
        }

        public OptionsLists(string cListName)
        {
            this.LoadList(cListName);
        }

        /// <summary>
        /// Load all AvailableList using the stored procedure
        /// (this method shouldn't be used in the Interface, it will raise an error)
        /// </summary>
        protected void LoadParams()
        {

            DataTable oLists = DBConn.ExecuteCommand("sp_GetChoices", null).Tables[0];
            for( int nX = 0; nX < oLists.Rows.Count; nX++)
            {
                this.AvailabeLists.Add( new AvailabeList( oLists.Rows[nX]["ChoiceName"].ToString(), oLists.Rows[nX]["ChoiceGroup"].ToString(), oLists.Rows[nX]["ChoiceSP"].ToString()));
            }
        }

        /// <summary>
        /// Load AvailableList by given list name
        /// (this method shouldn't be used in the Interface, it will raise an error)
        /// </summary>
        /// <param name="cListName"></param>
        /// <returns></returns>
        public bool LoadList(string cListName)
        {
            this.LoadParams();
            if (this.isValid && cListName != null && cListName.Length > 0)
            {
                AvailabeList Available;
                //string cSPList;
                //if (this.oAvailabeLists.TryGetValue(cListName, out cSPList))
                Available = this.AvailabeLists.Find(itm => itm.cListName == cListName);
                if (Available != null)
                {
                    this.ValueLists.Add( new OptionList(Available.cListName, Available.cListSP));
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

        /// <summary>
        /// Load all Available lists filtered by given group
        /// (this method shouldn't be used in the Interface, it will raise an error)
        /// </summary>
        /// <param name="cGroupName"></param>
        /// <returns></returns>
        public bool LoadListGroup(string cGroupName)
        {
            this.LoadParams();
            if (cGroupName != null && cGroupName.Length > 0)
            {
                //if (this.oAvailabeLists.TryGetValue(out cListName, out cSPList))
                foreach (AvailabeList oAvailable in AvailabeLists.FindAll(itm => itm.cListGroup == cGroupName))
                {
                    this.ValueLists.Add(new OptionList(oAvailable.cListName, oAvailable.cListSP));
                }
                return true;
            }

            this.sMsgError.Add("Invalid Parameter.");
            return false;

        }

        /// <summary>
        /// Load all Available lists
        /// (this method shouldn't be used in the Interface, it will raise an error)
        /// </summary>
        /// <returns></returns>
        public bool LoadAll()
        {
            //foreach (KeyValuePair<string, string> entry in this.oAvailabeLists)
            foreach (AvailabeList oList in this.AvailabeLists)
            {
                this.ValueLists.Add(new OptionList(oList.cListName, oList.cListSP));
            }

            return true;
        }

        /// <summary>
        /// Use this method to get the reference of an already loaded list by it's name
        /// (this method doesn't load the params, reusable in Interface)
        /// </summary>
        /// <param name="cListName"></param>
        /// <returns></returns>
        public IEnumerable<object> getList(string cListName)
        {
            OptionList oListRet;
            if (cListName != null && cListName.Length > 0)
            {
                //if (this.oAvailabeLists.TryGetValue(cListName, out cSPList))
                oListRet = this.ValueLists.Find(itm => itm.ListName == cListName);
                if(oListRet == null)
                {
                    this.sMsgError.Add("List not found.");
                    oListRet = new OptionList();
                }
            }
            else
            {
                this.sMsgError.Add("Bad parameter.");
                oListRet = new OptionList();
            }

            return oListRet.Itens.AsEnumerable();
        }

        /// <summary>
        /// Use this method to get the reference of an already loaded list6
        /// (this method doesn't load the params, reusable in Interface)
        /// </summary>
        /// <param name="cListName"></param>
        /// <returns></returns>
        public IEnumerable<object> getListGroup(string cGroupName)
        {
            OptionList oListRet;
            if (cGroupName != null && cGroupName.Length > 0)
            {
                AvailabeList oAvailable;
                //if (this.oAvailabeLists.TryGetValue(cListName, out cSPList))
                oAvailable = this.AvailabeLists.Find(itm => itm.cListGroup == cGroupName);
                if (oAvailable != null)
                {
                    oListRet = new OptionList(oAvailable.cListName, oAvailable.cListSP);
                }
                else
                {
                    this.sMsgError.Add("Group not found.");
                    oListRet = new OptionList();
                }
            }
            else
            {
                oListRet = new OptionList();
            }

            return oListRet.Itens.AsEnumerable();
        }

        /// <summary>
        /// Return an OptionList in SelectList format
        /// (this method doesn't load the params, reusable in Interface)
        /// </summary>
        /// <param name="cListName"></param>
        /// <param name="selectedValue"></param>
        /// <returns></returns>
        public SelectList getSelectList(string cListName, object selectedValue = null)
        {
            return new SelectList(this.getList(cListName), "id", "text", selectedValue);
        }

        public string getValue(string cListName, string cId)
        {
            OptionList oOption;
            OptionItem oItem;

            if ( (cListName != null && cListName.Length >0 ) && (cId != null && cId.Length > 0))
            {
                oOption = this.ValueLists.Find( list => list.ListName == cListName );
                if (oOption != null)
                {
                    oItem = oOption.Itens.Find( item => item.id == cId);
                    if (oItem != null)
                    {
                        return oItem.text;
                    }
                }
            }
            return "";
        }

    }

    /// <summary>
    /// This classs contains the available lists
    /// </summary>
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