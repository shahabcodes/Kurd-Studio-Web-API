using KurdStudio.Api.Models.DTOs;
using KurdStudio.Api.Repositories.Interfaces;

namespace KurdStudio.Api.Endpoints;

public static class WritingEndpoints
{
    public static RouteGroupBuilder MapWritingEndpoints(this RouteGroupBuilder group)
    {
        var writings = group.MapGroup("/writings");

        writings.MapGet("/", GetWritings)
            .WithName("GetWritings")
            .WithTags("Writings");

        writings.MapGet("/types", GetTypes)
            .WithName("GetWritingTypes")
            .WithTags("Writings");

        writings.MapGet("/{slug}", GetBySlug)
            .WithName("GetWritingBySlug")
            .WithTags("Writings");

        return group;
    }

    private static async Task<IResult> GetWritings(IWritingRepository repository, string? type = null)
    {
        var writings = await repository.GetAllAsync(type);

        var dtos = writings.Select(w => new WritingDto(
            w.Id,
            w.Title,
            w.Slug,
            w.TypeName,
            w.TypeDisplayName,
            w.Subtitle,
            w.Excerpt,
            null, // Don't include full content in list
            w.DatePublished,
            w.NovelName,
            w.ChapterNumber,
            w.CreatedAt
        ));

        return Results.Ok(dtos);
    }

    private static async Task<IResult> GetTypes(IWritingRepository repository)
    {
        var types = await repository.GetTypesAsync();
        return Results.Ok(types);
    }

    private static async Task<IResult> GetBySlug(string slug, IWritingRepository repository)
    {
        var writing = await repository.GetBySlugAsync(slug);

        if (writing == null)
        {
            return Results.NotFound();
        }

        var dto = new WritingDto(
            writing.Id,
            writing.Title,
            writing.Slug,
            writing.TypeName,
            writing.TypeDisplayName,
            writing.Subtitle,
            writing.Excerpt,
            writing.FullContent, // Include full content for detail view
            writing.DatePublished,
            writing.NovelName,
            writing.ChapterNumber,
            writing.CreatedAt
        );

        return Results.Ok(dto);
    }
}
