using BooksPot.API.Context;

namespace BooksPot.API.Handlers.Commands;

public record RemoveFavouriteBookCommand(int Id);

public class RemoveFavouriteBookCommandHandler
{
    private readonly ApplicationDbContext _context;

    public RemoveFavouriteBookCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(RemoveFavouriteBookCommand command)
    {
        var book = await _context.SavedBooks.FindAsync(command.Id);
        if (book == null)
        {
            throw new InvalidOperationException("Book not found.");
        }

        book.DeleteAsync(command.Id);
        await _context.SaveChangesAsync();
    }
}