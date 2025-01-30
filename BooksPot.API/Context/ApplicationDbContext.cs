using BooksPot.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksPot.API.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<SavedBook> SavedBooks { get; set; }
}