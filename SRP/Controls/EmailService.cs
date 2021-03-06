﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Mail;
using System.Web;
using STG.SRP.DAL;

namespace STG.SRP.Core.Utilities
{
    public class EmailService
    {
        // sends email
        // Configuration:
        //      Log email to email log - Yes/No
        //      Use Template - Yes/No
        //          If Yes and no template given, use the Default Template (web.config)

        // To figure Out -- "mail merging"

        private static bool _logEmails = bool.Parse(ConfigurationManager.AppSettings[AppSettings.LogEmails.ToString()] ?? "false");
        private static bool _useTemplates = bool.Parse(ConfigurationManager.AppSettings[AppSettings.UseEmailTemplates.ToString()] ?? "false");
        public static bool UseTemplates
        {
            get
            {
                return _useTemplates;
            }
            set
            {
                _useTemplates = value;
            }
        }

        private static string _emailFrom = ConfigurationManager.AppSettings[AppSettings.DefaultEmailFrom.ToString()];
        public static string EmailFrom
        {
            get
            {
                var em1 = _emailFrom;  // webconfig
                var em2 = SRPSettings.GetSettingValue("FromEmailAddress");

                if (em2.Length == 0)
                {
                    return _emailFrom;
                }
                else
                {
                    _emailFrom = em2;
                    return em2;
                }
                
            }
            set
            {
                _emailFrom = value;
            }
        }

        private static string _defaultTemplate = ConfigurationManager.AppSettings[AppSettings.DefaultEmailTemplate.ToString()] ?? "";

        public static string EmailTemplate
        {
             get
            {
                try
                {
                    var myFile = new System.IO.StreamReader(HttpContext.Current.Server.MapPath(_defaultTemplate));
                    string myString = myFile.ReadToEnd();

                    myFile.Close();
                    return myString;
                }
                catch //(Exception ex)
                {
                    return "{CONTENT}";
                }
            }
        }


        public static bool SendEmail
            (string fromAddress, string toAddress, string subject, string body)
        {
            var mm = new MailMessage(fromAddress, toAddress);
            mm.Subject = subject;
            mm.Body = UseTemplates ? EmailTemplate.Replace("{CONTENT}", body) : body;
            mm.IsBodyHtml = true;

            var smtp = new SmtpClient();
            smtp.Send(mm);

            if (_logEmails)
            {

                var l = new EmailLog
                            {
                                SentDateTime = DateTime.Now,
                                SentFrom = fromAddress,
                                SentTo = toAddress,
                                Subject = subject,
                                Body = UseTemplates ? EmailTemplate.Replace("{CONTENT}", body) : body
                            };
                l.Insert();
            }
            return true;
        }

        public static bool SendEmail
            (string toAddress, string subject, string body)
        {
            return SendEmail(EmailFrom, toAddress,
                             subject, body);
        }

        public static bool SendEmail
            (string fromAddress, List<string> toAddress, string subject, string body)
        {
            foreach (string address in toAddress)
            {
                SendEmail(fromAddress, address, subject, body);
            }
            return true;
        }

        public static bool SendEmail
            (List<string> toAddress, string subject, string body)
        {
            foreach (string address in toAddress)
            {
                SendEmail(EmailFrom, address, subject, body);
            }
            return true;
        }


    }
}
