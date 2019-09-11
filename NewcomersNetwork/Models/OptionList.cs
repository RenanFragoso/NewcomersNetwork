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
        public string ListName { get; set; } = "";
        [JsonIgnore]
        public string Source { get; set; } = "";
        public List<OptionItem> Itens { get; set; } = new List<OptionItem>();

        public OptionList()
        {
        }

        public OptionList(string Name, string Source)
        {
            this.ListName = Name;
            this.Source = Source;

            if (Source != null && Source.Length > 0)
            {
                DataTable ListData = DBConn.ExecuteCommand(Source, null).Tables[0];

                if (!ListData.HasErrors)
                {
                    foreach (DataRow row in ListData.Rows)
                    {
                        this.Itens.Add(new OptionItem (row["id"].ToString(), row["text"].ToString() ));
                    }
                }
                else
                {
                    this.sMsgError.Add(ListData.GetErrors().ToString());
                }
            }
        }

        public string getValue(string cId)
        {
            OptionItem Item;
            if (cId != null && cId.Length > 0)
            {
                Item = this.Itens.Find(item => item.id == cId);
                if (Item != null)
                {
                    return Item.text;
                }
            }
            return "";
        }
    }

    public class OptionItem
    {

        public string id { get; set; } = "";
        public string text { get; set; } = "";

        public OptionItem()
        {
        }

        public OptionItem(string cId, string cText)
        {
            this.id = cId;
            this.text = cText;
        }


    }


}