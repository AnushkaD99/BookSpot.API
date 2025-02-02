using BooksPot.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksPot.API.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
    public DbSet<SavedBook> SavedBooks { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    // public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }
}