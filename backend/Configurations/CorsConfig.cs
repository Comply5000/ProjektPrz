namespace API.Shared.Configurations.Cors;

public sealed class CorsConfig
{
    public bool AllowCredentials { get; set; }
    public IEnumerable<string> AllowedOrigins { get; set; }
    public IEnumerable<string> AllowedMethods { get; set; }// Lista metod HTTP, które są dozwolone w żądaniach CORS. Na przykład: GET, POST, PUT, DELETE, itp.
    public IEnumerable<string> AllowedHeaders { get; set; }
    public IEnumerable<string> ExposedHeaders { get; set; }// Lista nagłówków HTTP, które są dozwolone w odpowiedziach CORS
}