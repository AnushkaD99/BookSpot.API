using BooksPot.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BooksPot.API.EntityConfig;

public class SavedBookEntityConfig : IEntityTypeConfiguration<SavedBook>
{
    public void Configure(EntityTypeBuilder<SavedBook> builder)
    {
        // Create a unique composite index to prevent duplicate savedBooks for a user
        builder.HasIndex(b => new { b.UserId, b.Isbn })
            .IsUnique();

        // Configure the foreign key relationship with the User
        builder.HasOne(b => b.User)
            .WithMany(u => u.SavedBooks)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}