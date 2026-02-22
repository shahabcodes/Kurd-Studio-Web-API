using KurdStudio.Api.Models.DTOs;

namespace KurdStudio.Api.Services;

public interface INotificationService
{
    Task<NotificationResult> SendContactNotificationAsync(ContactRequest request, int submissionId);
}

public record NotificationResult(bool Success, string? Response);
