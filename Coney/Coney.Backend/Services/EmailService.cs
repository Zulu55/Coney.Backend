﻿using MimeKit;
using Microsoft.Extensions.Options;

namespace Coney.Backend.Services
{
    public class EmailService
    {
        private readonly MailSettings _mailSettings;

        public EmailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendEmailAsync(string toEmail)
        {
            var verificationLink = $"https://localhost:7104/api/Users/verifyUser?userEmail={toEmail}";
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("From Coney Support", _mailSettings.From));
            emailMessage.To.Add(new MailboxAddress("", toEmail));
            emailMessage.Subject = _mailSettings.SubjectConfirmationEs;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = string.Format(_mailSettings.BodyConfirmationEs, verificationLink)
            };
            emailMessage.Body = bodyBuilder.ToMessageBody();

            using (var smtpClient = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    await smtpClient.ConnectAsync(_mailSettings.Server, _mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
                    await smtpClient.AuthenticateAsync(_mailSettings.From, _mailSettings.Password);
                    await smtpClient.SendAsync(emailMessage);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al enviar el correo: {ex.Message}");
                }
                finally
                {
                    await smtpClient.DisconnectAsync(true);
                }
            }
        }
    }
}