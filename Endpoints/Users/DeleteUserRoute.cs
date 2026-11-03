using Books.Entities;
using Books.Interfaces;
using Books.Interfaces.Services.Users;
using Books.Responses;

namespace Books.Endpoints.Users;

public class DeleteUserRoute : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapDelete("/delete-user/{id:guid}", HandleAsync)
            .Produces<Response<User?>>()
            .RequireAuthorization();
    }

    private static async Task<IResult> HandleAsync(IDeleteUser service, Guid id)
    {
        var response = await service.Delete(id);

        return response.IsSuccess
            ? Results.Ok(response)
            : Results.BadRequest(response);
    }
}