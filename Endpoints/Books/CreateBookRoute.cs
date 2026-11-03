using Books.Entities;
using Books.Interfaces;
using Books.Interfaces.Services;
using Books.Requests.Books;
using Books.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Books.Endpoints.Books;

public class CreateBookRoute : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/create-book", HandleAsync)
            .DisableAntiforgery()
            .Accepts<IFormFile>("multipart/form-data")
            .Produces<Response<Book?>>(201)
            .WithName("CreateBook")
            .RequireAuthorization();
    }

    private static async Task<IResult> HandleAsync([FromForm] CreateBookRequest request, ICreateBook service)
    {
        var response = await service.Create(request);

        return response.IsSuccess
            ? TypedResults.Created("", response)
            : TypedResults.BadRequest(response);
    }
}