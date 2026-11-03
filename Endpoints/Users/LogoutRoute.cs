using Books.Interfaces;

namespace Books.Endpoints.Users;

public class LogoutRoute : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/logout", HandleAsync);
    }

    private static IResult HandleAsync(HttpContext httpContext)
    {
        httpContext.Response.Cookies.Delete("access_token", new CookieOptions
        {
            HttpOnly = true,
            Secure = false,
            SameSite = SameSiteMode.Lax
        });

        return Results.Ok(new { message = "Logout realizado com sucesso" });
    }
}