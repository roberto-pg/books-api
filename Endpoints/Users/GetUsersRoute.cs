using Books.Entities;
using Books.Interfaces;
using Books.Interfaces.Services.Users;
using Books.Responses;

namespace Books.Endpoints.Users;

public class GetUsersRoute : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/get-users", HandleAsync)
            .Produces<Response<List<User>?>>()
            .RequireAuthorization();
    }

    private static async Task<IResult> HandleAsync(IGetAllUsers service)
    {
        var response = await service.GetUsers();

        return response.IsSuccess
            ? TypedResults.Ok(response)
            : TypedResults.BadRequest(response);
    }
}