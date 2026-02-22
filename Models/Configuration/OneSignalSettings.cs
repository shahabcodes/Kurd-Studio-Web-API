namespace KurdStudio.Api.Models.Configuration;

public class OneSignalSettings
{
    public const string SectionName = "OneSignal";
    public string AppId { get; set; } = string.Empty;
    public string RestApiKey { get; set; } = string.Empty;
}
