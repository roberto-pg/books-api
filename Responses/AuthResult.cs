using Books.Entities;

namespace Books.Responses;

public class AuthResult
{
    public User? User { get; set; }
    public string? Token { get; set; }
    public bool Success { get; set; }
    public string? Message { get; set; }
}