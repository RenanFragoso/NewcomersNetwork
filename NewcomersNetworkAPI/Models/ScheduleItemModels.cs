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
    public class ScheduleItem : NNAPIModel
    {
        public string ScheduleId { get; set; } = "";
        public int Id { get; set; } = 0;

        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "Schedule Item Start Date is required")]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [Display(Name = "End Date")]
        [Required(ErrorMessage = "Schedule Item End Date is required")]
        public DateTime EndDate { get; set; } = DateTime.Now;

        [Display(Name = "Current Slots")]
        public int Slots { get; set; } = 0;

        [Display(Name = "Maximum Slots")]
        public int MaxSlots { get; set; } = 0;

        [Display(Name = "Unique Inscription")]
        public bool UniqueInscription { get; set; } = true;

        [Display(Name = "Require Registration")]
        public bool RequireRegister { get; set; } = true;

        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime AlterDate { get; set; } = DateTime.Now;

        public string Status { get; set; } = "";

        public ScheduleItem()
        {
        }

        public ScheduleItem(string cSchedId, int nItemId)
        {
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            infoParameters.Add("cScheduleId", cSchedId);
            infoParameters.Add("nId", nItemId);
            DataTable oSchedDB = DBConn.ExecuteCommand("sp_ScheduleItem_Get", infoParameters).Tables[0];

            if (oSchedDB.Rows.Count > 0)
            {
                this.MapFromTableRow(oSchedDB.Rows[0]);
            }
        }

        public override bool Validate()
        {
            if (this.Id > 0 && (this.ScheduleId != null && this.ScheduleId.Length > 0))
            {
                this.isValid = true;
                return true;
            }

            this.isValid = false;
            this.sMsgError.Add("Invalid Schedule ID or Item.");
            return false;
        }

        public override void MapFromTableRow(DataRow row)
        {
            try
            {
                this.Id = (int) row["Id"];
                this.ScheduleId = row["ScheduleId"].ToString();
                this.StartDate = (DateTime)row["StartDate"];
                this.EndDate = (DateTime)row["EndDate"];
                this.Slots = (int)row["Slots"];
                this.MaxSlots = (int)row["MaxSlots"];
                this.UniqueInscription = (bool) row["UniqueInscription"];
                this.RequireRegister = (bool)row["RequireRegister"];
                this.CreateDate = (DateTime)row["CreateDate"];
                this.AlterDate = (DateTime)row["AlterDate"];
                this.Status = row["Status"].ToString();
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

        public override bool Save()
        {
            DateTime dDateNow = DateTime.Now;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();

            infoParameters.Add("cScheduleId", this.ScheduleId);
            infoParameters.Add("dStartDate", this.StartDate);
            infoParameters.Add("dEndDate", this.EndDate);
            infoParameters.Add("nMaxSlots", this.MaxSlots);
            infoParameters.Add("dCreateDate", dDateNow);

            DataTable oItemData = DBConn.ExecuteCommand("sp_ScheduleItem_Insert", infoParameters).Tables[0];

            if (!oItemData.HasErrors && oItemData.Rows[0]["LAST_SCHDITM"] != null)
            {
                this.Id = (int)oItemData.Rows[0]["LAST_SCHDITM"];
                return true;
            }
            else
            {
                this.sMsgError.Add("Error Saving Schedule Item.");
                return false;
            }
        }

        public override bool Update()
        {
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();

            infoParameters.Add("nId", this.Id);
            infoParameters.Add("cScheduleId", this.ScheduleId);
            infoParameters.Add("dStartDate", this.StartDate);
            infoParameters.Add("dEndDate", this.EndDate);
            infoParameters.Add("nSlots", this.Slots);
            infoParameters.Add("nMaxSlots", this.MaxSlots);
            infoParameters.Add("dAlterDate", DateTime.Now);

            DataTable oItemData = DBConn.ExecuteCommand("sp_ScheduleItem_Update", infoParameters).Tables[0];

            if (!oItemData.HasErrors)
            {
                return true;
            }
            else
            {
                this.sMsgError.Add("Error Updating Schedule Item.");
                return false;
            }
        }

        public override bool Delete()
        {
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();

            infoParameters.Add("nId", this.Id);
            infoParameters.Add("cScheduleId", this.ScheduleId);

            DataTable oItemData = DBConn.ExecuteCommand("sp_ScheduleItem_Delete", infoParameters).Tables[0];

            if (!oItemData.HasErrors)
            {
                return true;
            }
            else
            {
                this.sMsgError.Add("Error Updating Schedule Item.");
                return false;
            }


        }

    }
}