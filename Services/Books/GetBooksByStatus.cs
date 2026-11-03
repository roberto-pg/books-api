using Books.Data;
using Books.Entities;
using Books.Interfaces.Services;
using Books.Responses;
using Microsoft.EntityFrameworkCore;

namespace Books.Services.Books;

public class GetBooksByStatus(AppDbContext dbContext) : IGetBooksByStatus
{
    public async Task<Response<List<Book>?>> BooksByStatus(bool read)
    {
        try
        {
            var books = await dbContext
                .Books
                .Where(book => book.Read == read)
                .ToListAsync();

            return new Response<List<Book>?>(books, 200, "Books retrieved successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[GetBooksByStatus] Error: {ex.Message}");
            return new Response<List<Book>?>(null, 500, "Error retrieving books");
        }
    }
}