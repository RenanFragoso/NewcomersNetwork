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
    public class ServicesSchedule : NNAPIModel
    {
        [Key]
        public string Id { get; set; } = "";
        [Key]
        public string ServiceId { get; set; } = "";

        [Display(Name = "Schedule Name")]
        [Required(ErrorMessage = "Schedule Name is required.")]
        public string Name { get; set; } = "";

        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "Schedule Start Date is required.")]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [Display(Name = "End Date")]
        [Required(ErrorMessage = "Schedule End Date is required.")]
        public DateTime EndDate { get; set; } = DateTime.Now;

        [Display(Name = "Responsible Person")]
        [Required(ErrorMessage = "Schedule Responsible person is required.")]
        public string Responsible { get; set; } = "";

        [JsonIgnore]
        public string DoW { get; set; } = "";

        [Display(Name = "Enable Schedule")]
        public bool Enable { get; set; } = true;

        [Display(Name = "Schedule Description")]
        public string Description { get; set; } = "";

        [JsonIgnore]
        public int Slots { get; set; } = 0;

        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime AlterDate { get; set; } = DateTime.Now;

        public List<ScheduleItem> ScheduleItens { get; set; } = new List<ScheduleItem>();
        public User oResponsible { get; set; } = new User();

        [Display(Name = "Unique Inscription")]
        public bool UniqueInscription { get; set; } = true;

        [Display(Name = "Require Registration")]
        public bool RequireRegister { get; set; } = true;

        public string Status { get; set; } = "";

        public ServicesSchedule()
        {
        }

        public ServicesSchedule(string cServiceId, string cSchedId)
        {
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            infoParameters.Add("cScheduleId", cSchedId);
            infoParameters.Add("cServiceId", cServiceId);
            DataTable oSchedDB = DBConn.ExecuteCommand("sp_Schedule_Get", infoParameters).Tables[0];

            if (oSchedDB.Rows.Count > 0)
            {
                this.MapFromTableRow(oSchedDB.Rows[0]);
            }
        }

        public override bool Validate()
        {
            if ((this.Id != null && this.Id.Length > 0) && (this.ServiceId != null && this.ServiceId.Length > 0))
            {
                this.isValid = true;
                return true;
            }

            this.isValid = false;
            this.sMsgError.Add("Invalid Schedule ID or Service.");
            return false;
        }

        public override void MapFromTableRow(DataRow row)
        {
            try
            {
                this.Id = row["Id"].ToString();
                this.ServiceId = row["ServiceId"].ToString();
                this.Name = row["Name"].ToString();
                this.StartDate = (DateTime)row["StartDate"];
                this.EndDate = (DateTime)row["EndDate"];
                this.Responsible = row["Responsible"].ToString();
                this.DoW = row["DoW"].ToString();
                this.Enable = (bool) row["Enable"];
                this.Description = row["Description"].ToString();
                this.Slots = (int) row["Slots"];
                this.UniqueInscription = (bool)row["UniqueInscription"];
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

        public void LoadItens()
        {
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            infoParameters.Add("cScheduleId", this.Id);
            DataTable oSchedDB = DBConn.ExecuteCommand("sp_ScheduleItem_GetAll", infoParameters).Tables[0];

            if (oSchedDB.Rows.Count > 0)
            {
                ScheduleItem oSchedItem;
                foreach ( DataRow row in oSchedDB.Rows)
                {
                    oSchedItem = new ScheduleItem();
                    oSchedItem.MapFromTableRow(row);
                    if (oSchedItem.isValid)
                    {
                        this.ScheduleItens.Add(oSchedItem);
                    }
                }
            }
        }

        public override void LoadFullDetails()
        {
            this.LoadItens();
            this.oResponsible = new User(this.Responsible);
        }

        public override bool Save()
        {
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();

            infoParameters.Add("cServiceId", this.ServiceId);
            infoParameters.Add("cName", this.Name);
            infoParameters.Add("cDescription", this.Description);
            infoParameters.Add("dStartDate", this.StartDate);
            infoParameters.Add("dEndDate", this.EndDate);
            infoParameters.Add("cResponsible", this.Responsible);
            infoParameters.Add("bUniqueInscription", this.UniqueInscription);
            infoParameters.Add("bRequireRegister", this.RequireRegister);
            infoParameters.Add("dCreateDate", DateTime.Now);

            DataTable oSchedData = DBConn.ExecuteCommand("sp_Schedule_Insert", infoParameters).Tables[0];

            if (!oSchedData.HasErrors && oSchedData.Rows[0]["LAST_SCHEDULE"] != null)
            {
                this.Id = oSchedData.Rows[0]["LAST_SCHEDULE"].ToString();
                return true;
            }
            else
            {
                this.sMsgError.Add("Error Updating Item.");
                return false;
            }
        }

        public override bool Update()
        {
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();

            infoParameters.Add("cServiceId", this.ServiceId);
            infoParameters.Add("cId", this.Id);
            infoParameters.Add("cName", this.Name);
            infoParameters.Add("cDescription", this.Description);
            infoParameters.Add("dStartDate", this.StartDate);
            infoParameters.Add("dEndDate", this.EndDate);
            infoParameters.Add("cResponsible", this.Responsible);
            infoParameters.Add("bUniqueInscription", this.UniqueInscription);
            infoParameters.Add("bRequireRegister", this.RequireRegister);
            infoParameters.Add("dAlterDate", DateTime.Now);

            DataTable oSchedData = DBConn.ExecuteCommand("sp_Schedule_Update", infoParameters).Tables[0];

            if (!oSchedData.HasErrors)
            {
                return true;
            }
            else
            {
                this.sMsgError.Add("Error Updating Schedule.");
                return false;
            }
        }

        public override bool Delete()
        {
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();

            infoParameters.Add("cServiceId", this.ServiceId);
            infoParameters.Add("cScheduleId", this.Id);

            DataTable oSchedData = DBConn.ExecuteCommand("sp_Schedule_Delete", infoParameters).Tables[0];

            if (!oSchedData.HasErrors)
            {
                return true;
            }
            else
            {
                this.sMsgError.Add("Error Updating Schedule.");
                return false;
            }
        }

        public bool Deactivate()
        {
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();

            infoParameters.Add("cServiceId", this.ServiceId);
            infoParameters.Add("cScheduleId", this.Id);

            DataTable oSchedData = DBConn.ExecuteCommand("sp_Schedule_Deactivate", infoParameters).Tables[0];

            if (!oSchedData.HasErrors)
            {
                return true;
            }
            else
            {
                this.sMsgError.Add("Error Deactivating Schedule.");
                return false;
            }
        }

        public bool Activate()
        {
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();

            infoParameters.Add("cServiceId", this.ServiceId);
            infoParameters.Add("cScheduleId", this.Id);

            DataTable oSchedData = DBConn.ExecuteCommand("sp_Schedule_Activate", infoParameters).Tables[0];

            if (!oSchedData.HasErrors)
            {
                return true;
            }
            else
            {
                this.sMsgError.Add("Error Activating Schedule.");
                return false;
            }
        }


    }
}