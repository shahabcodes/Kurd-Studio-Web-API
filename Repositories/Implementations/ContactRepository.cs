using Dapper;
using KurdStudio.Api.Data;
using KurdStudio.Api.Models.DTOs;
using KurdStudio.Api.Repositories.Interfaces;
using System.Data;

namespace KurdStudio.Api.Repositories.Implementations;

public class ContactRepository : IContactRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public ContactRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<int> CreateSubmissionAsync(ContactRequest request)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.ExecuteScalarAsync<int>(
            "usp_CreateContactSubmission",
            new
            {
                request.Name,
                request.Email,
                request.Subject,
                request.Budget,
                request.Message
            },
            commandType: CommandType.StoredProcedure
        );
    }
}
