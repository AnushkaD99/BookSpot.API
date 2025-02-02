using BooksPot.API.Context;
using BooksPot.API.Handlers.Commands;
using BooksPot.API.Handlers.Queries;
using BooksPot.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BooksPot.API.Controllers;

[Authorize]
[Route("api/book")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly AddFavouriteBookCommandHandler _addFavouriteBookCommandHandler;
    private readonly GetFavouriteBooksByIdHandler _getFavouriteBooksByIdHandler;
    private readonly RemoveFavouriteBookCommandHandler _removeFavouriteBookCommandHandler;
    public BookController(ApplicationDbContext context)
    {
        _context = context;
        _addFavouriteBookCommandHandler = new AddFavouriteBookCommandHandler(_context);
        _getFavouriteBooksByIdHandler = new GetFavouriteBooksByIdHandler(_context);
        _removeFavouriteBookCommandHandler = new RemoveFavouriteBookCommandHandler(_context);
    }

    [HttpGet]
    public async Task<IActionResult> GetBooksByUser([FromQuery] string userId)
    {
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest("UserId is required");
        }

        var query = new GetFavouriteBooksById(userId);
        var books = await _getFavouriteBooksByIdHandler.Handle(query);
        return Ok(books);
    }
    

    [HttpPost()]
    public async Task<IActionResult> AddBook(AddFavouriteBookCommand command)
    {
        var result = await _addFavouriteBookCommandHandler.Handle(command);
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> RemoveBook(int id)
    {
        var command = new RemoveFavouriteBookCommand(id);
        await _removeFavouriteBookCommandHandler.Handle(command);
        return NoContent();
    }
}