using FluentValidation;
using KurdStudio.Api.Models.DTOs;
using KurdStudio.Api.Repositories.Interfaces;

namespace KurdStudio.Api.Endpoints;

public static class ContactEndpoints
{
    public static RouteGroupBuilder MapContactEndpoints(this RouteGroupBuilder group)
    {
        var contact = group.MapGroup("/contact");

        contact.MapPost("/", CreateSubmission)
            .WithName("CreateContactSubmission")
            .WithTags("Contact")
            .Produces<object>(201)
            .Produces<object>(400);

        return group;
    }

    private static async Task<IResult> CreateSubmission(
        ContactRequest request,
        IContactRepository repository,
        IValidator<ContactRequest> validator)
    {
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );

            return Results.BadRequest(new { Errors = errors });
        }

        var id = await repository.CreateSubmissionAsync(request);

        return Results.Created($"/api/contact/{id}", new { Id = id, Message = "Thank you! Your message has been sent." });
    }
}
