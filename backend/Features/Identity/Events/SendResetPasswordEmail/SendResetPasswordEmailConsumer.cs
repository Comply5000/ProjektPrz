using API.Common;
using API.Common.MessageBroker.Services;
using API.Common.SMTP.Services;
using MassTransit;

namespace API.Features.Identity.Events.SendResetPasswordEmail;

public sealed class SendResetPasswordEmailConsumer : IConsumer<SendResetPasswordEmailEvent>
{
    private readonly IEmailSenderService _emailSenderService;

    public SendResetPasswordEmailConsumer(IEmailSenderService emailSenderService)
    {
        _emailSenderService = emailSenderService;
    }
    
    public async Task Consume(ConsumeContext<SendResetPasswordEmailEvent> context)
    {
        var link = SetUrl(context.Message.Token, context.Message.UserId);
        var message = "Click link bellow reset your password" + Environment.NewLine + link;
        
        await _emailSenderService.SendEmailAsync(context.Message.Email, "Reset password", message);
    }
    
    private string SetUrl(string token, Guid userId)
    {
        return $"{Globals.ApplicationUrl}/reset-password?token={token}&userId={userId}";
    }
}