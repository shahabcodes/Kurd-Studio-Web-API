-- ============================================================
-- Kurd Studio Seed Data
-- ============================================================

USE KurdStudioDb;
GO

-- ============================================================
-- ARTWORK TYPES
-- ============================================================
INSERT INTO ArtworkTypes (TypeName, DisplayName, DisplayOrder) VALUES
('painting', 'Painting', 1),
('sketch', 'Sketch', 2),
('mixed', 'Mixed Media', 3);
GO

-- ============================================================
-- WRITING TYPES
-- ============================================================
INSERT INTO WritingTypes (TypeName, DisplayName, DisplayOrder) VALUES
('reflection', 'Reflection', 1),
('poem', 'Poem', 2),
('novel', 'Novel Excerpt', 3);
GO

-- ============================================================
-- SITE SETTINGS
-- ============================================================
INSERT INTO SiteSettings (SettingKey, SettingValue, SettingType) VALUES
('site_name', 'Kurd Studio', 'string'),
('site_tagline', 'Art & Words', 'string'),
('site_description', 'Kurd Studio — Sanya''s sketches, paintings, poems, reflections, and a novel in progress.', 'string'),
('theme_color', '#ec4899', 'color'),
('footer_quote', 'She turned her dreams into her plans.', 'string'),
('footer_quote_attribution', 'Inspirational', 'string'),
('copyright_text', 'Kurd Studio · All rights reserved', 'string');
GO

-- ============================================================
-- IMAGES (Placeholder data - actual binary will be loaded separately)
-- For development, we'll use placeholder URLs that the API will proxy
-- ============================================================
-- Note: In production, these would contain actual VARBINARY data
-- For now, we store the picsum URLs as a reference

-- Hero/Featured Image
INSERT INTO Images (FileName, ContentType, ImageData, AltText, Width, Height)
VALUES ('hero-featured.jpg', 'image/jpeg', 0x00, 'Featured artwork: Abstract oil painting on canvas', 1200, 1500);

-- Avatar Image
INSERT INTO Images (FileName, ContentType, ImageData, AltText, Width, Height)
VALUES ('avatar-sanya.jpg', 'image/jpeg', 0x00, 'Portrait of Sanya, the artist', 900, 1200);

-- Artwork Images
INSERT INTO Images (FileName, ContentType, ImageData, AltText, Width, Height)
VALUES ('dawn-at-the-quay.jpg', 'image/jpeg', 0x00, 'Painting: Dawn at the Quay', 800, 600);

INSERT INTO Images (FileName, ContentType, ImageData, AltText, Width, Height)
VALUES ('lines-of-silence.jpg', 'image/jpeg', 0x00, 'Sketch: Lines of Silence', 800, 600);

INSERT INTO Images (FileName, ContentType, ImageData, AltText, Width, Height)
VALUES ('orbiting-thought.jpg', 'image/jpeg', 0x00, 'Mixed Media: Orbiting Thought', 800, 600);

INSERT INTO Images (FileName, ContentType, ImageData, AltText, Width, Height)
VALUES ('tide-and-ember.jpg', 'image/jpeg', 0x00, 'Painting: Tide and Ember', 800, 600);

INSERT INTO Images (FileName, ContentType, ImageData, AltText, Width, Height)
VALUES ('paper-wings.jpg', 'image/jpeg', 0x00, 'Sketch: Paper Wings', 800, 600);

INSERT INTO Images (FileName, ContentType, ImageData, AltText, Width, Height)
VALUES ('echo-layers.jpg', 'image/jpeg', 0x00, 'Mixed Media: Echo Layers', 800, 600);
GO

-- ============================================================
-- PROFILE
-- ============================================================
INSERT INTO Profile (Name, Tagline, Bio, AvatarImageId, Email, InstagramUrl, TwitterUrl, ArtworksCount, PoemsCount, YearsExperience)
VALUES (
    'Sanya',
    'Artist · Writer · Dreamer of beautiful things',
    'Hi, I''m Sanya. I draw because graphite understands what words can''t. I paint because colors argue beautifully. I write to translate the hush between both. Commission inquiries, collaboration ideas, or just a hello are always welcome.',
    2, -- Avatar Image ID
    'hello@kurd.studio',
    'https://instagram.com/',
    'https://twitter.com/',
    '50+',
    '30+',
    '5'
);
GO

-- ============================================================
-- SECTIONS
-- ============================================================
INSERT INTO Sections (SectionKey, Tag, Title, Subtitle, DisplayOrder, IsActive) VALUES
('gallery', 'Portfolio', 'Creative Gallery', 'Sketches · Paintings · Mixed Media', 1, 1),
('writing', 'Literature', 'Words & Stories', 'Reflections · Poems · Novel in Progress', 2, 1),
('about', 'About Me', 'Meet Sanya', 'Artist · Writer · Dreamer of beautiful things', 3, 1),
('contact', 'Get in Touch', 'Let''s Connect', 'Have a project in mind? Let''s make something unforgettable.', 4, 1);
GO

