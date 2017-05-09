using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace NewcomersNetworkAPI.Providers
{
    public sealed class NNSMTPSender
    {
        private static volatile NNSMTPSender instance;
        private static object syncRoot = new Object();

        private SmtpClient smtpClient;
        private string MailTitle = "";
        private string SenderMail = "";

        private NNSMTPSender()
        {
            this.MailTitle = ConfigurationManager.AppSettings["MailTitle"].ToString();
            this.SenderMail = ConfigurationManager.AppSettings["MailServerEmailAddress"].ToString();

            this.smtpClient = new SmtpClient(ConfigurationManager.AppSettings["MailSmtpServer"].ToString(), int.Parse(ConfigurationManager.AppSettings["MailSmtpServerPort"].ToString()));
            this.smtpClient.Credentials = new NetworkCredential(this.SenderMail, ConfigurationManager.AppSettings["MailServerEmailPW"].ToString()); 
            //this.smtpClient.UseDefaultCredentials = true;
            this.smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            this.smtpClient.EnableSsl = true;
        }

        public static NNSMTPSender Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new NNSMTPSender();
                    }
                }

                return instance;
            }
        }

        public static void Init()
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                        instance = new NNSMTPSender();
                }
            }
        }


        //Fire and forget
        public async Task SendMailAsync(string cMessage, string cTitle, string cMailTo, string cMailCC, bool bSendHtml = false)
        {
            MailMessage oMail = new MailMessage();

            //Setting From , To and CC
            oMail.From = new MailAddress(this.SenderMail, this.MailTitle);

            //Mail TO:
            foreach (string toAddr in cMailTo.Split(';'))
            {
                if(toAddr.Length > 0)
                    oMail.To.Add(new MailAddress(toAddr));
            }

            foreach (string ccAddr in cMailCC.Split(';'))
            {
                if(ccAddr.Length > 0)
                    oMail.CC.Add(new MailAddress(ccAddr));
            }

            oMail.Subject = cTitle;
            oMail.Body = cMessage;
            oMail.IsBodyHtml = bSendHtml;

            this.smtpClient.SendAsync(oMail, new Object());
        }

        public bool SendMail(string cMessage, string cTitle, string cMailTo, string aMailCC, bool bSendHtml = false)
        {

            MailMessage oMail = new MailMessage();

            //Setting From , To and CC
            oMail.From = new MailAddress(this.SenderMail, this.MailTitle);

            //Mail TO:
            foreach (string toAddr in cMailTo.Split(';'))
            {
                if(toAddr.Length > 0)
                    oMail.To.Add(new MailAddress(toAddr));
            }

            foreach (string ccAddr in aMailCC.Split(';'))
            {
                if (ccAddr.Length > 0)
                    oMail.CC.Add(new MailAddress(ccAddr));
            }

            oMail.Subject = cTitle;
            oMail.Body = cMessage;
            oMail.IsBodyHtml = bSendHtml;

            try
            {
                this.smtpClient.Send(oMail);
            }
            catch(Exception error)
            {
                return false;
            }


            return true;
        }

    }

}