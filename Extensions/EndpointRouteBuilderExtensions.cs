using KurdStudio.Api.Endpoints;

namespace KurdStudio.Api.Extensions;

public static class EndpointRouteBuilderExtensions
{
    public static IEndpointRouteBuilder MapApiEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var api = endpoints.MapGroup("/api");

        api.MapImageEndpoints();
        api.MapSiteEndpoints();
        api.MapArtworkEndpoints();
        api.MapWritingEndpoints();
        api.MapContactEndpoints();

        return endpoints;
    }
}
