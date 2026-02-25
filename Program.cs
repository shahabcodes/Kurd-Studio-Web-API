using KurdStudio.Api.Extensions;
using KurdStudio.Api.Models.Configuration;
using KurdStudio.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.Configure<OneSignalSettings>(
    builder.Configuration.GetSection(OneSignalSettings.SectionName));
builder.Services.AddHttpClient<INotificationService, OneSignalNotificationService>(client =>
{
    var oneSignalConfig = builder.Configuration.GetSection(OneSignalSettings.SectionName).Get<OneSignalSettings>();
    if (oneSignalConfig is not null && !string.IsNullOrEmpty(oneSignalConfig.RestApiKey))
    {
        client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", oneSignalConfig.RestApiKey);
    }
});
builder.Services.AddApplicationServices();
builder.Services.AddCorsPolicy(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Kurd Studio API", Version = "v1" });
});

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

// Map endpoints
var apiPrefix = builder.Configuration.GetValue<string>("ApiPrefix") ?? "/api";
app.MapApiEndpoints(apiPrefix);

// Health check
app.MapGet("/", () => Results.Ok(new { Status = "Healthy", Service = "Kurd Studio API" }));


app.Run();
