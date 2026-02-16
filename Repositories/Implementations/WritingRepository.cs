using Dapper;
using KurdStudio.Api.Data;
using KurdStudio.Api.Models.Entities;
using KurdStudio.Api.Repositories.Interfaces;
using System.Data;

namespace KurdStudio.Api.Repositories.Implementations;

public class WritingRepository : IWritingRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public WritingRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<Writing>> GetAllAsync(string? typeName = null)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<Writing>(
            "usp_GetWritings",
            new { TypeName = typeName },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<Writing?> GetBySlugAsync(string slug)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Writing>(
            "usp_GetWritingBySlug",
            new { Slug = slug },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<IEnumerable<WritingType>> GetTypesAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<WritingType>(
            "usp_GetWritingTypes",
            commandType: CommandType.StoredProcedure
        );
    }
}
