using KurdStudio.Api.Models.Entities;

namespace KurdStudio.Api.Models.DTOs;

public record SiteDataDto(
    Dictionary<string, string?> Settings,
    ProfileDto? Profile,
    HeroDto? Hero,
    IEnumerable<Section> Sections,
    IEnumerable<NavigationItem> Navigation,
    IEnumerable<SocialLink> SocialLinks
);
