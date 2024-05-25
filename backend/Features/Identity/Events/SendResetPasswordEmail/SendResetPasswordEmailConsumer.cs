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
        
        string htmlTemplate = $@"
            <!DOCTYPE html>
            <html>
            <head>
                <style>
                    body {{
                        background-color: #d4edda; /* Jasnozielone tło */
                        display: flex;
                        justify-content: center;
                        align-items: center;
                        height: 100vh;
                        margin: 0;
                        font-family: Arial, sans-serif;
                    }}
                    .container {{
                        text-align: center;
                    }}
                    h3 {{
                        color: #155724; /* Ciemnozielony kolor tekstu */
                    }}
                    button {{
                        background-color: #28a745; /* Zielony kolor przycisku */
                        color: white;
                        border: none;
                        padding: 10px 20px;
                        font-size: 16px;
                        cursor: pointer;
                        border-radius: 5px;
                        margin-top: 20px;
                        text-decoration: none;
                    }}
                    button a {{
                        color: white;
                        text-decoration: none;
                    }}
                    button:hover {{
                        background-color: #218838; /* Ciemniejszy zielony na hover */
                    }}
                </style>
            </head>
            <body>
                <div class=""container"">
                    <h3>Kliknij przycisk poniżej aby zmienić hasło</h3>
                    <button><a href=""{link}"" target=""_blank"">Zmień hasło</a></button>
                </div>
            </body>
            </html>";
        
        await _emailSenderService.SendEmailAsync(context.Message.Email, "Reset password", htmlTemplate);
    }
    
    private string SetUrl(string token, Guid userId)
    {
        return $"{Globals.ApplicationUrl}/reset-password?token={token}&userId={userId}";
    }
}