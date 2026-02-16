-- ============================================================
-- Kurd Studio Tables Creation Script
-- ============================================================

USE KurdStudioDb;
GO

-- ============================================================
-- 1. Images (Central Image Storage)
-- ============================================================
CREATE TABLE Images (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FileName NVARCHAR(255) NOT NULL,
    ContentType NVARCHAR(100) NOT NULL,
    ImageData VARBINARY(MAX) NOT NULL,
    ThumbnailData VARBINARY(MAX),
    AltText NVARCHAR(255),
    FileSize INT,
    Width INT,
    Height INT,
    CreatedAt DATETIME2 DEFAULT GETUTCDATE()
);
GO

CREATE INDEX IX_Images_FileName ON Images(FileName);
GO

-- ============================================================
-- 2. ArtworkTypes (Lookup Table)
-- ============================================================
CREATE TABLE ArtworkTypes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    TypeName NVARCHAR(50) NOT NULL UNIQUE,
    DisplayName NVARCHAR(100) NOT NULL,
    DisplayOrder INT DEFAULT 0
);
GO

-- ============================================================
-- 3. WritingTypes (Lookup Table)
-- ============================================================
CREATE TABLE WritingTypes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    TypeName NVARCHAR(50) NOT NULL UNIQUE,
    DisplayName NVARCHAR(100) NOT NULL,
    DisplayOrder INT DEFAULT 0
);
GO

-- ============================================================
-- 4. SiteSettings
-- ============================================================
CREATE TABLE SiteSettings (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    SettingKey NVARCHAR(100) NOT NULL UNIQUE,
    SettingValue NVARCHAR(MAX),
    SettingType NVARCHAR(50) DEFAULT 'string',
    UpdatedAt DATETIME2 DEFAULT GETUTCDATE()
);
GO

CREATE INDEX IX_SiteSettings_Key ON SiteSettings(SettingKey);
GO

-- ============================================================
-- 5. Profile
-- ============================================================
CREATE TABLE Profile (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Tagline NVARCHAR(255),
    Bio NVARCHAR(MAX),
    AvatarImageId INT FOREIGN KEY REFERENCES Images(Id),
    Email NVARCHAR(255),
    InstagramUrl NVARCHAR(500),
    TwitterUrl NVARCHAR(500),
    ArtworksCount NVARCHAR(20),
    PoemsCount NVARCHAR(20),
    YearsExperience NVARCHAR(20),
    UpdatedAt DATETIME2 DEFAULT GETUTCDATE()
);
GO

-- ============================================================
-- 6. Sections
-- ============================================================
CREATE TABLE Sections (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    SectionKey NVARCHAR(50) NOT NULL UNIQUE,
    Tag NVARCHAR(100),
    Title NVARCHAR(255),
    Subtitle NVARCHAR(MAX),
    DisplayOrder INT DEFAULT 0,
    IsActive BIT DEFAULT 1,
    UpdatedAt DATETIME2 DEFAULT GETUTCDATE()
);
GO

CREATE INDEX IX_Sections_Key ON Sections(SectionKey);
GO

-- ============================================================
-- 7. Artworks
-- ============================================================
CREATE TABLE Artworks (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(255) NOT NULL,
    Slug NVARCHAR(255) NOT NULL UNIQUE,
    ArtworkTypeId INT NOT NULL FOREIGN KEY REFERENCES ArtworkTypes(Id),
    ImageId INT NOT NULL FOREIGN KEY REFERENCES Images(Id),
    Description NVARCHAR(MAX),
    IsFeatured BIT DEFAULT 0,
    DisplayOrder INT DEFAULT 0,
    CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 DEFAULT GETUTCDATE()
);
GO

CREATE INDEX IX_Artworks_Slug ON Artworks(Slug);
CREATE INDEX IX_Artworks_TypeId ON Artworks(ArtworkTypeId);
CREATE INDEX IX_Artworks_Featured ON Artworks(IsFeatured) WHERE IsFeatured = 1;
GO

-- ============================================================
-- 8. Writings
-- ============================================================
CREATE TABLE Writings (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(255) NOT NULL,
    Slug NVARCHAR(255) NOT NULL UNIQUE,
    WritingTypeId INT NOT NULL FOREIGN KEY REFERENCES WritingTypes(Id),
    Subtitle NVARCHAR(500),
    Excerpt NVARCHAR(1000),
    FullContent NVARCHAR(MAX),
    DatePublished DATE,
    NovelName NVARCHAR(255),
    ChapterNumber INT,
    DisplayOrder INT DEFAULT 0,
    CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 DEFAULT GETUTCDATE()
);
GO

CREATE INDEX IX_Writings_Slug ON Writings(Slug);
CREATE INDEX IX_Writings_TypeId ON Writings(WritingTypeId);
GO

-- ============================================================
-- 9. HeroContent
-- ============================================================
CREATE TABLE HeroContent (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Quote NVARCHAR(500),
    QuoteAttribution NVARCHAR(100),
    Headline NVARCHAR(255),
    Subheading NVARCHAR(MAX),
    FeaturedImageId INT FOREIGN KEY REFERENCES Images(Id),
    BadgeText NVARCHAR(100),
    PrimaryCtaText NVARCHAR(100),
    PrimaryCtaLink NVARCHAR(255),
    SecondaryCtaText NVARCHAR(100),
    SecondaryCtaLink NVARCHAR(255),
    IsActive BIT DEFAULT 1,
    UpdatedAt DATETIME2 DEFAULT GETUTCDATE()
);
GO

-- ============================================================
-- 10. ContactSubmissions
-- ============================================================
CREATE TABLE ContactSubmissions (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    Subject NVARCHAR(255) NOT NULL,
    Budget NVARCHAR(100),
    Message NVARCHAR(MAX) NOT NULL,
    SubmittedAt DATETIME2 DEFAULT GETUTCDATE(),
    IsRead BIT DEFAULT 0,
    IsResponded BIT DEFAULT 0
);
GO

CREATE INDEX IX_ContactSubmissions_SubmittedAt ON ContactSubmissions(SubmittedAt DESC);
GO

-- ============================================================
-- 11. NavigationItems
-- ============================================================
CREATE TABLE NavigationItems (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Label NVARCHAR(50) NOT NULL,
    Link NVARCHAR(255) NOT NULL,
    IconSvg NVARCHAR(MAX),
    DisplayOrder INT DEFAULT 0,
    IsActive BIT DEFAULT 1
);
GO

-- ============================================================
-- 12. SocialLinks
-- ============================================================
CREATE TABLE SocialLinks (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Platform NVARCHAR(50) NOT NULL,
    Url NVARCHAR(500) NOT NULL,
    IconSvg NVARCHAR(MAX),
    DisplayOrder INT DEFAULT 0,
    IsActive BIT DEFAULT 1
);
GO

PRINT 'All tables created successfully.';
GO
