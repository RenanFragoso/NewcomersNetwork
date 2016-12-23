using NewcomersNetworkAPI.Providers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace NewcomersNetworkAPI.Models
{
    public class ServiceGroup : NNAPIModel
    {
        [Key]
        public string GroupId { get; set; } = "";
        public string GroupName { get; set; } = "";
        public string GroupDescription { get; set; } = "";
        public string GroupColor { get; set; } = "";
        public string GroupStatus { get; set; } = "";

        public ServiceGroup()
        {
        }

        public ServiceGroup(string cGroupId)
        {
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            infoParameters.Add("cGroupId", cGroupId);
            DataTable oGroupData = DBConn.ExecuteCommand("sp_ServicesGroup_Get", infoParameters).Tables[0];

            if (!oGroupData.HasErrors && oGroupData.Rows.Count > 0)
            {
                this.MapFromTableRow(oGroupData.Rows[0]);
            }

        }
        
        public override void MapFromTableRow(DataRow row)
        {
            try
            {
                this.GroupId = row["GroupId"].ToString();
                this.GroupName = row["GroupName"].ToString();
                this.GroupDescription = row["GroupDescription"].ToString();
                this.GroupColor = row["GroupColor"].ToString();
                this.GroupStatus = row["GroupStatus"].ToString();
            }
            catch (Exception e)
            {
                this.isValid = false;
                this.sMsgError.Add("Error:  ServiceGroup.MapFromTableRow().");
                this.sMsgError.Add(e.Message);
                return;
            }

            this.Validate();

        }

        public override bool Validate()
        {
            if (this.GroupId.Length == 0)
            {
                this.isValid = false;
                this.sMsgError.Add("Invalid Group ID.");
                return false;
            }

            this.isValid = true;
            return true;
        }


    }
}