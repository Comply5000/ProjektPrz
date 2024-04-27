namespace API.Features.Identity.Events.SendConfirmAccountEmail;

public record SendConfirmAccountEmailEvent
{
    public string Email { get; set; }
    public string Token { get; set; }
    public Guid UserId { get; set; }
}