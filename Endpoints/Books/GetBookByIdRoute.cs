using Books.Entities;
using Books.Interfaces;
using Books.Interfaces.Services;
using Books.Responses;

namespace Books.Endpoints.Books;

public class GetBookByIdRoute : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("book-by-id/{id:guid}", HandleAsync)
            .Produces<Response<Book?>>()
            .RequireAuthorization();
    }

    private static async Task<IResult> HandleAsync(IGetBookById service, Guid id)
    {
        var response = await service.BookById(id);

        return response.IsSuccess
            ? TypedResults.Ok(response)
            : TypedResults.BadRequest(response);
    }
}