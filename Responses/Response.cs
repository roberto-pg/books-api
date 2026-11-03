using System.Text.Json.Serialization;

namespace Books.Responses;

public class Response<TData>
{
    private readonly int _code;

    [JsonConstructor]
    public Response() => _code = GetDefaultStatusCode();

    public Response(
        TData? data,
        int code,
        string? message = null
    )
    {
        Data = data;
        _code = code;
        Message = message;
    }

    public TData? Data { get; private set; }
    public string? Message { get; private set; }
    public int StatusCode => _code;

    [JsonIgnore] public bool IsSuccess => _code is >= 200 and <= 299;
    
    private static int GetDefaultStatusCode()
    {
        const int code = 200;
        return code;
    }
}