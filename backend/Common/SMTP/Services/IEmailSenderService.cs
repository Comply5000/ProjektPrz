﻿namespace API.Common.SMTP.Services;

public interface IEmailSenderService
{
    Task SendEmailAsync(string email, string subject, string textBody);
}