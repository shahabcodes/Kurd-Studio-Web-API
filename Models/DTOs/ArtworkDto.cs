namespace KurdStudio.Api.Models.DTOs;

public record ArtworkDto(
    int Id,
    string Title,
    string Slug,
    string TypeName,
    string TypeDisplayName,
    int ImageId,
    string ImageUrl,
    string ThumbnailUrl,
    string? Description,
    bool IsFeatured,
    DateTime CreatedAt
);
