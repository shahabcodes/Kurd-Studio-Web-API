using KurdStudio.Api.Models.DTOs;

namespace KurdStudio.Api.Services;

public interface INotificationService
{
    Task SendContactNotificationAsync(ContactRequest request, int submissionId);
}
