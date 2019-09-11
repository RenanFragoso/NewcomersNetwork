using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NewcomersNetworkAPI.Models.Widgets
{
    public class WidgetServices : NNAPIModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Link { get; set; }

        public WidgetServices()
        {
        }

        public WidgetServices(string title, string desc, string image, string link)
        {
            this.Title = title;
            this.Description = desc;
            this.Image = image;
            this.Link = link;
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
                this.Title = row["Title"].ToString();
                this.Description = row["Description"].ToString();
                this.Image = row["Image"].ToString();
                this.Link = row["Link"].ToString();
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