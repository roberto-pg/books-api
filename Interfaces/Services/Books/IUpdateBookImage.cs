using Books.Entities;
using Books.Requests.Books;
using Books.Responses;

namespace Books.Interfaces.Services.Books;

public interface IUpdateBookImage
{
    Task<Response<Book?>> UpdateImage(Guid id, UpdateBookRequest request);
}