using Dapper;
using KurdStudio.Api.Data;
using KurdStudio.Api.Models.Entities;
using KurdStudio.Api.Repositories.Interfaces;
using System.Data;

namespace KurdStudio.Api.Repositories.Implementations;

public class ArtworkRepository : IArtworkRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public ArtworkRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<Artwork>> GetAllAsync(string? typeName = null)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<Artwork>(
            "usp_GetArtworks",
            new { TypeName = typeName },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<Artwork?> GetBySlugAsync(string slug)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Artwork>(
            "usp_GetArtworkBySlug",
            new { Slug = slug },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<IEnumerable<ArtworkType>> GetTypesAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<ArtworkType>(
            "usp_GetArtworkTypes",
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<IEnumerable<Artwork>> GetFeaturedAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<Artwork>(
            "usp_GetFeaturedArtworks",
            commandType: CommandType.StoredProcedure
        );
    }
}
