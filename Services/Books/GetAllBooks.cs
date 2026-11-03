using Books.Data;
using Books.Entities;
using Books.Interfaces.Services;
using Books.Responses;
using Microsoft.EntityFrameworkCore;

namespace Books.Services.Books;

public class GetAllBooks(AppDbContext dbContext) : IGetAllBooks
{
    public async Task<Response<List<Book>?>> GetBooks()
    {
        try
        {
            var response = await dbContext
                .Books
                .OrderBy(book => book.Title)
                .ToListAsync();

            return new Response<List<Book>?>(response, 200, "Books retrieved successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[GetAllBooks] Error: {ex.Message}");
            return new Response<List<Book>?>(null, 500, "Error retrieving books");
        }
    }
}