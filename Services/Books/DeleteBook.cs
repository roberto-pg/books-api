using Books.Data;
using Books.Entities;
using Books.Interfaces.Services.Books;
using Books.Responses;
using Books.Utils;
using Microsoft.EntityFrameworkCore;

namespace Books.Services.Books;

public class DeleteBook(AppDbContext dbContext, IWebHostEnvironment env) : IDeleteBook
{
    public async Task<Response<Book?>> Delete(Guid id)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync();

        try
        {
            var book = await dbContext
                .Books
                .FirstOrDefaultAsync(book => book.Id == id);

            if (book is null)
                return new Response<Book?>(null, 404, "Book not found");

            dbContext.Books.Remove(book);
            await dbContext.SaveChangesAsync();
            await transaction.CommitAsync();

         HandleImage.DeleteImage(book.ImageUrl, env);

            return new Response<Book?>(book, 200, "Book deleted successfully");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();

            Console.WriteLine($"[DeleteBook] Error: {ex.Message}");
            return new Response<Book?>(null, 500, "book not found");
        }
    }
}