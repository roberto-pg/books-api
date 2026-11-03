using System.Security.Claims;
using Books.Interfaces;
using Books.Responses;

namespace Books.Endpoints.Users;

public class IsAuthenticatedRoute : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/me", HandleAsync)
            .RequireAuthorization();
    }

    private static IResult HandleAsync(ClaimsPrincipal user)
    {
        return Results.Ok(new Response<object>(
            new { Name = user.Identity?.Name },
            200,
            "Usuário autenticado"
        ));
    }
}