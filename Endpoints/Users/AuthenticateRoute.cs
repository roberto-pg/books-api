using Books.Entities;
using Books.Filters;
using Books.Interfaces;
using Books.Interfaces.Services.Users;
using Books.Requests.Users;
using Books.Responses;

namespace Books.Endpoints.Users;

public class AuthenticateRoute : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/login", HandleAsync)
            .Produces<Response<User?>>();
    }

    private static async Task<IResult> HandleAsync(
        HttpContext httpContext,
        IAuthenticate service,
        AuthenticateRequest request)
    {
        if (!Validate.TryValidate(request, out var results))
            return Results.BadRequest(results);

        var result = await service.Login(request);

        if (!result.Success)
            return Results.BadRequest(result.Message);

        // 🔐 agora você tem o token aqui
        if (!string.IsNullOrEmpty(result.Token))
        {
            httpContext.Response.Cookies.Append("access_token", result.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Lax,
                Expires = DateTimeOffset.UtcNow.AddMinutes(60)
            });
        }

        return Results.Ok(new Response<User?>(
            result.User,
            200,
            result.Message
        ));
    }
}