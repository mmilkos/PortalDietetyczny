using System.Linq.Expressions;
using PagedList;
using PortalDietetycznyAPI.Domain.Entities;
using PortalDietetycznyAPI.DTOs;

namespace PortalDietetycznyAPI.Domain.Interfaces;

public interface IPDRepository
{
    Task<T?> FindEntityByConditionAsync<T>(Expression<Func<T, bool>> condition,
        params Expression<Func<T, object>>[] include) where T : Entity;
    Task<List<T>> FindEntitiesByConditionAsync<T>(Expression<Func<T, bool>> condition,
        params Expression<Func<T, object>>[] include) where T : Entity;
    List<T> FindEntitiesByCondition<T>(Expression<Func<T, bool>> condition,
        params Expression<Func<T, object>>[] include) where T : Entity;
    Task<bool> AnyUserAsync();
    Task AddAsync<T>(T entity) where T : Entity;
    Task AddRangeAsync<T>(List<T> list) where T : Entity;
    Task UpdateAsync<T>(T entity) where T : Entity;
    Task UpdateRange<T>(List<T> entity) where T : Entity;
    Task<List<T>> GetAllEntitiesAsync<T>(Expression<Func<T, bool>> condition) where T : Entity;
    Task<IPagedList<Recipe>> GetRecipesPagedAsync(RecipesPreviewPageRequest dto);
    Task<Recipe?> GetRecipe(int uid);
    Task<IPagedList<BlogPost>> GetBlogPostsPagedAsync(BlogPostsPreviewPageRequest dto);
    Task DeleteAsync<T>(int id) where T : Entity;
    Task DeleteRangeAsync<T>(List<T> list) where T : Entity;
    Task<IPagedList<Diet>> GetDietsPagedAsync(DietsPreviewPageRequest dto);
    Task<int> CountAsync<T>() where T : Entity;
}