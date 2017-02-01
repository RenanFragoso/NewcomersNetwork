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

        protected Dictionary<string, string> oAvailabeLists = new Dictionary<string, string>();

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
            #region Users Lists
            //Users Administration
            
            //User Education List
            this.oAvailabeLists.Add("education", "sp_GetChoices_Education");
            //User Age Range List
            this.oAvailabeLists.Add("agerange", "sp_GetChoices_AgeRange");
            //User Gender List
            this.oAvailabeLists.Add("gender", "sp_GetChoices_Gender");
            //User Marital Status List
            this.oAvailabeLists.Add("maritalstatus", "sp_GetChoices_MaritalStatus");
            //Users Roles List
            this.oAvailabeLists.Add("usersroles", "sp_GetChoices_UsersRoles");
            #endregion

            #region Services Lists
            //Services Group List
            this.oAvailabeLists.Add("servicesgroup", "sp_GetChoices_ServicesGroups");
            //Services Group List Icon/Color
            this.oAvailabeLists.Add("servicesgroupicon", "sp_GetChoices_ServicesGroupsIcon");
            //Services Group Colors
            this.oAvailabeLists.Add("groupcolors", "sp_GetChoices_GroupColors");
            //Services Group Icons
            this.oAvailabeLists.Add("groupicons", "sp_GetChoices_GroupIcons");
            #endregion
        }

        public bool LoadList(string cListName)
        {
            if (cListName != null && cListName.Length > 0)
            {
                string cSPList;
                if (this.oAvailabeLists.TryGetValue(cListName, out cSPList))
                {
                    this.oOptions.Add( new OptionList(cListName, cSPList));
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

        public bool LoadAll()
        {
            foreach (KeyValuePair<string, string> entry in this.oAvailabeLists)
            {
                this.oOptions.Add(new OptionList(entry.Key, entry.Value));
            }

            return true;
        }

        public IEnumerable<object> getList(string cListName)
        {
            OptionList oListRet;
            if (cListName != null && cListName.Length > 0)
            {
                string cSPList;
                if (this.oAvailabeLists.TryGetValue(cListName, out cSPList))
                {
                    oListRet = new OptionList(cListName, cSPList);
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

}