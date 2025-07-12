using App.Shared.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace App.Shared.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendNewContactNotificationAsync(string webUser)
        {
            using var client = GetSmtpClient();
            var senderEmail = _configuration["NetworkCredentials:Email"];
            var toUser = _configuration["NetworkCredentials:ContactReceiver"];
            var adminPanelUrl = _configuration["WebUrls:AdminUrl"];

            var mailMessage = new MailMessage
            {
                From = new MailAddress(senderEmail),
                Subject = "Test",
                Body = "Test",
                IsBodyHtml = true
            };

            mailMessage.To.Add(toUser);
            await client.SendMailAsync(mailMessage);
        }

        private SmtpClient GetSmtpClient()
        {
            var email = _configuration["NetworkCredentials:Email"];
            var appKey = _configuration["NetworkCredentials:AppKey"];

            return new SmtpClient("smtp.gmail.com", 587)
            {
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true,
                Credentials = new NetworkCredential(email, appKey)
            };
        }
    }
}
