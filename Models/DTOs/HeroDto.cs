namespace KurdStudio.Api.Models.DTOs;

public record HeroDto(
    int Id,
    string? Quote,
    string? QuoteAttribution,
    string? Headline,
    string? Subheading,
    int? FeaturedImageId,
    string? FeaturedImageUrl,
    string? BadgeText,
    string? PrimaryCtaText,
    string? PrimaryCtaLink,
    string? SecondaryCtaText,
    string? SecondaryCtaLink
);
