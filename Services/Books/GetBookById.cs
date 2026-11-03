using Books.Data;
using Books.Entities;
using Books.Interfaces.Services;
using Books.Responses;
using Microsoft.EntityFrameworkCore;

namespace Books.Services.Books;

public class GetBookById(AppDbContext dbContext) : IGetBookById
{
    public async Task<Response<Book?>> BookById(Guid id)
    {
        try
        {
            var response = await dbContext
                .Books
                .FirstOrDefaultAsync(book => book.Id == id);

            return new Response<Book?>(response, 200, "Book retrieved successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[GetBookById] Error: {ex.Message}");
            return new Response<Book?>(null, 500, "Error retrieving book");
        }
    }
}