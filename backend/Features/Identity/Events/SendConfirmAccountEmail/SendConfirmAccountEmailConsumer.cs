using API.Common.SMTP.Services;
using MassTransit;

namespace API.Features.Identity.Events.SendConfirmAccountEmail;

public sealed class SendConfirmAccountEmailConsumer : IConsumer<SendConfirmAccountEmailEvent>
{
    private readonly IEmailSenderService _emailSenderService;

    public SendConfirmAccountEmailConsumer(IEmailSenderService emailSenderService)
    {
        _emailSenderService = emailSenderService;
    }
    
    public async Task Consume(ConsumeContext<SendConfirmAccountEmailEvent> context)
    {
        var link = SetUrl(context.Message);
        
        var message = "Click link bellow to activate your account" + Environment.NewLine + link;
        await _emailSenderService.SendEmailAsync(context.Message.Email, "Activate your account", message);
    }
    
    private string SetUrl(SendConfirmAccountEmailEvent notification)
    {
        return $"/confirm-account?token={notification.Token}&userId={notification.UserId}";
    }
}