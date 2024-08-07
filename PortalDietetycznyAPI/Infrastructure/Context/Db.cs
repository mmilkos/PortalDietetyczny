using Microsoft.EntityFrameworkCore;
using PortalDietetycznyAPI.Domain.Entities;

namespace PortalDietetycznyAPI.Infrastructure.Context;

public class Db : DbContext
{
    public Db(DbContextOptions<Db> options) : base(options) { }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Recipe>().OwnsOne(recipe => recipe.Nutrition);
        
        modelBuilder.Entity<Recipe>()
            .HasOne(recipe => recipe.Photo)
            .WithOne(photo => photo.Recipe)
            .HasForeignKey<Photo>(b => b.RecipeId);
        
        base.OnModelCreating(modelBuilder);
    }
}