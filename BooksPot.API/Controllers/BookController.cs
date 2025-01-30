using BooksPot.API.Context;
using Microsoft.AspNetCore.Mvc;

namespace BooksPot.API.Controllers;

[Route("api/book")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public BookController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var books = _context.SavedBooks.ToList();
        return Ok(books);
    }
}