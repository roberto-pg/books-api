using Books.Entities;
using Books.Interfaces;
using Books.Interfaces.Services.Books;
using Books.Requests.Books;
using Books.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Books.Endpoints.Books;

public class UpdateBookImageRoute : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPatch("update-image/{id:guid}", HandleAsync)
            .DisableAntiforgery()
            .Accepts<IFormFile>("multipart/form-data")
            .Produces<Response<Book?>>()
            .WithName("UpdateBookImage")
            .RequireAuthorization();
    }

    private static async Task<IResult> HandleAsync([FromForm] UpdateBookRequest request, IUpdateBookImage service,
        Guid id)
    {
        var response = await service.UpdateImage(id, request);

        return response.IsSuccess
            ? TypedResults.Ok(response)
            : TypedResults.BadRequest(response);
    }
}