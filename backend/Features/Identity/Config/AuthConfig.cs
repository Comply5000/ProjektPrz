namespace API.Features.Identity.Config;

//add comments
public class AuthConfig
{
    public string JwtKey { get; init; }
    public string JwtIssuer { get; init; }
    public TimeSpan Expires { get; init; }
}
