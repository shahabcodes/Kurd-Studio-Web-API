using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using KurdStudio.Api.Models.Configuration;
using KurdStudio.Api.Models.DTOs;
using Microsoft.Extensions.Options;

namespace KurdStudio.Api.Services;

public class OneSignalNotificationService : INotificationService
{
    private readonly HttpClient _httpClient;
    private readonly OneSignalSettings _settings;
    private readonly ILogger<OneSignalNotificationService> _logger;

    public OneSignalNotificationService(
        HttpClient httpClient,
        IOptions<OneSignalSettings> settings,
        ILogger<OneSignalNotificationService> logger)
    {
        _httpClient = httpClient;
        _settings = settings.Value;
        _logger = logger;
    }

    public async Task SendContactNotificationAsync(ContactRequest request, int submissionId)
    {
        try
        {
            var messagePreview = request.Message.Length > 100
                ? request.Message[..100] + "..."
                : request.Message;

            var contentLines = new List<string> { request.Subject };
            if (!string.IsNullOrWhiteSpace(request.Budget))
                contentLines.Add($"\ud83d\udcb0 Budget: {request.Budget}");
            contentLines.Add(string.Empty);
            contentLines.Add(messagePreview);

            var payload = new
            {
                app_id = _settings.AppId,
                included_segments = new[] { "Total Subscriptions" },
                headings = new { en = $"\u2709\ufe0f {request.Name}" },
                contents = new { en = string.Join("\n", contentLines) },
                android_accent_color = "FFDB2777",
                small_icon = "ic_stat_notification",
                large_icon = "ic_launcher",
                android_group = "contact_submissions",
                android_group_message = new { en = "$[notif_count] new contact messages" },
                priority = 10,
                data = new
                {
                    type = "contact_submission",
                    submission_id = submissionId,
                    name = request.Name,
                    email = request.Email,
                    subject = request.Subject
                }
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", _settings.RestApiKey);

            var response = await _httpClient.PostAsync(
                "https://api.onesignal.com/notifications", content);

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation(
                    "Push notification sent for contact submission {SubmissionId}", submissionId);
            }
            else
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                _logger.LogWarning(
                    "OneSignal returned {StatusCode} for submission {SubmissionId}: {Response}",
                    response.StatusCode, submissionId, responseBody);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Failed to send push notification for contact submission {SubmissionId}", submissionId);
        }
    }
}
