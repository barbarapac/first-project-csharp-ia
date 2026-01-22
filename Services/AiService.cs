using Microsoft.Extensions.AI;
using Microsoft.Extensions.Caching.Distributed;

namespace first_project_csharp_ia.Services;

public class AiService : IAiService
{
    private readonly IChatClient _client;
    private readonly IDistributedCache _cache;
    private const string WeatherExpertPrompt = "You're weather expert, answer me in just one sentence within 50 words.";

    public AiService(IChatClient client, IDistributedCache cache)
    {
        _client = client;
        _cache = cache;
    }

    public async Task<string> GetSimpleResponseAsync(string prompt)
    {
        var result = await _client.GetResponseAsync(prompt);
        return result.Text;
    }

    public async Task<string> GetWeatherExpertResponseAsync(string prompt)
    {
        var messages = new[]
        {
            new ChatMessage(ChatRole.System, WeatherExpertPrompt),
            new ChatMessage(ChatRole.User, prompt)
        };

        var result = await _client.GetResponseAsync(messages);
        return result.Text;
    }

    public async Task<string> GetCachedWeatherExpertResponseAsync(string prompt)
    {
        var cachedClient = new ChatClientBuilder(_client)
            .UseDistributedCache(_cache)
            .Build();

        var messages = new[]
        {
            new ChatMessage(ChatRole.System, WeatherExpertPrompt),
            new ChatMessage(ChatRole.User, prompt)
        };

        var result = await cachedClient.GetResponseAsync(messages);
        return result.Text;
    }
}