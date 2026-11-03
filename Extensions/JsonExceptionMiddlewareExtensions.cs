using Books.Middleware;

namespace Books.Extensions;

public static class JsonExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseJsonExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<JsonExceptionMiddleware>();
    }
}