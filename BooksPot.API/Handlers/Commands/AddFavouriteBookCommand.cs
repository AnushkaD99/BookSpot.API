using BooksPot.API.Context;
using BooksPot.API.DTOs;
using BooksPot.API.Models;

namespace BooksPot.API.Handlers.Commands;

public record AddFavouriteBookCommand(
    string Isbn,
    string Title,
    string Author,
    string CoverImage,
    string UserId);

public class AddFavouriteBookCommandHandler
{
    private readonly ApplicationDbContext _context;

    public AddFavouriteBookCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<BookDetailDto> Handle(AddFavouriteBookCommand command)
    {
        var favBook = new SavedBook(
            command.Isbn,
            command.Title,
            command.Author,
            command.CoverImage,
            command.UserId);
        
        var result = favBook.CreateAsync(
            command.Isbn, 
            command.Title, 
            command.Author, 
            command.CoverImage,
            command.UserId);

        await _context.SavedBooks.AddAsync(result);
        await _context.SaveChangesAsync();
        
        return new BookDetailDto(
            result.Isbn,
            result.Title,
            result.Author,
            result.CoverImage,
            result.UserId,
            result.CreatedAt);
    }
}