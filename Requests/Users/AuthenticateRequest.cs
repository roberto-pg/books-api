using System.ComponentModel.DataAnnotations;
using Books.Entities;

namespace Books.Requests.Users;

public class AuthenticateRequest
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email is not valid")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
    public string Password { get; set; } = string.Empty;

    public static implicit operator User(AuthenticateRequest request)
        => new User
        {
            Email = request.Email,
            Password = request.Password
        };
}