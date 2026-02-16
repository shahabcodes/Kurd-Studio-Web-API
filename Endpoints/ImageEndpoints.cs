using KurdStudio.Api.Repositories.Interfaces;

namespace KurdStudio.Api.Endpoints;

public static class ImageEndpoints
{
    public static RouteGroupBuilder MapImageEndpoints(this RouteGroupBuilder group)
    {
        var images = group.MapGroup("/images");

        images.MapGet("/{id:int}", GetImage)
            .WithName("GetImage")
            .WithTags("Images");

        images.MapGet("/{id:int}/thumbnail", GetThumbnail)
            .WithName("GetThumbnail")
            .WithTags("Images");

        return group;
    }

    private static async Task<IResult> GetImage(int id, IImageRepository repository, HttpContext context)
    {
        var image = await repository.GetByIdAsync(id);

        if (image == null)
        {
            return Results.NotFound();
        }

        // Set cache headers
        context.Response.Headers.CacheControl = "public, max-age=31536000";

        return Results.Bytes(image.ImageData, image.ContentType);
    }

    private static async Task<IResult> GetThumbnail(int id, IImageRepository repository, HttpContext context)
    {
        var image = await repository.GetThumbnailByIdAsync(id);

        if (image == null)
        {
            return Results.NotFound();
        }

        // Set cache headers
        context.Response.Headers.CacheControl = "public, max-age=31536000";

        return Results.Bytes(image.ImageData, image.ContentType);
    }
}
