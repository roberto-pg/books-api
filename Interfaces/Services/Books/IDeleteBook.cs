using Books.Entities;
using Books.Responses;

namespace Books.Interfaces.Services.Books;

public interface IDeleteBook
{
    Task<Response<Book?>> Delete(Guid id);
}