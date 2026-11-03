using System.ComponentModel.DataAnnotations;
using Books.Entities;

namespace Books.Requests.Users;

public class CreateUserRequest : Request
{
    [Required(ErrorMessage = "Name is required")]
    [MinLength(3, ErrorMessage = "Name must be at least 3 characters")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email is not valid")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
    public string Password { get; set; } = string.Empty;

    public static implicit operator User(CreateUserRequest request)
        => new User
        {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password
        };
}