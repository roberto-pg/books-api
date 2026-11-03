using Books.Entities;
using Books.Interfaces;
using Books.Interfaces.Services.Books;
using Books.Responses;

namespace Books.Endpoints.Books;

public class DeleteBookRoute : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapDelete("/delete-book/{id:guid}", HandleAsync)
            .Produces<Response<Book?>>()
            .RequireAuthorization();
    }

    private static async Task<IResult> HandleAsync(IDeleteBook service, Guid id)
    {
        var response = await service.Delete(id);

        return response.IsSuccess
            ? Results.Ok(response)
            : Results.BadRequest(response);
    }
}