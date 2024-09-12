using Microsoft.EntityFrameworkCore;
using PortalDietetycznyAPI.Domain.Entities;

namespace PortalDietetycznyAPI.Infrastructure.Context;

public class Db : DbContext
{
    public Db(DbContextOptions<Db> options) : base(options) { }
    
    public DbSet<Recipe> Recipes { get; init; }
    public DbSet<Ingredient> Ingredients { get; init; }
    public DbSet<RecipeIngredient> RecipeIngredients { get; init; }
    public DbSet<RecipePhoto> Photos { get; init; }
    
    public DbSet<Tag> Tags { get; init; }
    public DbSet<RecipeTag> RecipeTags { get; init; }
    public DbSet<User> Users { get; init; }
    
    public DbSet<BlogPost> BlogPosts { get; init; }
    public DbSet<BlogPhoto> BlogPhotos { get; init; }
    
    public DbSet<Diet> Diets { get; set; }
    public DbSet<DietPhoto> DietPhotos { get; set; }
    public DbSet<DietTag> DietTags { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Recipe>().OwnsOne(recipe => recipe.Nutrition);
        
        modelBuilder.Entity<Recipe>()
            .HasIndex(r => r.Uid);

        modelBuilder.Entity<Ingredient>()
            .HasIndex(e => e.Name).IsUnique();

        modelBuilder.Entity<Recipe>()
            .HasOne(r => r.Photo)
            .WithOne(p => p.Recipe)
            .HasForeignKey<RecipePhoto>(p => p.RecipeId)
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
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<BlogPost>()
            .HasIndex(bp => bp.Uid);

        modelBuilder.Entity<BlogPost>()
            .HasOne(bp => bp.Photo)
            .WithOne(bPhoto => bPhoto.BlogPost)
            .HasForeignKey<BlogPhoto>(bp => bp.BlogPostId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Diet>()
            .HasOne(d => d.Photo)
            .WithOne(dPhoto => dPhoto.Diet)
            .HasForeignKey<DietPhoto>(dPhoto => dPhoto.DietId)
            .OnDelete(DeleteBehavior.SetNull);
        
        modelBuilder.Entity<DietTag>()
            .HasKey(dt => new { dt.DietId, dt.TagId }); 

        modelBuilder.Entity<DietTag>()
            .HasOne(dt => dt.Tag)
            .WithMany(t => t.DietTags)
            .HasForeignKey(dt => dt.TagId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<DietTag>()
            .HasOne(dt => dt.Diet)
            .WithMany(t => t.DietTags)
            .HasForeignKey(dt => dt.DietId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}