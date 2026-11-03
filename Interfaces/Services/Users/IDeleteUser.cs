using Books.Entities;
using Books.Responses;

namespace Books.Interfaces.Services.Users;

public interface IDeleteUser
{
    Task<Response<User?>> Delete(Guid id);
}