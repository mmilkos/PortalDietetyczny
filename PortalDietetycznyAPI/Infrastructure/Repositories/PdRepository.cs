using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PagedList;
using PortalDietetycznyAPI.Domain.Entities;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.DTOs;
using PortalDietetycznyAPI.Infrastructure.Context;

namespace PortalDietetycznyAPI.Infrastructure.Repositories;

public class PdRepository : IPDRepository
{
    private Db _db;
    
    public PdRepository(Db db)
    {
        _db = db;
    }
    public async Task<T?> FindEntityByConditionAsync<T>(Expression<Func<T, bool>> condition,
        params Expression<Func<T, object>>[] include) where T : class
    {
        var query = _db.Set<T>().Where(condition);

        foreach (var includeProperty in include) query = query.Include(includeProperty);
        
        return await query.FirstOrDefaultAsync();
    }

    public async Task<List<T>> FindEntitiesByConditionAsync<T>(Expression<Func<T, bool>> condition,
        params Expression<Func<T, object>>[] include) where T : class
    {
        var query = _db.Set<T>().Where(condition);
        foreach (var includeProperty in include) query = query.Include(includeProperty);

        return await query.ToListAsync();
    }

    public Task<bool> AnyAsync<T>() where T : class
    {
        return _db.Set<T>().AnyAsync();
    }

    public async Task AddAsync<T>(T entity) where T : class
    {
        _db.Set<T>().Add(entity);
        await _db.SaveChangesAsync();
    }

    public async Task AddRangeAsync<T>(List<T> list) where T : class
    {
        _db.Set<T>().AddRange(list);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync<T>(T entity) where T : class
    {
        _db.Set<T>().Update(entity);
        await _db.SaveChangesAsync();
    }

    public async Task<List<T>> GetAllEntitiesAsync<T>(Expression<Func<T, bool>> condition) where T : class
    { 
        return await _db.Set<T>().Where(condition).ToListAsync();
    }
    
    public async Task DeleteAsync<T>(T entity) where T : class
    {
        _db.Set<T>().Remove(entity);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteRangeAsync<T>(List<T> list) where T : class
    {
        _db.Set<T>().RemoveRange(list);
        await _db.SaveChangesAsync();
    }

    public async Task<IPagedList<Recipe>> GetRecipesPagedAsync(RecipesPreviewPageRequest dto)
    {
        var wantedTags = dto.TagsIds;
        var wantedIngredient = dto.IngredientsIds;

        var query = _db.Recipe
            .Include(r => r.Photo)
            .Include(r => r.RecipeTags)
            .Include(r => r.Ingredients)
            .Where(recipe =>
                (wantedTags.Count == 0 || 
                 wantedTags.All(tagId => recipe.RecipeTags.Select(rt => rt.TagId).Contains(tagId)))
                &&
                (wantedIngredient.Count == 0 || 
                 wantedIngredient.All(ingredientId => recipe.Ingredients.Select(i => i.IngredientId).Contains(ingredientId))));
        
        var list = await query.ToPagedListAsync(dto.PageNumber, dto.PageSize);
        
        return list;
    }

    public async Task<Recipe?> GetRecipe(int uid)
    {
        var recipe = await _db.Recipe.Where(r => r.Uid == uid)
            .Include(r => r.RecipeTags)
            .ThenInclude(rt => rt.Tag)
            .Include(r => r.Nutrition)
            .Include(r => r.Ingredients)
            .ThenInclude(i => i.Ingredient)
            .Include(r => r.Photo)
            .FirstOrDefaultAsync();
        
        return recipe;
    }

    public async Task<IPagedList<BlogPost>> GetBlogPostsPagedAsync(BlogPostsPreviewPageRequest dto)
    {
        var query = _db.BlogPosts.Include(bp => bp.Photo);

        var list = await query.ToPagedListAsync(dto.PageNumber, dto.PageSize);

        return list;
    }

    public async Task<IPagedList<Diet>> GetDietsPagedAsync(DietsPreviewPageRequest dto)
    {
        var wantedTags = dto.TagsIds;

        var query = _db.Diets
            .Include(d => d.Photo)
            .Include(d => d.DietTags)
            .Where(diet =>
                (wantedTags.Count == 0 ||
                 wantedTags.All(tagId => diet.DietTags.Select(dt => dt.TagId).Contains(tagId))));
        
        var list = await query.ToPagedListAsync(dto.PageNumber, dto.PageSize);
        
        return list;
    }

   
}