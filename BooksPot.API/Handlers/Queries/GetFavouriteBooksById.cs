using BooksPot.API.Context;
using BooksPot.API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BooksPot.API.Handlers.Queries;

public record GetFavouriteBooksById(string UserId);

public class GetFavouriteBooksByIdHandler
{
    private readonly ApplicationDbContext _context;

    public GetFavouriteBooksByIdHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<BookDetailDto>> Handle(GetFavouriteBooksById query)
    {
        var favouriteBooks = await _context.SavedBooks
            .Where(b => b.UserId == query.UserId && !b.IsDeleted)
            .Select(b => new BookDetailDto(
                b.Id,
                b.Isbn,
                b.Title,
                b.Author,
                b.CoverImage,
                b.UserId,
                b.CreatedAt))
            .ToListAsync();

        return favouriteBooks;
    }
}