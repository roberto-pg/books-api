namespace Books.Requests.Books;

public class UpdateBookRequest : Request
{
    public bool Read { get; set; }

    public IFormFile? ImageFile { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
}