using Books.Entities;
using Books.Interfaces;
using Books.Interfaces.Services;
using Books.Responses;

namespace Books.Endpoints.Books;

public class GetBooksByStatusRoute : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/books-by-status/{read:bool}", HandleAsync)
            .Produces<Response<List<Book>?>>()
            .RequireAuthorization();
    }

    private static async Task<IResult> HandleAsync(IGetBooksByStatus service, bool read)
    {
        var response = await service.BooksByStatus(read);

        return response.IsSuccess
            ? Results.Ok(response)
            : Results.BadRequest(response);
    }
}