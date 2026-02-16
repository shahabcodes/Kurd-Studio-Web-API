-- ============================================================
-- Kurd Studio Stored Procedures
-- ============================================================

USE KurdStudioDb;
GO

-- ============================================================
-- IMAGE PROCEDURES
-- ============================================================

-- Get image by ID
CREATE OR ALTER PROCEDURE usp_GetImageById
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        FileName,
        ContentType,
        ImageData,
        AltText,
        FileSize,
        Width,
        Height,
        CreatedAt
    FROM Images
    WHERE Id = @Id;
END
GO

-- Get image thumbnail by ID
CREATE OR ALTER PROCEDURE usp_GetImageThumbnailById
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        FileName,
        ContentType,
        COALESCE(ThumbnailData, ImageData) AS ImageData,
        AltText,
        FileSize,
        Width,
        Height,
        CreatedAt
    FROM Images
    WHERE Id = @Id;
END
GO

-- ============================================================
-- SITE PROCEDURES
-- ============================================================

-- Get all site settings
CREATE OR ALTER PROCEDURE usp_GetSiteSettings
    @Key NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        SettingKey,
        SettingValue,
        SettingType,
        UpdatedAt
    FROM SiteSettings
    WHERE @Key IS NULL OR SettingKey = @Key
    ORDER BY SettingKey;
END
GO

-- Get artist profile
CREATE OR ALTER PROCEDURE usp_GetProfile
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 1
        p.Id,
        p.Name,
        p.Tagline,
        p.Bio,
        p.AvatarImageId,
        p.Email,
        p.InstagramUrl,
        p.TwitterUrl,
        p.ArtworksCount,
        p.PoemsCount,
        p.YearsExperience,
        p.UpdatedAt
    FROM Profile p;
END
GO

-- Get all active sections
CREATE OR ALTER PROCEDURE usp_GetSections
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        SectionKey,
        Tag,
        Title,
        Subtitle,
        DisplayOrder,
        IsActive,
        UpdatedAt
    FROM Sections
    WHERE IsActive = 1
    ORDER BY DisplayOrder;
END
GO

-- Get active hero content
CREATE OR ALTER PROCEDURE usp_GetHeroContent
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 1
        Id,
        Quote,
        QuoteAttribution,
        Headline,
        Subheading,
        FeaturedImageId,
        BadgeText,
        PrimaryCtaText,
        PrimaryCtaLink,
        SecondaryCtaText,
        SecondaryCtaLink,
        IsActive,
        UpdatedAt
    FROM HeroContent
    WHERE IsActive = 1;
END
GO

-- Get navigation items
CREATE OR ALTER PROCEDURE usp_GetNavigationItems
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        Label,
        Link,
        IconSvg,
        DisplayOrder,
        IsActive
    FROM NavigationItems
    WHERE IsActive = 1
    ORDER BY DisplayOrder;
END
GO

-- Get social links
CREATE OR ALTER PROCEDURE usp_GetSocialLinks
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        Platform,
        Url,
        IconSvg,
        DisplayOrder,
        IsActive
    FROM SocialLinks
    WHERE IsActive = 1
    ORDER BY DisplayOrder;
END
GO

-- ============================================================
-- ARTWORK PROCEDURES
-- ============================================================

-- Get artworks with optional type filter
CREATE OR ALTER PROCEDURE usp_GetArtworks
    @TypeName NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        a.Id,
        a.Title,
        a.Slug,
        a.ArtworkTypeId,
        at.TypeName,
        at.DisplayName AS TypeDisplayName,
        a.ImageId,
        a.Description,
        a.IsFeatured,
        a.DisplayOrder,
        a.CreatedAt,
        a.UpdatedAt
    FROM Artworks a
    INNER JOIN ArtworkTypes at ON a.ArtworkTypeId = at.Id
    WHERE @TypeName IS NULL OR at.TypeName = @TypeName
    ORDER BY a.DisplayOrder, a.CreatedAt DESC;
END
GO

