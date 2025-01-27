﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SultanSklepBackend.Utilities
{
    public static class Helper
    {
        #region Email Sender


        public static bool SendEmail(string userEmail, string msgArea, string subject)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("nesib7474@gmail.com");
            mailMessage.To.Add(new MailAddress(userEmail));

            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = msgArea;

            SmtpClient client = new SmtpClient();
            client.Credentials = new NetworkCredential("nesib7474@gmail.com", "tgqkolgckmexrxxs");
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            try
            {
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                // log exception
            }
            return false;
        }


        #endregion
    }

    #region Roles

    public enum UserRoles
    {
        Admin,
        User
    }

    #endregion
}
