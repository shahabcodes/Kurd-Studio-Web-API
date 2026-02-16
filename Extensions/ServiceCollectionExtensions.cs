using FluentValidation;
using KurdStudio.Api.Data;
using KurdStudio.Api.Repositories.Implementations;
using KurdStudio.Api.Repositories.Interfaces;

namespace KurdStudio.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Data
        services.AddSingleton<IDbConnectionFactory, SqlConnectionFactory>();

        // Repositories
        services.AddScoped<IImageRepository, ImageRepository>();
        services.AddScoped<ISiteRepository, SiteRepository>();
        services.AddScoped<IArtworkRepository, ArtworkRepository>();
        services.AddScoped<IWritingRepository, WritingRepository>();
        services.AddScoped<IContactRepository, ContactRepository>();

        // Validators
        services.AddValidatorsFromAssemblyContaining<Program>();

        return services;
    }

    public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins("http://localhost:4200", "https://localhost:4200")
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials();
            });
        });

        return services;
    }
}
