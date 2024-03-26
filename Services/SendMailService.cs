using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;

namespace website_shopping.Services
{
    public class MailSettings
    {
        public string Mail { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }
    public class SendMailService : IEmailSender
    {
        public readonly MailSettings _mailSettings;
        public readonly ILogger<SendMailService> _logger;
        public SendMailService(IOptions<MailSettings> mailSettings, ILogger<SendMailService> logger)
        {
            _mailSettings = mailSettings.Value;
            _logger = logger;

        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var msg = new MimeMessage();
            msg.Sender = new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail);
            msg.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail));
            msg.To.Add(MailboxAddress.Parse(email));
            msg.Subject = subject;

            var builder = new BodyBuilder();
            builder.HtmlBody = htmlMessage;
            msg.Body = builder.ToMessageBody();

            using var stmp = new MailKit.Net.Smtp.SmtpClient();

            try
            {
                stmp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                stmp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                await stmp.SendAsync(msg);
            }
            catch (Exception e)
            {
                //* gửi mail thất bại, nội dung mail sẽ được lưu vào thư mục mailssave
                System.IO.Directory.CreateDirectory("mailssave");
                var emailSaveFile = string.Format(@"mailssave/{0}.eml", Guid.NewGuid());

                await msg.WriteToAsync(emailSaveFile);
                _logger.LogInformation(e.Message);
                _logger.LogError(e.Message);
            }
            stmp.Disconnect(true);
            _logger.LogInformation("send mail to " + email);
        }
    }
}