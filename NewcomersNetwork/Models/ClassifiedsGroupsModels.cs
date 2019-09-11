using NewcomersNetworkAPI.Providers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NewcomersNetworkAPI.Models
{
    public class ClassifiedsGroups : NNAPIModel
    {
        public string Id { get; set; } = "NULLID";
        public string GroupName { get; set; } = "";
        public string GroupDescription { get; set; } = "";
        public string GroupColor { get; set; } = "";
        public string GroupIcon { get; set; } = "";
        public string GroupStatus { get; set; } = "";
        public string TextColor { get; set; } = "";
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime AlterDate { get; set; } = DateTime.Now;

        public ClassifiedsGroups()
        {
        }

        public ClassifiedsGroups(string cGroupId)
        {
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            infoParameters.Add("cId", cGroupId);
            DataTable oGroupsDB = DBConn.ExecuteCommand("sp_ClassifiedsGroups_GetByID", infoParameters).Tables[0];

            if (oGroupsDB.Rows.Count > 0)
            {
                this.MapFromTableRow(oGroupsDB.Rows[0]);
            }

        }

        public override void MapFromTableRow(DataRow row)
        {
            try
            {
                this.Id = row["Id"].ToString();
                this.GroupName = row["GroupName"].ToString();
                this.GroupDescription = row["GroupDescription"].ToString();
                this.GroupColor = row["GroupColor"].ToString();
                this.GroupIcon = row["GroupIcon"].ToString();
                this.GroupStatus = row["GroupStatus"].ToString();
                this.TextColor = row["TextColor"].ToString();
                this.CreateDate = (DateTime)row["CreateDate"];
                this.AlterDate = (DateTime)row["AlterDate"];
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
            if (this.Id.Length == 0)
            {
                this.isValid = false;
                this.sMsgError.Add("Invalid Group ID.");
                return false;
            }

            this.isValid = true;
            return true;
        }

    }

    public class ClsGrpSimple
    {
        public string GroupName { get; set; } = "";
        public string GroupDescription { get; set; } = "";
        public string GroupColor { get; set; } = "";
        public string GroupIcon { get; set; } = "";
        public string TextColor { get; set; } = "";

        public ClsGrpSimple()
        {
        }

        public ClsGrpSimple(string cGroupId)
        {
            ClassifiedsGroups oGroup = new ClassifiedsGroups(cGroupId);
            if (oGroup.isValid)
            {
                this.GroupName = oGroup.GroupName;
                this.GroupDescription = oGroup.GroupDescription;
                this.GroupColor = oGroup.GroupColor;
                this.GroupIcon = oGroup.GroupIcon;
                this.TextColor = oGroup.TextColor;
            }
        }

    }

}