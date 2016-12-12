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
        
        public OptionsLists()
        {
        }

        public bool LoadAll()
        {

            //Education List
            oOptions.Add(new OptionList("Education","sp_GetChoices_Education"));

            //Age Range List
            oOptions.Add(new OptionList("AgeRange", "sp_GetChoices_AgeRange"));

            //Gender List
            oOptions.Add(new OptionList("Gender", "sp_GetChoices_Gender"));

            //Marital Status List
            oOptions.Add(new OptionList("MaritalStatus", "sp_GetChoices_MaritalStatus"));

            //Users Roles List
            oOptions.Add(new OptionList("UsersRoles", "sp_GetChoices_UsersRoles"));

            return true;
        }

        public IEnumerable<object> getList(string cListName)
        {

            if (cListName != null && cListName.Length > 0)
            {

                OptionList oListRet = this.oOptions.Find(oList => oList.cListName == cListName);
                if (oListRet != null)
                {
                    return oListRet.oItens.AsEnumerable();
                }
                else
                {
                    return new OptionList().oItens.AsEnumerable();
                }
            }

            return new OptionList().oItens.AsEnumerable();

        }

        public SelectList getSelectList(string cListName, object selectedValue)
        {
            return new SelectList(this.getList(cListName), "Id", "Text", selectedValue);
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
                    oItem = oOption.oItens.Find( item => item.Id == cId);
                    if (oItem != null)
                    {
                        return oItem.Text;
                    }
                }
            }
            return "";
        }

    }

}