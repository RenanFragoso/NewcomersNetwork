using NewcomersNetworkAPI.Exceptions;
using NewcomersNetworkAPI.Providers;
using NewcomersNetworkAPI.ValidationAttributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace NewcomersNetworkAPI.Models
{

    public class ClassifiedMessages : NNAPIModel
    {
        public List<ClassifiedMessage> oMessages = new List<ClassifiedMessage>();
        public ClassifiedMessages()
        {
        }

        public void LoadMessagesFrom(string cFromId)
        {
            ClassifiedMessage oMessage;
            DataTable oMessagesDB;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            infoParameters.Add("cFrom", cFromId);

            try
            {
                oMessagesDB = DBConn.ExecuteCommand("sp_AllMessages_GetFrom", infoParameters).Tables[0];
                if (!oMessagesDB.HasErrors)
                {
                    foreach (DataRow oRow in oMessagesDB.Rows)
                    {
                        oMessage = new ClassifiedMessage();
                        oMessage.MapFromTableRow(oRow);
                        if (oMessage.isValid)
                        {
                            if(oMessage.ReplyTo > 0)
                            {
                                this.AddReplyTo(oMessage);
                            }
                            else
                            {
                                this.oMessages.Add(oMessage);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                this.sMsgError.Add("Error getting messages.");
                this.sMsgError.Add(e.Message);
            }
        }

        public void Add(ClassifiedMessage oMessage)
        {
            this.oMessages.Add(oMessage);
        }

        public void AddReplyTo(ClassifiedMessage oMessageReply)
        {
            ClassifiedMessage oMessage = this.oMessages.Find(msg => msg.Id == oMessageReply.ReplyTo);
            if(oMessage != null)
            {
                oMessage.AddReply(oMessageReply);
            }
        }
    }

    public class ClassifiedMessage : NNAPIModel
    {
        public int Id { get; set; } = 0;
        public string ClassifiedId { get; set; } = "";
        public string From { get; set; } = "";
        public string To { get; set; } = "";
        public DateTime Date { get; set; } = DateTime.Now;
        public DateTime AlterDate { get; set; } = DateTime.Now;
        public string Message { get; set; } = "";
        public string Status { get; set; } = "";
        public int ReplyTo { get; set; } = 0;


        public UserSimple oFrom { get; set; } = new UserSimple();
        public UserSimple oTo { get; set; } = new UserSimple();
        public List<ClassifiedMessage> oReply { get; set; } = new List<ClassifiedMessage>();

        public ClassifiedMessage()
        {
        }

        public ClassifiedMessage(string cClassifiedId, int nId)
        {
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            infoParameters.Add("cClassifiedId", cClassifiedId);
            infoParameters.Add("nId", nId);
            DataTable oMsgData = DBConn.ExecuteCommand("sp_Messages_Get", infoParameters).Tables[0];

            if (oMsgData.Rows.Count > 0)
            {
                this.MapFromTableRow(oMsgData.Rows[0]);
            }
        }

        public ClassifiedMessage(SecureClassifiedMessage fromMessage)
        {
            if(fromMessage != null)
            {
                Dictionary<string, object> infoParameters = new Dictionary<string, object>();
                infoParameters.Add("classifiedId", Encoding.UTF8.GetString(Convert.FromBase64String(fromMessage.ClassifiedId)));
                DataTable msgData = DBConn.ExecuteCommand("sp_Message_FromClassified", infoParameters).Tables[0];

                if (msgData.Rows.Count > 0)
                {
                    this.MapFromTableRow(msgData.Rows[0]);
                }
            }
        }

        public override void MapFromTableRow(DataRow row)
        {
            try
            {
                this.Id = (int) row["Id"];
                this.ClassifiedId = row["ClassifiedId"].ToString();
                this.Date = (DateTime)row["Date"];
                this.To = row["To"].ToString();
                this.From = row["From"].ToString();
                this.Message = row["Message"].ToString();
                this.AlterDate = (DateTime)row["AlterDate"];
                this.Status = row["Status"].ToString();
                this.ReplyTo = (int) row["ReplyTo"];

                    this.oFrom = new UserSimple(this.From);

                if (this.To != null && this.To != string.Empty)
                    this.oTo = new UserSimple(this.To);
            }
            catch (Exception e)
            {
                //throw;
                this.isValid = false;
                this.sMsgError.Add("Error:  ClassifiedMessage.MapFromTableRow.");
                this.sMsgError.Add(e.Message);
                if (this.From != null && this.From != string.Empty)
                return;
            }

            this.Validate();
        }

        public override bool Validate()
        {
            //Object structure validation
            if (this.ClassifiedId == "" )
            {
                this.isValid = false;
                this.sMsgError.Add("Invalid JustShare Id.");
                return false;
            }

            if(this.From == "")
            {
                this.isValid = false;
                this.sMsgError.Add("Invalid Sender Id.");
                return false;
            }

            this.isValid = true;
            return true;
        }

        public override bool Save()
        {
            DataSet insertCMD;
            DateTime now = DateTime.Now;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>
            {
                { "classifiedId", this.ClassifiedId },
                { "from", this.From },
                { "to", this.To },
                { "message", this.Message },
                { "date", now },
                { "alterDate", now }
            };

            try
            {
                insertCMD = DBConn.ExecuteCommand("sp_Messages_Save", infoParameters);
                if (!insertCMD.HasErrors)
                {
                    this.Id = Int32.Parse(insertCMD.Tables[0].Rows[0]["LAST_MESSAGE"].ToString());
                    this.AlterDate = now;
                    this.Date = now;
                }
                else
                {
                    this.sMsgError.Add("Error saving message, try again later.");
                    return false;
                }

                return true;
            }
            catch (Exception error)
            {
                this.sMsgError.Add(error.Message);
                throw error;
            }
        }

        public void AddReply(ClassifiedMessage oMessage)
        {
            this.oReply.Add(oMessage);
        }

        public override void ValidOrBreak()
        {
            if (!this.Validate())
            {
                throw new InvalidModelException(sMsgError.ToArray());
            }
        }

    }

    public class SecureClassifiedMessage
    {
        public int Id { get; set; } = 0;
        [Required]
        public string ClassifiedId { get; set; }
        public int ResponseTo { get; set; } = 0;
        [Required]
        [CheckBlacklistedWords]
        public string Message { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Captcha { get; set; } = "";

        public SecureClassifiedMessage()
        {
        }

        public SecureClassifiedMessage(ClassifiedMessage fromMessage)
        {
            this.Id = fromMessage.Id;
            this.Date = fromMessage.Date;
            this.Message = fromMessage.Message;
        }
    }

    public class WidgetClassifiedMessage
    {
        public int Id { get; set; } = 0;
        public string ClassifiedId { get; set; }
        public int ResponseTo { get; set; } = 0;
        public string Message { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public bool IsOwner { get; set; } = false;
        public string MessageFrom { get; set; } = "";
        public List<WidgetClassifiedMessage> Responses { get; set; } = new List<WidgetClassifiedMessage>();
        [JsonIgnore]
        private int Depth { get; set; } = 0;

        public WidgetClassifiedMessage()
        {
        }

        public void AddMessage(WidgetClassifiedMessage message)
        {
            message.IncDepth();
            this.Responses.ToList().Add(message);
        }

        public void IncDepth()
        {
            this.Depth++;
        }

    }

    public static class ClassifiedMessagesService
    {

        public static IEnumerable<WidgetClassifiedMessage> GetWidgetClassifiedMessages(string classifiedId, string userId)
        {
            var messages = new List<WidgetClassifiedMessage>();

            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            infoParameters.Add("classifiedId", classifiedId);

            DataTable msgData = DBConn.ExecuteCommand("sp_Widgets_Messages_FromClassified", infoParameters).Tables[0];

            if (msgData.Rows.Count > 0)
            {
                foreach(DataRow row in msgData.Rows)
                {
                    var msgFrom = row["From"].ToString();

                    var message = new WidgetClassifiedMessage
                    {
                        ClassifiedId = row["ClassifiedId"].ToString(),
                        Id = (int)row["Id"],
                        ResponseTo = (int)row["ReplyTo"],
                        Message = row["Message"].ToString(),
                        Date = (DateTime)row["Date"],
                        MessageFrom = (msgFrom == userId ? "You" : UserDetailsService.RetFullName(msgFrom)),
                        IsOwner = msgFrom == userId
                    };

                    if (message.ResponseTo == 0)
                    {
                        messages.Add(message);
                    }
                    else
                    {
                        var msgFound = messages.Find(m => m.Id == message.ResponseTo);
                        if (msgFound == null)
                        {
                            messages.Add(message);
                        }
                        else
                        {
                            msgFound.Responses.Add(message);
                        }

                    }
                }
            }

            return messages;
        }

    }
}