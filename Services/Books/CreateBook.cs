using Books.Data;
using Books.Entities;
using Books.Interfaces.Services;
using Books.Requests.Books;
using Books.Responses;
using Books.Utils;

namespace Books.Services.Books;

public class CreateBook(AppDbContext dbContext, IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor) : ICreateBook
{
    public async Task<Response<Book?>> Create(CreateBookRequest request)
    {
        string? imageUrl = null;

        await using var transaction = await dbContext.Database.BeginTransactionAsync();

        try
        {
            if (request.ImageFile is not null && request.ImageFile.Length > 0)
            {
                imageUrl = await HandleImage.SaveImageAsync(request.ImageFile, env, httpContextAccessor);
                request.ImageUrl = imageUrl ?? string.Empty;
            }

            Book book = request;

            dbContext.Books.Add(book);
            await dbContext.SaveChangesAsync();

            await transaction.CommitAsync();

            return new Response<Book?>(book, 201, "Book created successfully");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();

            if (!string.IsNullOrEmpty(imageUrl))
                HandleImage.DeleteImage(imageUrl, env);

            Console.WriteLine($"[CreateBook] Error: {ex.Message}");
            return new Response<Book?>(null, 500, "Error creating book");
        }
    }
}