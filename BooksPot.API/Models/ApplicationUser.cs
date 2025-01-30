using Microsoft.AspNetCore.Identity;

namespace BooksPot.API.Models;

public class ApplicationUser : IdentityUser
{
    public string? FullName { get; set; }
}