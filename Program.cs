using DocumentSummarizer.Services;
using Microsoft.Extensions.AI;
using OpenAI;
using System.ClientModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Register our custom services
builder.Services.AddScoped<DocumentService>();
builder.Services.AddScoped<AiService>();

// Register Groq (free) using OpenAI-compatible client
var apiKey = builder.Configuration["AI:Groq:ApiKey"];
var model = "openai/gpt-oss-20b";

var openAIClient = new OpenAIClient(
    new ApiKeyCredential(apiKey),
    new OpenAIClientOptions
    {
        Endpoint = new Uri("https://api.groq.com/openai/v1")
    }
);

builder.Services.AddChatClient(
    openAIClient.GetChatClient(model).AsIChatClient()
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<DocumentSummarizer.Components.App>()
    .AddInteractiveServerRenderMode();

app.Run();