using Books.Entities;
using Books.Requests.Books;
using Books.Responses;

namespace Books.Interfaces.Services.Books;

public interface IUpdateBookStatus
{
    Task<Response<Book?>> UpdateStatus(Guid id, UpdateBookRequest request);  
}