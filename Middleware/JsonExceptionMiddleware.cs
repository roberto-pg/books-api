using System.Text.Json;

namespace Books.Middleware;

public class JsonExceptionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (BadHttpRequestException ex) when (ex.InnerException is JsonException)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";

            var response = new
            {
                status = 400,
                message =
                    "Falha na serialização! Verifique os tipos de dados enviados.",
                success = false
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}