-- ============================================================
-- HERO CONTENT
-- ============================================================
INSERT INTO HeroContent (
    Quote, QuoteAttribution, Headline, Subheading,
    FeaturedImageId, BadgeText,
    PrimaryCtaText, PrimaryCtaLink,
    SecondaryCtaText, SecondaryCtaLink,
    IsActive
) VALUES (
    'Create the beauty you wish to see in the world',
    NULL,
    'Art that feels. Words that linger.',
    'Sketches and paintings that breathe, poems and reflections that listen. Explore a living portfolio of visuals and verse — curated by Sanya.',
    1, -- Featured Image ID
    'New Collection',
    'Explore Gallery', '#gallery',
    'Read the Words', '#writing',
    1
);
GO

-- ============================================================
-- ARTWORKS
-- ============================================================
INSERT INTO Artworks (Title, Slug, ArtworkTypeId, ImageId, Description, IsFeatured, DisplayOrder) VALUES
('Dawn at the Quay', 'dawn-at-the-quay', 1, 3, 'An oil painting capturing the serene moments of dawn at the quay.', 0, 1),
('Lines of Silence', 'lines-of-silence', 2, 4, 'A pencil sketch exploring the quiet spaces between words.', 0, 2),
('Orbiting Thought', 'orbiting-thought', 3, 5, 'Mixed media piece contemplating the circular nature of consciousness.', 0, 3),
('Tide & Ember', 'tide-and-ember', 1, 6, 'Oil painting juxtaposing water and fire, calm and passion.', 0, 4),
('Paper Wings', 'paper-wings', 2, 7, 'Delicate sketch of wings formed from folded paper.', 0, 5),
('Echo Layers', 'echo-layers', 3, 8, 'Layered mixed media exploring the concept of memory and repetition.', 0, 6);
GO

-- ============================================================
-- WRITINGS
-- ============================================================
-- Reflection
INSERT INTO Writings (Title, Slug, WritingTypeId, Subtitle, Excerpt, FullContent, DatePublished, DisplayOrder) VALUES
(
    'The Day We Painted Quiet',
    'the-day-we-painted-quiet',
    1,
    'October 2025',
    'The studio window learned our names before we learned the light. Dust floated like tiny planets and every brushstroke spun its own gravity.',
    'Morning arrived on tiptoe, putting its palms on the glass. We waited for the color to warm, for tea to forgive its steam, for the first line to be brave enough to cross the cold paper. You told me, quiet is a bridge. I believed you—not because bridges don''t tremble, but because they do.

I learned to listen with my shoulders, to let the breath be a metronome, to make a promise to the empty space and keep it. The work never shouts. It simply turns toward you when you turn toward it.',
    '2025-10-15',
    1
);

-- Poems
INSERT INTO Writings (Title, Slug, WritingTypeId, Subtitle, Excerpt, FullContent, DatePublished, DisplayOrder) VALUES
(
    'Blueprint of a City I Loved',
    'blueprint-of-a-city-i-loved',
    2,
    'A cartography of rooftops and night names',
    'We drew the river with a borrowed pencil, soft lead, a weather of graphite and wrists.',
    'We drew the river with a borrowed pencil,
soft lead, a weather of graphite and wrists.

Rooftops kept their knees together, shy;
stairwells practiced echo like a scale.

Somewhere a window refused to close—
the night slipped in, letter by letter,
spelling your name on the ceiling fan.

By morning, the city had a heartbeat;
we folded the map and it beat in our hands.',
    '2025-09-20',
    1
),
(
    'How Graphite Sleeps',
    'how-graphite-sleeps',
    2,
    'The pencil becomes a moth',
    'The pencil sleeps with one eye open— a moth pretending to be a branch.',
    'The pencil sleeps with one eye open—
a moth pretending to be a branch.

When the room exhales its lamps,
it rises, dust-lit, to circle the page,
looking for the warmest margin.

In that glow, it remembers
every line it could not finish,
every face half-turned—
and lands, finally, on the word begin.',
    '2025-08-10',
    2
),
(
    'If We Carry Water',
    'if-we-carry-water',
    2,
    'Love as a vessel',
    'If we carry water in our hands, the path will teach us balance.',
    'If we carry water in our hands,
the path will teach us balance.

If we carry water in our mouths,
the words will learn to be careful.

But if we carry water in our hearts,
even the stones will soften their edges.',
    '2025-07-05',
    3
);

