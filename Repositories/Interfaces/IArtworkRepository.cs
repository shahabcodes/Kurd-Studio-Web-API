using KurdStudio.Api.Models.Entities;

namespace KurdStudio.Api.Repositories.Interfaces;

public interface IArtworkRepository
{
    Task<IEnumerable<Artwork>> GetAllAsync(string? typeName = null);
    Task<Artwork?> GetBySlugAsync(string slug);
    Task<IEnumerable<ArtworkType>> GetTypesAsync();
    Task<IEnumerable<Artwork>> GetFeaturedAsync();
}
