using Microsoft.Extensions.AI;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using OllamaSharp;

const string openAiKey = "CHAVE_DO_OPENAI";
var ollamaUrl = new Uri("http://localhost:11434");

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
var cache = new MemoryDistributedCache(Options.Create(new MemoryDistributedCacheOptions()));

var client = app.Environment.IsDevelopment()
    ? new OllamaApiClient(ollamaUrl, "phi3:latest")
    : new OpenAI.Chat.ChatClient("gpt-4o-mini", openAiKey)
        .AsIChatClient();

var cachedClient = new ChatClientBuilder(client)
    .UseDistributedCache(cache)
    .Build();

// Simples
app.MapPost("/", async (Question question) =>
{
    var result = await client.GetResponseAsync(question.Prompt);

    return Results.Ok(result.Text);
});

    
// Com contexto
app.MapPost("/v2", async (Question question) =>
{
    var result = await client.GetResponseAsync(
        [
            new ChatMessage(ChatRole.System, "You're weather expert, answer me in just one sentence within 50 words."),
            new ChatMessage(ChatRole.User, question.Prompt)
        ]);

    return Results.Ok(result.Text);
});

// Com cache, otimizando recurso
app.MapPost("/v3", async (Question question) =>
{
    var result = await cachedClient.GetResponseAsync(
    [
        new ChatMessage(ChatRole.System, "You're weather expert, answer me in just one sentence within 50 words."),
        new ChatMessage(ChatRole.User, question.Prompt)
    ]);

    return Results.Ok(result.Text);
});

app.Run();

public record Question(string Prompt);