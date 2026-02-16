using Dapper;
using KurdStudio.Api.Data;
using KurdStudio.Api.Models.Entities;
using KurdStudio.Api.Repositories.Interfaces;
using System.Data;

namespace KurdStudio.Api.Repositories.Implementations;

public class ImageRepository : IImageRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public ImageRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Image?> GetByIdAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Image>(
            "usp_GetImageById",
            new { Id = id },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<Image?> GetThumbnailByIdAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Image>(
            "usp_GetImageThumbnailById",
            new { Id = id },
            commandType: CommandType.StoredProcedure
        );
    }
}
