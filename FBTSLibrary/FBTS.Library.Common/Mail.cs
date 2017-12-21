using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace FBTS.Library.Common
{
    internal class Email
    {
           private readonly string _smtpServer;

        public Email(String smtpServer)
        {
            _smtpServer = smtpServer;
        }

        public List<string> Attachments = new List<string>();

     
        public string From
        {
            get;
            set;
        }
        public string To
        {
            get;
            set;
        }

        public string CC
        {
            get;
            set;
        }

        public string Subject
        {
            get;
            set;
        }
        public string Body
        {
            get;
            set;
        }


        /// <summary>
        /// Creates a Mail object and sends the email if the SMTPServer variable is not
        /// empty or null
        /// </summary>
        public void SendEmail()
        {
            // Don't attempt an email if there is no smtp server
            if (!string.IsNullOrEmpty(_smtpServer))
            {
                // Create Mail object
                using (var objMail = new MailMessage())
                {
                   
                        // Set properties needed for the email
                        objMail.From = new MailAddress(From);
                        objMail.To.Add(new MailAddress(To));


                        if (!string.IsNullOrEmpty(CC))
                        {
                            objMail.CC.Add(new MailAddress(CC));
                        }

                        objMail.Subject = ((string.IsNullOrEmpty(Subject) ? string.Empty : Subject));



                        objMail.Body = ((string.IsNullOrEmpty(Body) ? string.Empty : Body));


                        if (Attachments.Count > 0)
                        {
                            foreach (string attachment in Attachments)
                            {
                                var mailAttachment = new Attachment(attachment.Trim());
                                objMail.Attachments.Add(mailAttachment);
                            }
                        }

                        // Set the mail object's smtpserver property
                        var client = new SmtpClient(_smtpServer);
                        client.Send(objMail);
                    } 
            }
            else
                throw new Exception("Cannot send mail if no smtpserver is specified");
        }

    }  
}