-- Novel Excerpts
INSERT INTO Writings (Title, Slug, WritingTypeId, Subtitle, Excerpt, FullContent, NovelName, ChapterNumber, DatePublished, DisplayOrder) VALUES
(
    'Excerpt I',
    'the-quiet-meridian-excerpt-1',
    3,
    'Two cities, one river, and a chorus of small, stubborn miracles',
    'The river didn''t divide the city so much as tune it.',
    'The river didn''t divide the city so much as tune it. On our side, mornings began low and amber, shopkeepers clinking shutters like wind chimes. Across the water, trams stitched the fog with silver thread. I met you halfway, where the bridge kept its secrets in the bolts. You said the tide had two names. I believed you.',
    'The Quiet Meridian',
    1,
    '2025-06-01',
    1
),
(
    'Excerpt II',
    'the-quiet-meridian-excerpt-2',
    3,
    'That winter, we measured time in kettles',
    'That winter, we measured time in kettles. Every hour a warm, whistled vow.',
    'That winter, we measured time in kettles. Every hour a warm, whistled vow. We pinned rough drafts above the radiator and watched the paper curl like listening ears. When the power left the street, the dark became a room we could afford. You held the book we hadn''t written yet the way sailors hold a compass: not for where it points, but for how it steadies the hand.',
    'The Quiet Meridian',
    2,
    '2025-06-15',
    2
);
GO

-- ============================================================
-- NAVIGATION ITEMS
-- ============================================================
INSERT INTO NavigationItems (Label, Link, IconSvg, DisplayOrder, IsActive) VALUES
('Gallery', '#gallery', '<svg viewBox="0 0 24 24"><rect x="3" y="3" width="7" height="7" rx="1"/><rect x="14" y="3" width="7" height="7" rx="1"/><rect x="3" y="14" width="7" height="7" rx="1"/><rect x="14" y="14" width="7" height="7" rx="1"/></svg>', 1, 1),
('Writing', '#writing', '<svg viewBox="0 0 24 24"><path d="M3 6h18M3 12h12M3 18h8"/></svg>', 2, 1),
('About', '#about', '<svg viewBox="0 0 24 24"><circle cx="12" cy="8" r="4"/><path d="M4 21c2-4 6-6 8-6s6 2 8 6"/></svg>', 3, 1),
('Contact', '#contact', '<svg viewBox="0 0 24 24"><path d="M21 11.5a8.4 8.4 0 0 1-.9 3.8 8.5 8.5 0 0 1-7.6 4.7 8.4 8.4 0 0 1-3.8-.9L3 21l1.9-5.7a8.4 8.4 0 0 1-.9-3.8 8.5 8.5 0 0 1 4.7-7.6 8.4 8.4 0 0 1 3.8-.9h.5a8.5 8.5 0 0 1 8 8v.5z"/></svg>', 4, 1);
GO

-- ============================================================
-- SOCIAL LINKS
-- ============================================================
INSERT INTO SocialLinks (Platform, Url, IconSvg, DisplayOrder, IsActive) VALUES
('Instagram', 'https://instagram.com/', '<svg viewBox="0 0 24 24"><linearGradient id="ig" x1="0" y1="0" x2="1" y2="1"><stop offset="0%" stop-color="#F58529"/><stop offset="50%" stop-color="#DD2A7B"/><stop offset="100%" stop-color="#8134AF"/></linearGradient><rect x="3" y="3" width="18" height="18" rx="5" fill="url(#ig)"/><circle cx="12" cy="12" r="4" fill="#fff"/><circle cx="17.5" cy="6.5" r="1.2" fill="#fff"/></svg>', 1, 1),
('Twitter', 'https://twitter.com/', '<svg viewBox="0 0 24 24"><path fill="#1DA1F2" d="M23 3a10.9 10.9 0 0 1-3.14 1.53 4.48 4.48 0 0 0-7.86 3v1A10.66 10.66 0 0 1 3 4s-4 9 5 13a11.64 11.64 0 0 1-7 2c9 5 20 0 20-11.5a4.5 4.5 0 0 0-.08-.83A7.72 7.72 0 0 0 23 3z"/></svg>', 2, 1),
('Email', 'mailto:hello@kurd.studio', '<svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><path d="M4 4h16c1.1 0 2 .9 2 2v12c0 1.1-.9 2-2 2H4c-1.1 0-2-.9-2-2V6c0-1.1.9-2 2-2z"/><polyline points="22,6 12,13 2,6"/></svg>', 3, 1);
GO

PRINT 'Seed data inserted successfully.';
GO

-- ============================================================
-- VERIFICATION QUERIES
-- ============================================================
PRINT '--- Verification ---';
SELECT 'SiteSettings' AS [Table], COUNT(*) AS [Count] FROM SiteSettings
UNION ALL SELECT 'Profile', COUNT(*) FROM Profile
UNION ALL SELECT 'Artworks', COUNT(*) FROM Artworks
UNION ALL SELECT 'Writings', COUNT(*) FROM Writings
UNION ALL SELECT 'Images', COUNT(*) FROM Images
UNION ALL SELECT 'NavigationItems', COUNT(*) FROM NavigationItems
UNION ALL SELECT 'SocialLinks', COUNT(*) FROM SocialLinks;
GO
