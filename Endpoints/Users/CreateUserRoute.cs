using Books.Entities;
using Books.Filters;
using Books.Interfaces;
using Books.Interfaces.Services.Users;
using Books.Requests.Users;
using Books.Responses;

namespace Books.Endpoints.Users;

public class CreateUserRoute : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/create-user", HandleAsync)
            .Produces<Response<User?>>();
    }

    private static async Task<IResult> HandleAsync(ICreateUser service, CreateUserRequest request)
    {
        if (!Validate.TryValidate(request, out var results))
            return Results.BadRequest(results.Select(r => r.ErrorMessage));

        var response = await service.Create(request);
        return response.IsSuccess
            ? TypedResults.Created("", response)
            : TypedResults.BadRequest(response);
    }
}