namespace BooksPot.API.Models;

public class SavedBook
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string CoverImage { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    //foreign key
    public int UserId { get; set; }
}