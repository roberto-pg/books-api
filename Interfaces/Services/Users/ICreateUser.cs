using Books.Entities;
using Books.Requests.Users;
using Books.Responses;

namespace Books.Interfaces.Services.Users;

public interface ICreateUser
{
    Task<Response<User?>> Create(CreateUserRequest request);
}