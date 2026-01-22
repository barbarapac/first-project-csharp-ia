using Microsoft.Extensions.AI;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using OllamaSharp;

namespace first_project_csharp_ia._Shared;

public static class AiConfiguration
{
    public static void ConfigureAiServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        var openAiKey = configuration["OpenAI:ApiKey"] ?? "CHAVE_DO_OPENAI";
        var ollamaUrl = new Uri(configuration["Ollama:Url"] ?? "http://localhost:11434");
        var ollamaModel = configuration["Ollama:Model"] ?? "phi3:latest";

        services.AddSingleton<IDistributedCache>(provider =>
            new MemoryDistributedCache(Options.Create(new MemoryDistributedCacheOptions())));

        services.AddSingleton<IChatClient>(provider =>
        {
            return environment.IsDevelopment()
                ? new OllamaApiClient(ollamaUrl, ollamaModel)
                : new OpenAI.Chat.ChatClient("gpt-4o-mini", openAiKey).AsIChatClient();
        });
    }
}