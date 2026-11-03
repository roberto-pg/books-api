using Books.Entities;
using Books.Responses;

namespace Books.Interfaces.Services.Users;

public interface IGetAllUsers
{
    Task<Response<List<User>?>> GetUsers();
}