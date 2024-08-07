using System.Linq.Expressions;

namespace PortalDietetycznyAPI.Domain.Interfaces;

public interface IPDRepository
{
    Task<T?> FindEntityByConditionAsync<T>(Expression<Func<T, bool>> condition,
        params Expression<Func<T, object>>[] include) where T : class;

    Task<bool> AnyAsync<T>() where T : class;
    Task AddAsync<T>(T entity) where T : class;
}