using Books.Entities;
using Books.Interfaces;
using Books.Interfaces.Services.Books;
using Books.Requests.Books;
using Books.Responses;

namespace Books.Endpoints.Books;

public class UpdateBookStatusRoute : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPatch("/update-status/{id:guid}", HandleAsync)
            .Produces<Response<Book?>>()
            .RequireAuthorization();
    }

    private static async Task<IResult> HandleAsync(IUpdateBookStatus service, Guid id, UpdateBookRequest request)
    {
        var response = await service.UpdateStatus(id, request);

        return response.IsSuccess
            ? TypedResults.Ok(response)
            : TypedResults.BadRequest(response);
    }
}