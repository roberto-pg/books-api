using Books.Entities;
using Books.Responses;

namespace Books.Interfaces.Services;

public interface IGetAllBooks
{
    Task<Response<List<Book>?>> GetBooks();  
}