using BooksPot.API.Models;

namespace BooksPot.API.Services;

public interface ITokenService
{
    Task<string> GenerateToken(ApplicationUser user);
    Task<bool> ValidateToken(string token);
}
