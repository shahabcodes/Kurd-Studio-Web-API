using Dapper;
using KurdStudio.Api.Data;
using KurdStudio.Api.Models.Entities;
using KurdStudio.Api.Repositories.Interfaces;
using System.Data;

namespace KurdStudio.Api.Repositories.Implementations;

public class SiteRepository : ISiteRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public SiteRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<SiteSetting>> GetSettingsAsync(string? key = null)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<SiteSetting>(
            "usp_GetSiteSettings",
            new { Key = key },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<Profile?> GetProfileAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Profile>(
            "usp_GetProfile",
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<IEnumerable<Section>> GetSectionsAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<Section>(
            "usp_GetSections",
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<HeroContent?> GetHeroContentAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<HeroContent>(
            "usp_GetHeroContent",
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<IEnumerable<NavigationItem>> GetNavigationItemsAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<NavigationItem>(
            "usp_GetNavigationItems",
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<IEnumerable<SocialLink>> GetSocialLinksAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<SocialLink>(
            "usp_GetSocialLinks",
            commandType: CommandType.StoredProcedure
        );
    }
}
