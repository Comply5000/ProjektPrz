using API.Common;
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

        string htmlTemplate = @"
        <!DOCTYPE html>
        <html>
        <head>
            <style>
                body {
                    background-color: #d4edda; /* Jasnozielone tło */
                    display: flex;
                    justify-content: center;
                    align-items: center;
                    height: 100vh;
                    margin: 0;
                    font-family: Arial, sans-serif;
                }
                .container {
                    text-align: center;
                }
                h3 {
                    color: #155724; /* Ciemnozielony kolor tekstu */
                }
                button {
                    background-color: #28a745; /* Zielony kolor przycisku */
                    color: white;
                    border: none;
                    padding: 10px 20px;
                    font-size: 16px;
                    cursor: pointer;
                    border-radius: 5px;
                    margin-top: 20px;
                    text-decoration: none;
                }
                button a {
                    color: white;
                    text-decoration: none;
                }
                button:hover {
                    background-color: #218838; /* Ciemniejszy zielony na hover */
                }
            </style>
        </head>
        <body>
        <div class=""container"">
                <h3>Kliknij przycisk poniżej aby aktywować konto</h3>
                <button><a href=""{0}"" target=""_blank"">Aktywuj</a></button>
                </div>
        </body>
        </html>";

        var message = String.Format(htmlTemplate, link);
        await _emailSenderService.SendEmailAsync(context.Message.Email, "Activate your account", message);
    }
    
    private string SetUrl(SendConfirmAccountEmailEvent notification)
    {
        return $"{Globals.ApplicationUrl}/confirm-account?token={notification.Token}&userId={notification.UserId}";
    }
}