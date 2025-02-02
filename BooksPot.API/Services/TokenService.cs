using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BooksPot.API.Context;
using BooksPot.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BooksPot.API.Services;

public class TokenService : ITokenService
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    public TokenService(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<string> GenerateToken(ApplicationUser user)
    {
        var jwtSettings = _configuration.GetSection("Jwt");
        var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

        // Fetch the user's role asynchronously
        // var userRole = await _context.ApplicationUserRoles
        //     .FirstOrDefaultAsync(ur => ur.UserId == user.Id);

        // var claims = new List<Claim>
        // {
        //     new Claim(ClaimTypes.NameIdentifier, user.Id),
        //     new Claim(ClaimTypes.Email, user.Email)
        // };
        //
        // if (userRole != null)
        // {
        //     claims.Add(new Claim(ClaimTypes.Role, userRole.RoleId));
        // }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            // Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = jwtSettings["Issuer"],
            Audience = jwtSettings["Audience"]
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public async Task<bool> ValidateToken(string token)
    {
        var jwtSettings = _configuration.GetSection("Jwt");
        var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            // Validate the token
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidateAudience = true,
                ValidAudience = jwtSettings["Audience"],
                ValidateLifetime = true, // Check if the token is not expired
                ClockSkew = TimeSpan.Zero // No tolerance for expiration time
            }, out SecurityToken validatedToken);

            // If validation passes, the token is valid
            return true;
        }
        catch
        {
            // If validation fails, the token is invalid
            return false;
        }
    }
}