using Core.Entities;
using Core.Intrefaces;
using Core.Specifications;
using Infrastructue.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly StoreContext _context;
    private readonly DbSet<T> _entity; 

    public GenericRepository(StoreContext context)
    {
        _context = context;
        _entity = context.Set<T>();
    }
    
    public async Task<T> GetByIdAsync(int id)
    {
        return await _entity.FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> ListAllAsync()
    {
        return await _entity.ToListAsync();
    }

    public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).AsQueryable().FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).ToListAsync();
    }

    private IQueryable<T> ApplySpecification(ISpecification<T> spec)
    {
        return SpecificationEvaluator<T>.GetQuery(_entity.AsQueryable(), spec);
    }
}