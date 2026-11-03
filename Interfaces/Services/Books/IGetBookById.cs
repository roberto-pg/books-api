using Books.Entities;
using Books.Responses;

namespace Books.Interfaces.Services;

public interface IGetBookById
{
    Task<Response<Book?>> BookById(Guid id);  
}