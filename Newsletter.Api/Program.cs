using Newsletter.Ai;
using Newsletter.Api.Workers;
using Newsletter.Core;
using Newsletter.Infra;

var builder = WebApplication.CreateBuilder(args);

Configuration.OpenAi.ApiKey = builder.Configuration.GetValue<string>("OpenAi:ApiKey") ?? 
                              throw new InvalidOperationException("OpenAI API key is not configured.");

builder.Services.AddServices();
builder.Services.AddRepositories();
builder.Services.AddAgents();

builder.Services.AddHostedService<NewsletterWorker>();

var app = builder.Build();

Configuration.Rootpath = app.Environment.ContentRootPath;

app.MapGet("/", () => "Hello World!");

app.Run();
