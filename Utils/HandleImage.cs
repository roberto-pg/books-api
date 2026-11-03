namespace Books.Utils;

public class HandleImage
{
    public static async Task<string?> SaveImageAsync(IFormFile? file, IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
    {
        if (file is null || file.Length == 0)
            return null;

        try
        {
            var imagesFolder = Path.Combine(env.WebRootPath, "images");
            if (!Directory.Exists(imagesFolder))
                Directory.CreateDirectory(imagesFolder);

            var fileExtension = Path.GetExtension(file.FileName);
            var uniqueFileName = $"{Path.GetFileNameWithoutExtension(file.FileName)}_{DateTime.UtcNow.Ticks}{fileExtension}";

            var filePath = Path.Combine(imagesFolder, uniqueFileName);

            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            
            var request = httpContextAccessor.HttpContext?.Request;
            var baseUrl = $"{request?.Scheme}://{request?.Host}";

            return $"{baseUrl}/images/{uniqueFileName}";
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[FileHelper] Erro ao salvar imagem: {ex.Message}");
            return null;
        }
    }
    public static void DeleteImage(string? imageUrl, IWebHostEnvironment env)
    {
        if (string.IsNullOrWhiteSpace(imageUrl))
            return;

        try
        {
            var fileName = Path.GetFileName(imageUrl);

            var imagePath = Path.Combine(env.WebRootPath, "images", fileName);

            if (File.Exists(imagePath))
                File.Delete(imagePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[FileHelper] Erro ao excluir imagem: {ex.Message}");
        }
    }

    public static string GetPhysicalPath(string fileName, IWebHostEnvironment env)
    {
        return Path.Combine(env.WebRootPath, "images", fileName);
    }

    public static string GetPublicUrl(string fileName)
    {
        return $"/images/{fileName}";
    }
}