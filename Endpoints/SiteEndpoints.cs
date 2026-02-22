using KurdStudio.Api.Models.DTOs;
using KurdStudio.Api.Repositories.Interfaces;

namespace KurdStudio.Api.Endpoints;

public static class SiteEndpoints
{
    public static RouteGroupBuilder MapSiteEndpoints(this RouteGroupBuilder group)
    {
        var site = group.MapGroup("/site");

        site.MapGet("/settings", GetSettings)
            .WithName("GetSettings")
            .WithTags("Site");

        site.MapGet("/profile", GetProfile)
            .WithName("GetProfile")
            .WithTags("Site");

        site.MapGet("/sections", GetSections)
            .WithName("GetSections")
            .WithTags("Site");

        site.MapGet("/hero", GetHero)
            .WithName("GetHero")
            .WithTags("Site");

        site.MapGet("/navigation", GetNavigation)
            .WithName("GetNavigation")
            .WithTags("Site");

        site.MapGet("/social", GetSocialLinks)
            .WithName("GetSocialLinks")
            .WithTags("Site");

        site.MapGet("/all", GetAllSiteData)
            .WithName("GetAllSiteData")
            .WithTags("Site");

        return group;
    }

    private static async Task<IResult> GetSettings(ISiteRepository repository, string? key = null)
    {
        var settings = await repository.GetSettingsAsync(key);
        return Results.Ok(settings);
    }

    private static async Task<IResult> GetProfile(ISiteRepository repository, HttpRequest request, IConfiguration config)
    {
        var profile = await repository.GetProfileAsync();

        if (profile == null)
        {
            return Results.NotFound();
        }

        var configuredBaseUrl = config["BaseUrl"];
        var baseUrl = !string.IsNullOrEmpty(configuredBaseUrl) ? configuredBaseUrl : $"{request.Scheme}://{request.Host}";
        var avatarUrl = profile.AvatarImageId.HasValue
            ? $"{baseUrl}/api/images/{profile.AvatarImageId}"
            : null;

        var dto = new ProfileDto(
            profile.Id,
            profile.Name,
            profile.Tagline,
            profile.Bio,
            profile.AvatarImageId,
            avatarUrl,
            profile.Email,
            profile.InstagramUrl,
            profile.TwitterUrl,
            profile.ArtworksCount,
            profile.PoemsCount,
            profile.YearsExperience
        );

        return Results.Ok(dto);
    }

    private static async Task<IResult> GetSections(ISiteRepository repository)
    {
        var sections = await repository.GetSectionsAsync();
        return Results.Ok(sections);
    }

    private static async Task<IResult> GetHero(ISiteRepository repository, HttpRequest request, IConfiguration config)
    {
        var hero = await repository.GetHeroContentAsync();

        if (hero == null)
        {
            return Results.NotFound();
        }

        var configuredBaseUrl = config["BaseUrl"];
        var baseUrl = !string.IsNullOrEmpty(configuredBaseUrl) ? configuredBaseUrl : $"{request.Scheme}://{request.Host}";
        var featuredImageUrl = hero.FeaturedImageId.HasValue
            ? $"{baseUrl}/api/images/{hero.FeaturedImageId}"
            : null;

        var dto = new HeroDto(
            hero.Id,
            hero.Quote,
            hero.QuoteAttribution,
            hero.Headline,
            hero.Subheading,
            hero.FeaturedImageId,
            featuredImageUrl,
            hero.BadgeText,
            hero.PrimaryCtaText,
            hero.PrimaryCtaLink,
            hero.SecondaryCtaText,
            hero.SecondaryCtaLink
        );

        return Results.Ok(dto);
    }

    private static async Task<IResult> GetNavigation(ISiteRepository repository)
    {
        var navItems = await repository.GetNavigationItemsAsync();
        return Results.Ok(navItems);
    }

    private static async Task<IResult> GetSocialLinks(ISiteRepository repository)
    {
        var socialLinks = await repository.GetSocialLinksAsync();
        return Results.Ok(socialLinks);
    }

    private static async Task<IResult> GetAllSiteData(ISiteRepository repository, HttpRequest request, IConfiguration config)
    {
        var settingsTask = repository.GetSettingsAsync();
        var profileTask = repository.GetProfileAsync();
        var heroTask = repository.GetHeroContentAsync();
        var sectionsTask = repository.GetSectionsAsync();
        var navTask = repository.GetNavigationItemsAsync();
        var socialTask = repository.GetSocialLinksAsync();

        await Task.WhenAll(settingsTask, profileTask, heroTask, sectionsTask, navTask, socialTask);

        var settings = await settingsTask;
        var profile = await profileTask;
        var hero = await heroTask;
        var sections = await sectionsTask;
        var nav = await navTask;
        var social = await socialTask;

        var configuredBaseUrl = config["BaseUrl"];
        var baseUrl = !string.IsNullOrEmpty(configuredBaseUrl) ? configuredBaseUrl : $"{request.Scheme}://{request.Host}";

        var settingsDict = settings.ToDictionary(s => s.SettingKey, s => s.SettingValue);

        ProfileDto? profileDto = null;
        if (profile != null)
        {
            var avatarUrl = profile.AvatarImageId.HasValue
                ? $"{baseUrl}/api/images/{profile.AvatarImageId}"
                : null;

            profileDto = new ProfileDto(
                profile.Id,
                profile.Name,
                profile.Tagline,
                profile.Bio,
                profile.AvatarImageId,
                avatarUrl,
                profile.Email,
                profile.InstagramUrl,
                profile.TwitterUrl,
                profile.ArtworksCount,
                profile.PoemsCount,
                profile.YearsExperience
            );
        }

        HeroDto? heroDto = null;
        if (hero != null)
        {
            var featuredImageUrl = hero.FeaturedImageId.HasValue
                ? $"{baseUrl}/api/images/{hero.FeaturedImageId}"
                : null;

            heroDto = new HeroDto(
                hero.Id,
                hero.Quote,
                hero.QuoteAttribution,
                hero.Headline,
                hero.Subheading,
                hero.FeaturedImageId,
                featuredImageUrl,
                hero.BadgeText,
                hero.PrimaryCtaText,
                hero.PrimaryCtaLink,
                hero.SecondaryCtaText,
                hero.SecondaryCtaLink
            );
        }

        var siteData = new SiteDataDto(
            settingsDict,
            profileDto,
            heroDto,
            sections,
            nav,
            social
        );

        return Results.Ok(siteData);
    }
}
