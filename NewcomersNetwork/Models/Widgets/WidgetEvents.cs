using NewcomersNetworkAPI.Models.Image;
using NewcomersNetworkAPI.Providers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace NewcomersNetworkAPI.Models.Widgets
{
    public class WidgetEvents : NNAPIModel
    {
        private string Id { get; set; } = "";
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string Image { get { return ImageContainer.RetrieveImagePath("events", this.Id); } }
        public string Link { get { return "/event/" + Convert.ToBase64String(Encoding.Default.GetBytes(this.Id)); } }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;

        public WidgetEvents()
        {
        }

        public WidgetEvents(string title, string desc, string image)
        {
            this.Title = title;
            this.Description = desc;
        }

        public WidgetEvents(string id, string title, string desc, DateTime startDate, DateTime endDate)
        {
            this.Id = id;
            this.Title = title;
            this.Description = desc;
            this.StartDate = startDate;
            this.EndDate = endDate;
        }

        public WidgetEvents(DataRow row)
        {
            this.MapFromTableRow(row);
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
                this.Id = row["Id"].ToString();
                this.Title = row["Title"].ToString();
                this.Description = row["Description"].ToString();
                this.StartDate = (DateTime)row["StartDate"];
                this.EndDate = (DateTime)row["EndDate"];
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