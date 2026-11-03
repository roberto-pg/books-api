using Books.Data;
using Books.Entities;
using Books.Interfaces.Services.Users;
using Books.Responses;
using Microsoft.EntityFrameworkCore;

namespace Books.Services.Users;

public class GetAllAllUsers(AppDbContext dbContext) : IGetAllUsers
{
    public async Task<Response<List<User>?>> GetUsers()
    {
        try
        {
            var users = await dbContext
                .Users
                .ToListAsync();

            return new Response<List<User>?>(users, 200, "Users retrieved successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[GetAllAllUsers] Error: {ex.Message}");
            return new Response<List<User>?>(null, 500, "Error retrieving users");
        }
    }
}