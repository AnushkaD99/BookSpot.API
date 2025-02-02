using BooksPot.API.DTOs;
using BooksPot.API.Models;
using BooksPot.API.Services;
using Microsoft.AspNetCore.Identity;

namespace BooksPot.API.Handlers.Commands;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password);

public class RegisterCommandHandler
{
    private readonly UserManager<ApplicationUser> _userManager;

    public RegisterCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<RegisterResponseDto> Handle(RegisterCommand command)
    {
        // Create a new ApplicationUser object
        var user = new ApplicationUser
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            UserName = command.Email // Username is typically the same as email
        };

        // Create the user with the provided password
        var result = await _userManager.CreateAsync(user, command.Password);

        if (!result.Succeeded)
        {
            // Handle errors (e.g., password requirements not met, email already exists, etc.)
            throw new ApplicationException("User creation failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        return new RegisterResponseDto(user.Id, user.UserName, user.Email);
    }
}