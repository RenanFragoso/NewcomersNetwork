using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NewcomersNetworkAPI.Models.Widgets
{
    public class WidgetTestimonials : NNAPIModel
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Testimonial { get; set; }
        public string Link { get; set; }

        public WidgetTestimonials()
        {
        }

        public WidgetTestimonials(string author, string title, string image, string testimonial, string link)
        {
            this.Author = author;
            this.Title = title;
            this.Image = image;
            this.Testimonial = testimonial;
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
                this.Author = row["Author"].ToString();
                this.Title = row["Title"].ToString();
                this.Image = row["Image"].ToString();
                this.Testimonial = row["Testimonial"].ToString();
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