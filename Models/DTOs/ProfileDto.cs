namespace KurdStudio.Api.Models.DTOs;

public record ProfileDto(
    int Id,
    string Name,
    string? Tagline,
    string? Bio,
    int? AvatarImageId,
    string? AvatarImageUrl,
    string? Email,
    string? InstagramUrl,
    string? TwitterUrl,
    string? ArtworksCount,
    string? PoemsCount,
    string? YearsExperience
);
