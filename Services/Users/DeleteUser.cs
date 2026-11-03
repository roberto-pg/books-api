using Books.Data;
using Books.Entities;
using Books.Interfaces.Services.Users;
using Books.Responses;
using Microsoft.EntityFrameworkCore;

namespace Books.Services.Users;

public class DeleteUser(AppDbContext dbContext) : IDeleteUser
{
    public async Task<Response<User?>> Delete(Guid id)
    {
        try
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user is null)
                return new Response<User?>(null, 404, "User not found");

            dbContext.Users.Remove(user);
            await dbContext.SaveChangesAsync();
 
            return new Response<User?>(user, 200, "User deleted successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[DeleteUser] Error: {ex.Message}");
            return new Response<User?>(null, 500, "Error deleting user");
        }
    }
}