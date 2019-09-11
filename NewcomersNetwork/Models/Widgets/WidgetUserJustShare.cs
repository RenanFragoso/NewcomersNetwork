using NewcomersNetworkAPI.Exceptions;
using NewcomersNetworkAPI.Providers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace NewcomersNetworkAPI.Models.Widgets
{
    public class WidgetUserJustShare : NNAPIModel
    {
        string UserId { get; set; } = "";
        public int Page { get; private set; } = 1;
        public int Offset { get; private set; } = 10;
        public int TotalJs { get; set; } = 0;

        public List<UserJustShareWidget> UserJustShare = new List<UserJustShareWidget>();
        
        public WidgetUserJustShare()
        {
        }

        public WidgetUserJustShare(string userId, int page)
        {
            this.UserId = userId;
            this.Page = page;
            this.LoadUserJustShare();
        }

        public WidgetUserJustShare(string userId, int page, int offset)
        {
            this.UserId = userId;
            this.Page = page;
            this.Offset = offset;
            this.LoadUserJustShare();
        }

        private void LoadUserJustShare()
        {
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            infoParameters.Add("userId", this.UserId);
            infoParameters.Add("classifieds", this.Offset);
            infoParameters.Add("page", this.Page);
            var procResult = DBConn.ExecuteCommand("sp_Widgets_JustShareUser", infoParameters);
            var tableResult = procResult.Tables[0];
            var totalResult = procResult.Tables[1];

            if (tableResult.Rows.Count > 0)
            {
                foreach (DataRow row in tableResult.Rows)
                {
                    this.MapFromTableRow(row);
                }

                if(totalResult.Rows.Count > 0)
                {
                    this.TotalJs = (int) totalResult.Rows[0]["TotalJs"];
                }

                this.Validate();
            }
        }

        public override bool Validate()
        {
            this.isValid = this.sMsgError.Count == 0;
            return this.isValid;
        }

        public override void MapFromTableRow(DataRow row)
        {
            var imageControl = new ImageControl();

            try
            {
                var newJsWidget = new UserJustShareWidget()
                {
                    Title = row["Title"].ToString(),
                    Id = row["Id"].ToString(),
                    Image = imageControl.RetrieveImage("classifieds", row["Image"].ToString()),
                    Type = row["Type"].ToString(),
                    CreateDate = (DateTime)row["CreateDate"],
                    Text = row["Text"].ToString(),
                    Category = new ClsGrpSimple(row["Category"].ToString()),
                };
                newJsWidget.LoadMessages(this.UserId);
                newJsWidget.TotalMessages = newJsWidget.Messages.Count();
                this.UserJustShare.Add(newJsWidget);
            }
            catch (Exception e)
            {
                //throw;
                this.isValid = false;
                this.sMsgError.Add("Error:  WidgetUserJustShare.MapFromTableRow.");
                this.sMsgError.Add(e.Message);
                return;
            }
        }

        public override void ValidOrBreak()
        {
            if (!this.Validate())
            {
                throw new InvalidModelException(sMsgError.ToArray());
            }
        }
    }

    public class UserJustShareWidget
    {
        public string Id { get; set; } = "";
        public string Title { get; set; } = "";
        public string Text { get; set; } = "";
        public string Link { get; set; } = "";
        public string Image { get; set; } = "";
        public string Type { get; set; } = "";
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public ClsGrpSimple Category { get; set; }
        public IEnumerable<WidgetClassifiedMessage> Messages { get; private set; } = new List<WidgetClassifiedMessage>();
        public int TotalMessages { get; set; } = 0;
        public bool HasOwnership { get; set; } = false;

        public UserJustShareWidget()
        {
        }

        public UserJustShareWidget(string classifiedId)
        {
        }
        
        public UserJustShareWidget(Classified classified)
        {
            this.Id = classified.Id;
            this.Title = classified.Title;
            this.Text = classified.Text;
            this.Link = "/justshare/" + Convert.ToBase64String(Encoding.Default.GetBytes(classified.Id));
            this.Image = classified.Image;
            this.Type = classified.Type;
            this.CreateDate = classified.CreateDate;
            this.Category = classified.oCategory;
            this.HasOwnership = classified.SameOwnership;
        }

        public void LoadMessages(string userId)
        {
            this.Messages = ClassifiedMessagesService.GetWidgetClassifiedMessages(this.Id, userId);
        }

    }

}