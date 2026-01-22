namespace first_project_csharp_ia.Services;

public interface IAiService
{
    Task<string> GetSimpleResponseAsync(string prompt);
    Task<string> GetWeatherExpertResponseAsync(string prompt);
    Task<string> GetCachedWeatherExpertResponseAsync(string prompt);
}