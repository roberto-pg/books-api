using Books.Entities;

namespace Books.Requests.Books;

public class CreateBookRequest : Request
{
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Nationality { get; set; } = string.Empty;
    public int Year { get; set; }
    public bool Read { get; set; }
    
    public IFormFile? ImageFile { get; set; }
    public string ImageUrl { get; set; } = string.Empty;

    public static implicit operator Book(CreateBookRequest request)
        => new Book
        {
            Title = request.Title,
            Author = request.Author,
            Nationality = request.Nationality,
            ImageUrl = request.ImageUrl,
            Year = request.Year,
            Read = request.Read
        };
}