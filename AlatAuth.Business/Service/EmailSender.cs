using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace AlatAuth.Business.Service
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;


        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            await Execute(email, subject, htmlMessage);
        }
        public Task Execute(string email, string subject, string body)
        {
            string smtpServer = _configuration["Settings:Smtp:smtpServer"];
            int smtpPort = _configuration.GetValue<int>("Settings:Smtp:smtpPort");
            string smtpUsername = _configuration["Settings:Smtp:smtpUsername"];
            string smtpPassword = _configuration["Settings:Smtp:smtpPassword"];
            string emailSender = _configuration["Settings:Smtp:emailSender"];

            using (var client = new SmtpClient(smtpServer, smtpPort))
            {
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;


                var mailMessage = new MailMessage
                {
                    From = new MailAddress(emailSender),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                };

                mailMessage.To.Add(email);
                try
                {
                    client.Send(mailMessage);
                    Console.WriteLine("Email sent successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error sending email: {ex.Message}");
                }
            }

            return Task.CompletedTask;
        }

    }
}