using KurdStudio.Api.Models.Entities;

namespace KurdStudio.Api.Repositories.Interfaces;

public interface IImageRepository
{
    Task<Image?> GetByIdAsync(int id);
    Task<Image?> GetThumbnailByIdAsync(int id);
}
