using API.Common.SMTP.Config;
using MailKit.Net.Smtp;
using MimeKit;

namespace API.Common.SMTP.Services;

public class EmailSenderService : IEmailSenderService
{
    private readonly SmtpConfig _smtpConfig;

    public EmailSenderService(SmtpConfig smtpConfig)
    {
        _smtpConfig = smtpConfig;
    }
    
    public async Task SendEmailAsync(string email, string subject, string textBody)
    {
        var emailMessage = CreateEmailMessage(email, subject, textBody);
        using var client = new SmtpClient();
        {
            await client.ConnectAsync(_smtpConfig.SmtpUrl, _smtpConfig.SmtpPort,false);
            await client.AuthenticateAsync(_smtpConfig.SmtpLogin, _smtpConfig.SmtpPassword);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }
    
    private MimeMessage CreateEmailMessage(string email, string subject, string textBody)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress(_smtpConfig.SmtpSenderName, _smtpConfig.SmtpSenderMail));
        emailMessage.To.Add(new MailboxAddress("", email));
        emailMessage.Subject = subject;
        
        var bodyBuilder = new BodyBuilder
        {
            TextBody = textBody,
            HtmlBody = textBody
        };
        emailMessage.Body = bodyBuilder.ToMessageBody();

        return emailMessage;
    }
}