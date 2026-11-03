using Books.Entities;
using Books.Responses;

namespace Books.Interfaces.Services;

public interface IGetBooksByStatus
{
    Task<Response<List<Book>?>> BooksByStatus(bool read);
}