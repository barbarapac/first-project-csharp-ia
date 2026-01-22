using first_project_csharp_ia.Models;
using first_project_csharp_ia.Services;

namespace first_project_csharp_ia.Endpoints;

public static class AiEndpoints
{
    public static void MapAiEndpoints(this WebApplication app)
    {
        app.MapPost("/", GetSimpleResponse)
            .WithName("GetSimpleResponse")
            .WithSummary("Get simple AI response");

        app.MapPost("/v2", GetWeatherExpertResponse)
            .WithName("GetWeatherExpertResponse")
            .WithSummary("Get weather expert response");

        app.MapPost("/v3", GetCachedWeatherExpertResponse)
            .WithName("GetCachedWeatherExpertResponse")
            .WithSummary("Get cached weather expert response");
    }

    private static async Task<IResult> GetSimpleResponse(Question question, IAiService aiService)
    {
        var response = await aiService.GetSimpleResponseAsync(question.Prompt);
        return Results.Ok(response);
    }

    private static async Task<IResult> GetWeatherExpertResponse(Question question, IAiService aiService)
    {
        var response = await aiService.GetWeatherExpertResponseAsync(question.Prompt);
        return Results.Ok(response);
    }

    private static async Task<IResult> GetCachedWeatherExpertResponse(Question question, IAiService aiService)
    {
        var response = await aiService.GetCachedWeatherExpertResponseAsync(question.Prompt);
        return Results.Ok(response);
    }
}