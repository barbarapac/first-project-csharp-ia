using first_project_csharp_ia._Shared;
using first_project_csharp_ia.Endpoints;
using first_project_csharp_ia.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.ConfigureAiServices(builder.Configuration, builder.Environment);
builder.Services.AddScoped<IAiService, AiService>();

var app = builder.Build();

// Configure endpoints
app.MapAiEndpoints();

app.Run();