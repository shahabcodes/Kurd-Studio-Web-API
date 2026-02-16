using KurdStudio.Api.Models.Entities;

namespace KurdStudio.Api.Repositories.Interfaces;

public interface IWritingRepository
{
    Task<IEnumerable<Writing>> GetAllAsync(string? typeName = null);
    Task<Writing?> GetBySlugAsync(string slug);
    Task<IEnumerable<WritingType>> GetTypesAsync();
}
