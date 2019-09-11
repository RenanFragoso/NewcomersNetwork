using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NewcomersNetworkAPI.Models
{
    public class EventRegistration : NNAPIModel
    {
        public string EventId { get; set; } = "";
        public string UserId { get; set; } = "";
        public string Email { get; set; } = "";
        public string Name { get; set; } = "";
        public DateTime RegisterDate { get; set; }
        public int SlotsRequested { get; set; } = 1;
        public User oUser { get; set; } = new User();

        public EventRegistration()
        {
        }

        public override void MapFromTableRow(DataRow row)
        {
            try
            {
                this.EventId = row["EventId"].ToString();
                this.UserId = row["UserId"].ToString();
                this.Email = row["Email"].ToString();
                this.Name = row["Name"].ToString();
                this.RegisterDate = (DateTime)row["RegisterDate"];
                this.SlotsRequested = (int)row["SlotsRequested"];
            }
            catch (Exception e)
            {
                //throw;
                this.isValid = false;
                this.sMsgError.Add("Error:  Event.MapFromTableRow.");
                this.sMsgError.Add(e.Message);
                return;
            }

            this.Validate();
        }

        public override bool Validate()
        {
            if(this.EventId == null)
            {
                this.isValid = false;
                this.sMsgError.Add("Invalid Event ID.");
                return false;
            }

            if(this.EventId.Length <= 0)
            {
                this.isValid = false;
                this.sMsgError.Add("Invalid Event ID.");
                return false;
            }
            this.isValid = true;
            return true;
        }

        public void LoadUser()
        {
            if(this.UserId != null && this.UserId.Length > 0)
            {
                oUser = new User(this.UserId);
            }
        }

    }
}