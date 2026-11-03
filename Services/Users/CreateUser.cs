using Books.Data;
using Books.Entities;
using Books.Interfaces.Services.Users;
using Books.Requests.Users;
using Books.Responses;
using Books.Utils;

namespace Books.Services.Users;

public class CreateUser(AppDbContext dbContext) : ICreateUser
{
    public async Task<Response<User?>> Create(CreateUserRequest request)
    {
        User user = request;

        try
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
            
            return new Response<User?>(user, 201, "User created successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[CreateUser] Error: {ex.Message}");
            return new Response<User?>(null, 500, "Error creating user");
        }
    }
}