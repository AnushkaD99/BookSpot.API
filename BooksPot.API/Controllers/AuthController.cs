using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BooksPot.API.Services;
using BooksPot.API.Handlers.Commands;
using BooksPot.API.Models;

namespace BooksPot.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly RegisterCommandHandler _registerCommandHandler;
    private readonly LoginCommandHandler _loginHandler;

    public AuthController(
        UserManager<ApplicationUser> userManager, 
        ITokenService tokenService,
        SignInManager<ApplicationUser> signInManager)
    {
        _registerCommandHandler = new RegisterCommandHandler(userManager);
        _loginHandler = new LoginCommandHandler(userManager, tokenService, signInManager);
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { Errors = ModelState.Values
                .SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

        try
        {
            var result = await _registerCommandHandler.Handle(command);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                Error = $"An error occurred during registration. {ex.Message}"
            });
        }
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

        try
        {
            var result = await _loginHandler.Handle(command);
            return Ok(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { Error = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                Error = $"An error occurred during login. {ex.Message}"
            });
        }
    }
}