using KurdStudio.Api.Models.Entities;

namespace KurdStudio.Api.Repositories.Interfaces;

public interface ISiteRepository
{
    Task<IEnumerable<SiteSetting>> GetSettingsAsync(string? key = null);
    Task<Profile?> GetProfileAsync();
    Task<IEnumerable<Section>> GetSectionsAsync();
    Task<HeroContent?> GetHeroContentAsync();
    Task<IEnumerable<NavigationItem>> GetNavigationItemsAsync();
    Task<IEnumerable<SocialLink>> GetSocialLinksAsync();
}
