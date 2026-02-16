using KurdStudio.Api.Models.DTOs;
using KurdStudio.Api.Repositories.Interfaces;

namespace KurdStudio.Api.Endpoints;

public static class ArtworkEndpoints
{
    public static RouteGroupBuilder MapArtworkEndpoints(this RouteGroupBuilder group)
    {
        var artworks = group.MapGroup("/artworks");

        artworks.MapGet("/", GetArtworks)
            .WithName("GetArtworks")
            .WithTags("Artworks");

        artworks.MapGet("/types", GetTypes)
            .WithName("GetArtworkTypes")
            .WithTags("Artworks");

        artworks.MapGet("/featured", GetFeatured)
            .WithName("GetFeaturedArtworks")
            .WithTags("Artworks");

        artworks.MapGet("/{slug}", GetBySlug)
            .WithName("GetArtworkBySlug")
            .WithTags("Artworks");

        return group;
    }

    private static async Task<IResult> GetArtworks(
        IArtworkRepository repository,
        HttpRequest request,
        string? type = null)
    {
        var artworks = await repository.GetAllAsync(type);

        var baseUrl = $"{request.Scheme}://{request.Host}";

        var dtos = artworks.Select(a => new ArtworkDto(
            a.Id,
            a.Title,
            a.Slug,
            a.TypeName,
            a.TypeDisplayName,
            a.ImageId,
            $"{baseUrl}/api/images/{a.ImageId}",
            $"{baseUrl}/api/images/{a.ImageId}/thumbnail",
            a.Description,
            a.IsFeatured,
            a.CreatedAt
        ));

        return Results.Ok(dtos);
    }

    private static async Task<IResult> GetTypes(IArtworkRepository repository)
    {
        var types = await repository.GetTypesAsync();
        return Results.Ok(types);
    }

    private static async Task<IResult> GetFeatured(IArtworkRepository repository, HttpRequest request)
    {
        var artworks = await repository.GetFeaturedAsync();

        var baseUrl = $"{request.Scheme}://{request.Host}";

        var dtos = artworks.Select(a => new ArtworkDto(
            a.Id,
            a.Title,
            a.Slug,
            a.TypeName,
            a.TypeDisplayName,
            a.ImageId,
            $"{baseUrl}/api/images/{a.ImageId}",
            $"{baseUrl}/api/images/{a.ImageId}/thumbnail",
            a.Description,
            a.IsFeatured,
            a.CreatedAt
        ));

        return Results.Ok(dtos);
    }

    private static async Task<IResult> GetBySlug(string slug, IArtworkRepository repository, HttpRequest request)
    {
        var artwork = await repository.GetBySlugAsync(slug);

        if (artwork == null)
        {
            return Results.NotFound();
        }

        var baseUrl = $"{request.Scheme}://{request.Host}";

        var dto = new ArtworkDto(
            artwork.Id,
            artwork.Title,
            artwork.Slug,
            artwork.TypeName,
            artwork.TypeDisplayName,
            artwork.ImageId,
            $"{baseUrl}/api/images/{artwork.ImageId}",
            $"{baseUrl}/api/images/{artwork.ImageId}/thumbnail",
            artwork.Description,
            artwork.IsFeatured,
            artwork.CreatedAt
        );

        return Results.Ok(dto);
    }
}
