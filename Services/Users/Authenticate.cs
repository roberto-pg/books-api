using Books.Data;
using Books.Entities;
using Books.Interfaces.Services.Users;
using Books.Requests.Users;
using Books.Responses;
using Books.Utils;
using Microsoft.EntityFrameworkCore;

namespace Books.Services.Users;

public class Authenticate(AppDbContext dbContext, AuthService authService) : IAuthenticate
{
    public async Task<AuthResult> Login(AuthenticateRequest request)
    {
        try
        {
            var user = await dbContext.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
                return new AuthResult
                {
                    Success = false,
                    Message = "User not found"
                };

            bool passwordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);

            if (!passwordValid)
                return new AuthResult
                {
                    Success = false,
                    Message = "Invalid password"
                };

            var token = authService.GenerateToken(user);

            return new AuthResult
            {
                Success = true,
                User = user,
                Token = token,
                Message = "Login successful"
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);

            return new AuthResult
            {
                Success = false,
                Message = "Error logging in"
            };
        }
    }
}