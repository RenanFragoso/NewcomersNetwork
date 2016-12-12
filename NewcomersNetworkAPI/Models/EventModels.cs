using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NewcomersNetworkAPI.Providers;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace NewcomersNetworkAPI.Models
{
    public class Event : NNAPIModel
    {
        [Key]
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;
        public bool Published { get; set; } = false;
        public DateTime StartPublishDate { get; set; } = DateTime.Now;
        public DateTime EndPublishDate { get; set; } = DateTime.Now;
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime AlterDate { get; set; } = DateTime.Now;
        public string Image { get; set; } = "";
        public bool Finished { get; set; } = false;
        public int MaxSlots { get; set; } = 0;
        public int CurSlots { get; set; } = 0;
        public string CreatedBy { get; set; } = "";
        public int Type { get; set; } = 0;
        public string ExternalLink { get; set; } = "";

        public EventDetails oDetails { get; set; } = new EventDetails();

        public Event()
        {
        }

        public Event(string cEventID)
        {
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            infoParameters.Add("cId", cEventID);
            DataTable oEventDB = DBConn.ExecuteCommand("sp_Events_GetByID", infoParameters).Tables[0];

            if (oEventDB.Rows.Count > 0)
            {
                this.MapFromTableRow(oEventDB.Rows[0]);
            }

        }

        public override void MapFromTableRow(DataRow row)
        {
            try
            {
                this.Id = row["Id"].ToString();
                this.Name = row["Name"].ToString();
                this.Description = row["Description"].ToString();
                this.StartDate = (DateTime) row["StartDate"];
                this.EndDate = (DateTime) row["EndDate"];
                this.Published = (bool) row["Published"];
                this.StartPublishDate = (DateTime) row["StartPublishDate"];
                this.EndPublishDate = (DateTime) row["EndPublishDate"];
                this.Image = row["Image"].ToString();
                this.Finished = (bool) row["Finished"];
                this.MaxSlots = (int) row["MaxSlots"];
                this.CurSlots = (int) row["CurSlots"];
                this.CreatedBy = row["CreatedBy"].ToString();
                this.Type = (int) row["Type"];
                this.ExternalLink = row["ExternalLink"].ToString();
                this.CreateDate = (DateTime) row["CreateDate"];
                this.AlterDate = (DateTime) row["AlterDate"];
            }
            catch ( Exception e )
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
            //Object structure validation
            if(this.Id != null && this.Id.Length > 0)
            {
                this.isValid = true;
                return true;
            }

            this.isValid = false;
            this.sMsgError.Add("Invalid EventID.");
            return false;
        }

        public override bool Save()
        {

            DataSet oInsertCMD;
            DataTable oEventDB;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            DateTime dNow = DateTime.Now;
            
            if (this.Validate())
            {
                infoParameters.Add("cName", this.Name);
                infoParameters.Add("cDescription", this.Description);
                infoParameters.Add("dStartDate", this.StartDate);
                infoParameters.Add("dEndDate", this.EndDate);
                infoParameters.Add("bPublished", this.Published);
                infoParameters.Add("dStartPublishDate", this.StartPublishDate);
                infoParameters.Add("dEndPublishDate", this.EndPublishDate);
                infoParameters.Add("bFinished", this.Finished);
                infoParameters.Add("nMaxSlots", this.MaxSlots);
                infoParameters.Add("nCurSlots", this.CurSlots);
                infoParameters.Add("cImage", this.Image);
                infoParameters.Add("cCreatedBy", this.CreatedBy);
                infoParameters.Add("nType", this.Type);
                infoParameters.Add("dCreateDate", dNow);
                infoParameters.Add("dAlterDate", dNow);

                oInsertCMD = DBConn.ExecuteCommand("sp_Events_Insert", infoParameters);
                oEventDB = oInsertCMD.Tables[0];

                if(oEventDB.Rows[0]["LAST_EVENT"] != null)
                {
                    this.Id = oEventDB.Rows[0]["LAST_EVENT"].ToString();
                    return true;
                }
                else
                {
                    this.sMsgError.Add("Error Saving event.");
                    return false;
                }
                
            }
            else
            {
                this.sMsgError.Add("Error Saving event.");
                return false;
            }
            
        }

        public override bool Update()
        {
            DataTable oEventsDB;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();

            if (this.Validate())
            {
                infoParameters.Add("cId", this.Id);
                infoParameters.Add("cName", this.Name);
                infoParameters.Add("cDescription", this.Description);
                infoParameters.Add("dStartDate", this.StartDate);
                infoParameters.Add("dEndDate", this.EndDate);
                infoParameters.Add("bPublished", this.Published);
                infoParameters.Add("dStartPublishDate", this.StartPublishDate);
                infoParameters.Add("dEndPublishDate", this.EndPublishDate);
                infoParameters.Add("bFinished", this.Finished);
                infoParameters.Add("nMaxSlots", this.MaxSlots);
                infoParameters.Add("nCurSlots", this.CurSlots);
                infoParameters.Add("cImage", this.Image);
                infoParameters.Add("cCreatedBy", this.CreatedBy);
                infoParameters.Add("nType", this.Type);
                infoParameters.Add("cExternalLink", this);
                infoParameters.Add("dAlterDate", DateTime.Now);

                oEventsDB = DBConn.ExecuteCommand("sp_Events_Update", infoParameters).Tables[0];

                if(this.oDetails != null && (this.oDetails.GetType() == typeof(EventDetails)))
                {
                    this.oDetails.Update();
                }

                return true;
            }
            else
            {
                this.sMsgError.Add("Error Updating event.");
                return false;
            }

        }

        public bool EventSign()
        {

            if(!this.Finished && this.CurSlots > 0)
            {
                this.CurSlots -= 1;
                this.Update();
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public bool LoadDetails()
        {
            EventDetails oDetails;
            if (this.Validate())
            {
                oDetails = new EventDetails(this.Id);
                if (oDetails.isValid)
                {
                    this.oDetails = oDetails;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

    }
}