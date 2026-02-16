using Microsoft.Data.SqlClient;
using SkiaSharp;

const string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=KurdStudioDb;Integrated Security=True;TrustServerCertificate=True";

var imageUrls = new Dictionary<int, (string url, string fileName, string alt)>
{
    { 1, ("https://picsum.photos/id/1011/1200/1500", "featured-hero.jpg", "Featured artwork: Abstract oil painting on canvas") },
    { 2, ("https://picsum.photos/id/1005/900/1200",  "avatar-sanya.jpg",  "Portrait of Sanya, the artist") },
    { 3, ("https://picsum.photos/id/1025/800/600",   "dawn-at-the-quay.jpg", "Dawn at the Quay - Oil painting") },
    { 4, ("https://picsum.photos/id/1067/800/600",   "whisper-of-graphite.jpg", "Whisper of Graphite - Pencil sketch") },
    { 5, ("https://picsum.photos/id/1035/800/600",   "between-two-silences.jpg", "Between Two Silences - Watercolor") },
    { 6, ("https://picsum.photos/id/1043/800/600",   "petals-in-mercury.jpg", "Petals in Mercury - Mixed media") },
    { 7, ("https://picsum.photos/id/1050/800/600",   "old-letters-new-light.jpg", "Old Letters, New Light - Charcoal") },
    { 8, ("https://picsum.photos/id/106/800/600",    "gravity-of-bloom.jpg", "Gravity of Bloom - Acrylic on canvas") },
};

using var httpClient = new HttpClient();
httpClient.DefaultRequestHeaders.Add("User-Agent", "KurdStudio-ImageSeeder/1.0");

Console.WriteLine("Downloading images from picsum.photos and storing in database...\n");

foreach (var (id, (url, fileName, alt)) in imageUrls)
{
    try
    {
        Console.Write($"  [{id}/8] Downloading {fileName}... ");
        var imageBytes = await httpClient.GetByteArrayAsync(url);
        Console.WriteLine($"{imageBytes.Length / 1024}KB");

        // Generate thumbnail (300px wide, maintain aspect ratio)
        Console.Write($"         Generating thumbnail... ");
        var thumbnailBytes = GenerateThumbnail(imageBytes, 400);
        Console.WriteLine($"{thumbnailBytes.Length / 1024}KB");

        // Get image dimensions
        using var original = SKBitmap.Decode(imageBytes);
        var width = original?.Width ?? 0;
        var height = original?.Height ?? 0;

        // Update database
        Console.Write($"         Updating database... ");
        await using var conn = new SqlConnection(connectionString);
        await conn.OpenAsync();

        await using var cmd = new SqlCommand(@"
            UPDATE Images
            SET ImageData = @ImageData,
                ThumbnailData = @ThumbnailData,
                FileName = @FileName,
                AltText = @AltText,
                FileSize = @FileSize,
                Width = @Width,
                Height = @Height,
                ContentType = 'image/jpeg'
            WHERE Id = @Id", conn);

        cmd.Parameters.AddWithValue("@Id", id);
        cmd.Parameters.AddWithValue("@ImageData", imageBytes);
        cmd.Parameters.AddWithValue("@ThumbnailData", thumbnailBytes);
        cmd.Parameters.AddWithValue("@FileName", fileName);
        cmd.Parameters.AddWithValue("@AltText", alt);
        cmd.Parameters.AddWithValue("@FileSize", imageBytes.Length);
        cmd.Parameters.AddWithValue("@Width", width);
        cmd.Parameters.AddWithValue("@Height", height);

        var rows = await cmd.ExecuteNonQueryAsync();
        Console.WriteLine(rows > 0 ? "OK" : "NOT FOUND");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"ERROR: {ex.Message}");
    }
}

Console.WriteLine("\nDone! All images stored in database.");

static byte[] GenerateThumbnail(byte[] imageBytes, int maxWidth)
{
    using var original = SKBitmap.Decode(imageBytes);
    if (original == null) return imageBytes;

    var ratio = (float)maxWidth / original.Width;
    var newHeight = (int)(original.Height * ratio);

    using var resized = original.Resize(new SKImageInfo(maxWidth, newHeight), SKFilterQuality.Medium);
    if (resized == null) return imageBytes;

    using var image = SKImage.FromBitmap(resized);
    using var data = image.Encode(SKEncodedImageFormat.Jpeg, 75);
    return data.ToArray();
}
