namespace KurdStudio.Api.Models.DTOs;

public record WritingDto(
    int Id,
    string Title,
    string Slug,
    string TypeName,
    string TypeDisplayName,
    string? Subtitle,
    string? Excerpt,
    string? FullContent,
    DateTime? DatePublished,
    string? NovelName,
    int? ChapterNumber,
    DateTime CreatedAt
);
