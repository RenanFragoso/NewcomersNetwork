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
    public class Service : NNAPIModel
    {
        [Key]
        public string ServiceId { get; set; } = "";
        public string ServiceName { get; set; } = "";
        public string ServiceDescription { get; set; } = "";
        public string ServiceResponsible { get; set; } = "";
        public string ServiceGroup { get; set; } = "";
        public DateTime ServiceCreateDate { get; set; } = DateTime.Now;
        public DateTime ServiceAlterDate { get; set; } = DateTime.Now;

        [JsonIgnore]
        List<ServicesSchedule> ServiceSchedule;

        public Service()
        {
        }

        public Service(string cServiceId)
        {
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            infoParameters.Add("cServiceId", cServiceId);
            DataTable oServiceDB = DBConn.ExecuteCommand("sp_Services_GetByID", infoParameters).Tables[0];

            if (oServiceDB.Rows.Count > 0)
            {
                this.MapFromTableRow(oServiceDB.Rows[0]);
            }

        }

        public override bool Validate()
        {
            //Object structure validation
            if (this.ServiceId != null && this.ServiceId.Length > 0)
            {
                this.isValid = true;
                return true;
            }

            this.isValid = false;
            this.sMsgError.Add("Invalid ServiceId.");
            return false;

        }

        public override void MapFromTableRow(DataRow row)
        {
            try
            {
                this.ServiceId = row["ServiceID"].ToString();
                this.ServiceName = row["ServiceName"].ToString();
                this.ServiceDescription = row["ServiceDescription"].ToString();
                this.ServiceResponsible = row["ServiceResponsible"].ToString();
                this.ServiceGroup = row["ServiceGroup"].ToString();
                this.ServiceCreateDate = (DateTime) row["ServiceCreateDate"];
                this.ServiceAlterDate = (DateTime) row["ServiceAlterDate"];

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

            DateTime dNow = DateTime.Now;
            DataSet oInsertCMD;
            DataTable oServicesDB;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();

            if (this.isValid)
            {
                infoParameters.Add("cServiceName", this.ServiceName);
                infoParameters.Add("cServiceDescription", this.ServiceDescription);
                infoParameters.Add("cServiceResponsible", this.ServiceResponsible);
                infoParameters.Add("cServiceGroup", this.ServiceGroup);
                infoParameters.Add("dServiceCreateDate", dNow);
                infoParameters.Add("dServiceAlterDate", dNow);

                oInsertCMD = DBConn.ExecuteCommand("sp_Services_Insert", infoParameters);
                oServicesDB = oInsertCMD.Tables[0];

                if (oServicesDB.Rows[0]["LAST_SERVICE"] != null)
                {
                    this.ServiceId = oServicesDB.Rows[0]["LAST_SERVICE"].ToString();
                    this.ServiceAlterDate = dNow;
                    this.ServiceCreateDate = dNow;
                    return true;
                }
                else
                {
                    this.sMsgError.Add("Error Saving Service.");
                    return false;
                }

            }
            else
            {
                this.sMsgError.Add("Error Saving Service.");
                return false;
            }

        }

        public override bool Update()
        {
            DateTime dNow = DateTime.Now;
            DataTable oServicesDB;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();

            if (this.Validate())
            {
                infoParameters.Add("cServiceId", this.ServiceId);
                infoParameters.Add("cServiceName", this.ServiceName);
                infoParameters.Add("cServiceDescription", this.ServiceDescription);
                infoParameters.Add("cServiceResponsible", this.ServiceResponsible);
                infoParameters.Add("cServiceGroup", this.ServiceGroup);
                infoParameters.Add("dServiceAlterDate", dNow);

                oServicesDB = DBConn.ExecuteCommand("sp_Services_Update", infoParameters).Tables[0];

                return true;
            }
            else
            {
                this.sMsgError.Add("Error Updating Service.");
                return false;
            }

        }

        public override bool Delete()
        {
            DataTable oServicesDB;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();

            if (this.Validate())
            {
                infoParameters.Add("cServiceId", this.ServiceId);
                oServicesDB = DBConn.ExecuteCommand("sp_Services_Delete", infoParameters).Tables[0];

                return true;
            }
            else
            {
                this.sMsgError.Add("Error Deleting Service.");
                return false;
            }

        }

    }
}