-- Get artwork by slug
CREATE OR ALTER PROCEDURE usp_GetArtworkBySlug
    @Slug NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        a.Id,
        a.Title,
        a.Slug,
        a.ArtworkTypeId,
        at.TypeName,
        at.DisplayName AS TypeDisplayName,
        a.ImageId,
        a.Description,
        a.IsFeatured,
        a.DisplayOrder,
        a.CreatedAt,
        a.UpdatedAt
    FROM Artworks a
    INNER JOIN ArtworkTypes at ON a.ArtworkTypeId = at.Id
    WHERE a.Slug = @Slug;
END
GO

-- Get artwork types
CREATE OR ALTER PROCEDURE usp_GetArtworkTypes
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        TypeName,
        DisplayName,
        DisplayOrder
    FROM ArtworkTypes
    ORDER BY DisplayOrder;
END
GO

-- Get featured artworks
CREATE OR ALTER PROCEDURE usp_GetFeaturedArtworks
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        a.Id,
        a.Title,
        a.Slug,
        a.ArtworkTypeId,
        at.TypeName,
        at.DisplayName AS TypeDisplayName,
        a.ImageId,
        a.Description,
        a.IsFeatured,
        a.DisplayOrder,
        a.CreatedAt,
        a.UpdatedAt
    FROM Artworks a
    INNER JOIN ArtworkTypes at ON a.ArtworkTypeId = at.Id
    WHERE a.IsFeatured = 1
    ORDER BY a.DisplayOrder;
END
GO

-- ============================================================
-- WRITING PROCEDURES
-- ============================================================

-- Get writings with optional type filter
CREATE OR ALTER PROCEDURE usp_GetWritings
    @TypeName NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        w.Id,
        w.Title,
        w.Slug,
        w.WritingTypeId,
        wt.TypeName,
        wt.DisplayName AS TypeDisplayName,
        w.Subtitle,
        w.Excerpt,
        w.DatePublished,
        w.NovelName,
        w.ChapterNumber,
        w.DisplayOrder,
        w.CreatedAt,
        w.UpdatedAt
    FROM Writings w
    INNER JOIN WritingTypes wt ON w.WritingTypeId = wt.Id
    WHERE @TypeName IS NULL OR wt.TypeName = @TypeName
    ORDER BY w.DisplayOrder, w.CreatedAt DESC;
END
GO

-- Get writing by slug with full content
CREATE OR ALTER PROCEDURE usp_GetWritingBySlug
    @Slug NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        w.Id,
        w.Title,
        w.Slug,
        w.WritingTypeId,
        wt.TypeName,
        wt.DisplayName AS TypeDisplayName,
        w.Subtitle,
        w.Excerpt,
        w.FullContent,
        w.DatePublished,
        w.NovelName,
        w.ChapterNumber,
        w.DisplayOrder,
        w.CreatedAt,
        w.UpdatedAt
    FROM Writings w
    INNER JOIN WritingTypes wt ON w.WritingTypeId = wt.Id
    WHERE w.Slug = @Slug;
END
GO

-- Get writing types
CREATE OR ALTER PROCEDURE usp_GetWritingTypes
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        TypeName,
        DisplayName,
        DisplayOrder
    FROM WritingTypes
    ORDER BY DisplayOrder;
END
GO

-- ============================================================
-- CONTACT PROCEDURES
-- ============================================================

-- Create contact submission
CREATE OR ALTER PROCEDURE usp_CreateContactSubmission
    @Name NVARCHAR(255),
    @Email NVARCHAR(255),
    @Subject NVARCHAR(255),
    @Budget NVARCHAR(100) = NULL,
    @Message NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO ContactSubmissions (Name, Email, Subject, Budget, Message)
    VALUES (@Name, @Email, @Subject, @Budget, @Message);

    SELECT SCOPE_IDENTITY() AS Id;
END
GO

-- Get contact submissions
CREATE OR ALTER PROCEDURE usp_GetContactSubmissions
    @OnlyUnread BIT = 0
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        Name,
        Email,
        Subject,
        Budget,
        Message,
        SubmittedAt,
        IsRead,
        IsResponded
    FROM ContactSubmissions
    WHERE @OnlyUnread = 0 OR IsRead = 0
    ORDER BY SubmittedAt DESC;
END
GO

PRINT 'All stored procedures created successfully.';
GO
