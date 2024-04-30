namespace API.Features.Identity.Events.SendResetPasswordEmail;

public sealed class SendResetPasswordEmailEvent
{
    public string Email { get; set; }
    public string Token { get; set; }
    public Guid UserId { get; set; }
}