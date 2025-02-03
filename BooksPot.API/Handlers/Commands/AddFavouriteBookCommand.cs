using BooksPot.API.Context;
using BooksPot.API.DTOs;
using BooksPot.API.Models;
using Microsoft.EntityFrameworkCore;

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
        // Check if the book is already saved by the user
        var existingBook = await _context.SavedBooks
            .FirstOrDefaultAsync(b => b.UserId == command.UserId && b.Isbn == command.Isbn);

        if (existingBook != null)
        {
            throw new InvalidOperationException("This book is already saved.");
        }

        var favBook = new SavedBook(
            command.Isbn,
            command.Title,
            command.Author,
            command.CoverImage,
            command.UserId);

        await _context.SavedBooks.AddAsync(favBook);
        await _context.SaveChangesAsync();
        
        return new BookDetailDto(
            favBook.Id,
            favBook.Isbn,
            favBook.Title,
            favBook.Author,
            favBook.CoverImage,
            favBook.UserId,
            favBook.CreatedAt);
    }
}