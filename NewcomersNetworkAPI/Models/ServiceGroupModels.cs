using NewcomersNetworkAPI.Providers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NewcomersNetworkAPI.Models
{
    public class ServiceGroup : NNAPIModel
    {
        [Key]
        [Display(Name = "Group Id")]
        public string GroupId { get; set; } = "";

        [Display(Name = "Group Name")]
        [Required(ErrorMessage = "Group Name is required")]
        public string GroupName { get; set; } = "";

        [Display(Name = "Group Description")]
        public string GroupDescription { get; set; } = "";

        [Display(Name = "Group Color")]
        public string GroupColor { get; set; } = "";

        [Display(Name = "Group Icon")]
        public string GroupIcon { get; set; } = "";

        [Display(Name = "Group Status")]
        public string GroupStatus { get; set; } = "";

        public ServiceGroup()
        {
        }

        public ServiceGroup(string cGroupId)
        {
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            infoParameters.Add("cGroupId", cGroupId);
            DataTable oGroupData = DBConn.ExecuteCommand("sp_ServicesGroup_GetById", infoParameters).Tables[0];

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
                this.GroupIcon = row["GroupIcon"].ToString();
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

        public override bool Save()
        {
            DateTime dDateNow = DateTime.Now;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();

            infoParameters.Add("cName", this.GroupName);
            infoParameters.Add("cDescription", this.GroupDescription);
            infoParameters.Add("cColor", this.GroupColor);
            infoParameters.Add("cIcon", this.GroupIcon);
            infoParameters.Add("dAlterDate", dDateNow);
            infoParameters.Add("dCreateDate", dDateNow);
            
            DataTable oGroupData = DBConn.ExecuteCommand("sp_ServicesGroup_Insert", infoParameters).Tables[0];

            if (!oGroupData.HasErrors && oGroupData.Rows[0]["LAST_SERVICEGROUP"] != null)
            {
                this.GroupId = oGroupData.Rows[0]["LAST_SERVICEGROUP"].ToString();
                return true;
            }
            else
            {
                this.sMsgError.Add("Error Saving Service Group.");
                return false;
            }
        }

        public override bool Update()
        {
            DateTime dDateNow = DateTime.Now;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();

            infoParameters.Add("cId", this.GroupId);
            infoParameters.Add("cName", this.GroupName);
            infoParameters.Add("cDescription", this.GroupDescription);
            infoParameters.Add("cColor", this.GroupColor);
            infoParameters.Add("cIcon", this.GroupIcon);
            infoParameters.Add("dAlterDate", dDateNow);

            DataTable oGroupData = DBConn.ExecuteCommand("sp_ServicesGroup_Update", infoParameters).Tables[0];

            if (oGroupData.HasErrors)
            {
                this.sMsgError.Add(oGroupData.GetErrors().ToString());
                return false;
            }

            return true;
        }

        public override bool Delete()
        {
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            infoParameters.Add("cGroupId", this.GroupId);
            DataTable oGroupData = DBConn.ExecuteCommand("sp_ServicesGroup_Delete", infoParameters).Tables[0];

            if (!oGroupData.HasErrors)
            {
                return true;
            }

            return false;
        }

        public bool Deactivate()
        {
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            infoParameters.Add("cGroupId", this.GroupId);
            DataTable oGroupData = DBConn.ExecuteCommand("sp_ServicesGroup_Deactivate", infoParameters).Tables[0];

            if (!oGroupData.HasErrors)
            {
                return true;
            }

            return false;
        }

        public bool Activate()
        {
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            infoParameters.Add("cGroupId", this.GroupId);
            DataTable oGroupData = DBConn.ExecuteCommand("sp_ServicesGroup_Activate", infoParameters).Tables[0];

            if (!oGroupData.HasErrors)
            {
                return true;
            }

            return false;
        }

    }

    public class SvcGrpSimple
    {
        public string GroupName { get; set; } = "";
        public string GroupDescription { get; set; } = "";
        public string GroupColor { get; set; } = "";
        public string GroupIcon { get; set; } = "";

        public SvcGrpSimple()
        {
        }

        public SvcGrpSimple(string cGroupId)
        {
            ServiceGroup oGroup = new ServiceGroup(cGroupId);
            if (oGroup.isValid)
            {
                this.GroupName = oGroup.GroupName;
                this.GroupDescription = oGroup.GroupDescription;
                this.GroupColor = oGroup.GroupColor;
                this.GroupIcon = oGroup.GroupIcon;
            }
        }

    }

}