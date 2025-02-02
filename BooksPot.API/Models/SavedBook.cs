namespace BooksPot.API.Models;

public class SavedBook
{
    public int Id { get; set; }
    public string Isbn { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string CoverImage { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsDeleted { get; set; } = false;
    
    //foreign key
    public string UserId { get; set; }
    
    private SavedBook(){}

    public SavedBook(
        string isbn,
        string title,
        string author,
        string coverImage,
        string userId)
    {
        Isbn = isbn;
        Title = title;
        Author = author;
        CoverImage = coverImage;
        UserId = userId;
    }

    public SavedBook CreateAsync(
        string isbn,
        string title,
        string author,
        string coverImage,
        string userId)
    {
        Isbn = isbn;
        Title = title;
        Author = author;
        CoverImage = coverImage;
        UserId = userId;

        return this;
    }

    public void DeleteAsync(int id)
    {
        IsDeleted = true;
    }
}