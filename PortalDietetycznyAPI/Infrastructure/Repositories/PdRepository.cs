using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PortalDietetycznyAPI.Domain.Interfaces;
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

    public Task<bool> AnyAsync<T>() where T : class
    {
        return _db.Set<T>().AnyAsync();
    }

    public async Task AddAsync<T>(T entity) where T : class
    {
        _db.Set<T>().Add(entity);
        await _db.SaveChangesAsync();
    }
}