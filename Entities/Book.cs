namespace Books.Entities;

public class Book
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Nationality { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public int Year { get; set; }
    public bool Read { get; set; }
}