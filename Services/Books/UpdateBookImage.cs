using Books.Data;
using Books.Entities;
using Books.Interfaces.Services.Books;
using Books.Requests.Books;
using Books.Responses;
using Books.Utils;
using Microsoft.EntityFrameworkCore;

namespace Books.Services.Books;

public class UpdateBookImage(AppDbContext dbContext, IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor) : IUpdateBookImage
{
    public async Task<Response<Book?>> UpdateImage(Guid id, UpdateBookRequest request)
    {
        try
        {
            var book = await dbContext
                .Books
                .FirstOrDefaultAsync(book => book.Id == id);

            if (book is null)
                return new Response<Book?>(null, 404, "Book Not Found");

            string? newImageUrl = null;
            if (request.ImageFile is not null && request.ImageFile.Length > 0)
            {
                newImageUrl = await HandleImage.SaveImageAsync(request.ImageFile, env, httpContextAccessor);
            }

            if (string.IsNullOrWhiteSpace(newImageUrl))
                return new Response<Book?>(book, 400, "Invalid image file");

            HandleImage.DeleteImage(book.ImageUrl, env);

            book.ImageUrl = newImageUrl;
            await dbContext.SaveChangesAsync();

            return new Response<Book?>(book, 200, "Book Image Updated Successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[UpdateBookImage] Error: {ex.Message}");
            return new Response<Book?>(null, 500, "Internal Server Error");
        }
    }
}