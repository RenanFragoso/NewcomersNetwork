using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace NewcomersNetworkAPI.Models.Widgets
{
    public class WidgetServiceBlock : NNAPIModel
    {
        private string _Id { get; set; } = "";
        public int Position { get; set; }
        public string BlockColor { get; set; }
        public string Text { get; set; }
        public string Icon { get; set; }
        public string Link { get { return "/services/" + Convert.ToBase64String(Encoding.Default.GetBytes(_Id)); } }

        public WidgetServiceBlock()
        {
        }

        public WidgetServiceBlock(string id, int pos, string color, string text, string icon)
        {
            this._Id = id;
            this.Position = pos;
            this.BlockColor = color;
            this.Text = text;
            this.Icon = icon;
        }

        public override bool Validate()
        {
            this.isValid = true;
            return true;
        }

        public override void MapFromTableRow(DataRow row)
        {
            try
            {
                this._Id = row["Id"].ToString();
                this.Position = (int) row["Position"];
                this.BlockColor = row["BlockColor"].ToString();
                this.Text = row["Text"].ToString();
                this.Icon = row["Icon"].ToString();
            }
            catch (Exception e)
            {
                //throw;
                this.isValid = false;
                this.sMsgError.Add("Error:  MapFromTableRow.");
                this.sMsgError.Add(e.Message);
                return;
            }

            this.Validate();
        }

    }
}