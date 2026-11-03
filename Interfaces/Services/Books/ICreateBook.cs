using Books.Entities;
using Books.Requests.Books;
using Books.Responses;

namespace Books.Interfaces.Services;

public interface ICreateBook
{
 Task<Response<Book?>> Create(CreateBookRequest request);   
}