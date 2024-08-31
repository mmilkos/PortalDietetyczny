using Microsoft.EntityFrameworkCore;
using PortalDietetycznyAPI.Domain.Entities;

namespace PortalDietetycznyAPI.Infrastructure.Context;

public class Db : DbContext
{
    public Db(DbContextOptions<Db> options) : base(options) { }
    
    public DbSet<Ingredient> Ingredients { get; init; }
    public DbSet<Photo> Photos { get; init; }
    public DbSet<RecipeIngredient> RecipeIngredients { get; init; }
    public DbSet<Recipe> Recipes { get; init; }
    public DbSet<Tag> Tags { get; init; }
    public DbSet<RecipeTag> RecipeTags { get; init; }
    public DbSet<User> Users { get; init; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Recipe>().OwnsOne(recipe => recipe.Nutrition);
        
        modelBuilder.Entity<Recipe>()
            .HasIndex(p => p.Uid);

        modelBuilder.Entity<Ingredient>().HasIndex(e => e.Name).IsUnique();

        modelBuilder.Entity<Recipe>()
            .HasOne(r => r.Photo)
            .WithOne(p => p.Recipe)
            .HasForeignKey<Photo>(p => p.RecipeId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<RecipeIngredient>()
            .HasOne(ri => ri.Ingredient)
            .WithMany()
            .HasForeignKey(ri => ri.IngredientId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<RecipeIngredient>()
            .HasOne<Recipe>()
            .WithMany(r => r.Ingredients)
            .HasForeignKey(ri => ri.RecipeId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<RecipeTag>()
            .HasKey(rt => new { rt.RecipeId, rt.TagId });
    
        modelBuilder.Entity<RecipeTag>()
            .HasOne(rt => rt.Recipe)
            .WithMany(r => r.RecipeTags)
            .HasForeignKey(rt => rt.RecipeId)
            .OnDelete(DeleteBehavior.Cascade);
    
        modelBuilder.Entity<RecipeTag>()
            .HasOne(rt => rt.Tag)
            .WithMany(t => t.RecipeTags)
            .HasForeignKey(rt => rt.TagId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}