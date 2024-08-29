using System.Linq.Expressions;
using PagedList;
using PortalDietetycznyAPI.Domain.Entities;
using PortalDietetycznyAPI.DTOs;

namespace PortalDietetycznyAPI.Domain.Interfaces;

public interface IPDRepository
{
    Task<T?> FindEntityByConditionAsync<T>(Expression<Func<T, bool>> condition,
        params Expression<Func<T, object>>[] include) where T : class;

    Task<bool> AnyAsync<T>() where T : class;
    Task AddAsync<T>(T entity) where T : class;
    Task AddRangeAsync<T>(List<T> list) where T : class;
    Task UpdateAsync<T>(T entity) where T : class;
    Task<List<T>> GetAllEntitiesAsync<T>(Expression<Func<T, bool>> condition) where T : class;
    Task<IPagedList<Recipe>> GetRecipesPagedAsync(RecipesPreviewPageRequest dto);
    
}