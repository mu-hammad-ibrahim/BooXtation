using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;

namespace BooXtation.Helpers
{
    public class EmailService
    {
        private readonly SmtpSettings _smtpSettings;

        public EmailService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("BooXtation", _smtpSettings.UserName));
            message.To.Add(new MailboxAddress(to,to));
            message.Subject = subject;

            message.Body = new TextPart("html")
            {
                Text = body
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_smtpSettings.Host, _smtpSettings.Port, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_smtpSettings.UserName, _smtpSettings.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }

        public void SendConfirmationEmail(string recipientEmail, string recipientName, string confirmationCode)
        {
            // Create a new email message
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("BooXtation", _smtpSettings.UserName));
            message.To.Add(new MailboxAddress(recipientName, recipientEmail));
            message.Subject = "Confirmation Email";

            // Email body
            message.Body = new TextPart("plain")
            {
                Text = $"Hi {recipientName},\n\n" +
                       "Thank you for your registration!\n\n" +
                       "Please confirm your email address by entering the following confirmation code:\n\n" +
                       $"**{confirmationCode}**\n\n" +
                       "If you did not initiate this request, please ignore this email.\n\n" +
                       "Best regards,\n" +
                       "BooXtation"
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_smtpSettings.Host, _smtpSettings.Port, SecureSocketOptions.StartTls);
                    client.Authenticate(_smtpSettings.UserName, _smtpSettings.Password);

                    // Send the email
                    client.Send(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
                finally
                {
                    client.Disconnect(true);
                }
            }
        }

    }
}
