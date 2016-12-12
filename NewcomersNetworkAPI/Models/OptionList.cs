using NewcomersNetworkAPI.Providers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace NewcomersNetworkAPI.Models
{
    public class OptionList : NNAPIModel
    {
        public string cListName { get; set; } = "";

        [JsonIgnore]
        public string cSource { get; set; } = "";
        public List<OptionItem> oItens { get; set; } = new List<OptionItem>();

        public OptionList()
        {
        }

        public OptionList(string cName, string cSource)
        {
            this.cListName = cName;
            this.cSource = cSource;

            if (cSource != null && cSource.Length > 0)
            {
                DataTable oListData = DBConn.ExecuteCommand(cSource, null).Tables[0];

                if (!oListData.HasErrors)
                {
                    foreach (DataRow row in oListData.Rows)
                    {
                        this.oItens.Add(new OptionItem (row["Id"].ToString(), row["Text"].ToString() ));
                    }
                }
                else
                {
                    this.sMsgError.Add(oListData.GetErrors().ToString());
                }
            }
        }

        public string getValue(string cId)
        {
            OptionItem oItem;
            if (cId != null && cId.Length > 0)
            {
                oItem = this.oItens.Find(item => item.Id == cId);
                if (oItem != null)
                {
                    return oItem.Text;
                }
            }
            return "";
        }

    }

    public class OptionItem
    {

        public string Id { get; set; } = "";
        public string Text { get; set; } = "";

        public OptionItem()
        {
        }

        public OptionItem(string cId, string cText)
        {
            this.Id = cId;
            this.Text = cText;
        }


    }


}