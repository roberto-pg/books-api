using Books.Entities;
using Books.Requests.Users;
using Books.Responses;

namespace Books.Interfaces.Services.Users;

public interface IAuthenticate
{
    Task<AuthResult> Login(AuthenticateRequest request);
}