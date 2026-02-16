namespace KurdStudio.Api.Models.DTOs;

public record ContactRequest(
    string Name,
    string Email,
    string Subject,
    string? Budget,
    string Message
);
