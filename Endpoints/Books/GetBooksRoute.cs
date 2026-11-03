using Books.Entities;
using Books.Interfaces;
using Books.Interfaces.Services;
using Books.Responses;

namespace Books.Endpoints.Books;

public class GetBooksRoute : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/get-books", HandleAsync)
            .Produces<Response<List<Book>?>>()
            .RequireAuthorization();
    }

    private static async Task<IResult> HandleAsync(HttpResponse httpResponse, IGetAllBooks service)
    {
        var response = await service.GetBooks();

        httpResponse.Headers.Append("X-Total-Count", response.Data?.Count.ToString() ?? "0");

        return response.IsSuccess
            ? TypedResults.Ok(response)
            : TypedResults.BadRequest(response);
    }
}