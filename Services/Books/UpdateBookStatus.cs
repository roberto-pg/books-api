using Books.Data;
using Books.Entities;
using Books.Interfaces.Services.Books;
using Books.Requests.Books;
using Books.Responses;
using Microsoft.EntityFrameworkCore;

namespace Books.Services.Books;

public class UpdateBookStatus(AppDbContext dbContext) : IUpdateBookStatus
{
    public async Task<Response<Book?>> UpdateStatus(Guid id, UpdateBookRequest request)
    {
        try
        {
            var updatedBook = await dbContext
                .Books
                .FirstOrDefaultAsync(book => book.Id == id);

            if (updatedBook is null)
                return new Response<Book?>(null, 404, "Book not found");

            updatedBook.Read = request.Read;

            dbContext.Books.Update(updatedBook);
            await dbContext.SaveChangesAsync();

            return new Response<Book?>(updatedBook, 200, "Book status updated successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[UpdateBookStatus] Error: {ex.Message}");
            return new Response<Book?>(null, 500, "Error updating book status");
        }
    }
}