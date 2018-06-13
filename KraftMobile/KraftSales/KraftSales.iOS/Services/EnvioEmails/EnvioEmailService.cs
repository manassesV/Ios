﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Foundation;
using KraftSales.Services.EnvioEmails;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(KraftSales.iOS.Services.EnvioEmails.EnvioEmailService))]
namespace KraftSales.iOS.Services.EnvioEmails
{
    public class EnvioEmailService : IEnvioEmailService
    {
        public async Task SendMail(string to, string from, string subject, string text, List<string> copies)
        {
            try
            {
                var message = new MailMessage(from, to, subject, text);
                message.IsBodyHtml = true;
                if (copies != null)
                {
                    foreach (var item in copies.ToList())
                    {
                        message.Bcc.Add(item);
                    }
                }

                var client = new SmtpClient
                {
                    Host = App.MailHost,
                    Port = App.MailPort, //smtp port for SSL
                    Credentials = new System.Net.NetworkCredential(App.MailUser, App.MailPassword),
                    EnableSsl = true //for gmail SSL must be true
                };

                await Task.Run(() => client.Send(message));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}