using BooksPot.API.DTOs;
using BooksPot.API.Models;
using BooksPot.API.Services;
using Microsoft.AspNetCore.Identity;

namespace BooksPot.API.Handlers.Commands;

public record LoginCommand(
    string Username,
    string Password);

public class LoginCommandHandler
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public LoginCommandHandler(
        UserManager<ApplicationUser> userManager, 
        ITokenService tokenService,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
    }

    public async Task<LoginResponseDto> Handle(LoginCommand loginCommand)
    {
        if (loginCommand == null)
            throw new ArgumentNullException(nameof(loginCommand));
            
        if (string.IsNullOrWhiteSpace(loginCommand.Username) || string.IsNullOrWhiteSpace(loginCommand.Password))
            throw new ArgumentException("Username and password are required.");

        var user = await _userManager.FindByNameAsync(loginCommand.Username);
        if (user == null)
            throw new UnauthorizedAccessException("Invalid username or password");

        var signInResult = await _signInManager.CheckPasswordSignInAsync(user, loginCommand.Password, true);
        
        if (signInResult.IsLockedOut)
            throw new UnauthorizedAccessException("Account is locked out. Please try again later.");
            
        if (signInResult.RequiresTwoFactor)
            throw new UnauthorizedAccessException("Two-factor authentication is required.");
            
        if (!signInResult.Succeeded)
            throw new UnauthorizedAccessException("Invalid username or password");

        var token = await _tokenService.GenerateToken(user);
        return new LoginResponseDto(user.UserName, token);
    }
}