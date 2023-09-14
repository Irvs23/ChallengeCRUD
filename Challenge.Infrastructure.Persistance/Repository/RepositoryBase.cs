using Microsoft.EntityFrameworkCore;
using Challenge.Core.Contracts.Repository;
using Challenge.Infrastructure.Persistance.DBContext;

namespace Challenge.Infrastructure.Persistance.Repository;

internal class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected RepositoryDbContext RepositoryContext { get; set; }
    public RepositoryBase(RepositoryDbContext repositoryContext) => RepositoryContext = repositoryContext;
    public void Create(T entity) => RepositoryContext.Set<T>().Add(entity);

    public void CreateRange(IEnumerable<T> entity) => RepositoryContext.Set<T>().AddRange(entity);

    public void Delete(T entity) => RepositoryContext.Set<T>().Remove(entity);

    public void DeleteRange(IEnumerable<T> entity) => RepositoryContext.Set<T>().RemoveRange(entity);

    public IQueryable<T> FindAll(bool trackChanges) =>
    !trackChanges ?
      RepositoryContext.Set<T>()
        .AsNoTracking() :
      RepositoryContext.Set<T>();

    public IQueryable<T> FindByCondition(System.Linq.Expressions.Expression<Func<T, bool>> expression, bool trackChanges) =>
        !trackChanges ?
          RepositoryContext.Set<T>()
            .Where(expression)
            .AsNoTracking() :
          RepositoryContext.Set<T>()
            .Where(expression);

    public async Task<T> FindById(int id, bool trackChanges)
    {
        var entity = await RepositoryContext.Set<T>().FindAsync(id);
        if (!trackChanges)
        {
            RepositoryContext.Entry(entity).State = EntityState.Detached;
        }
        return entity;
    }


    public async Task<T> FindById(long id, bool trackChanges)
    {
        var entity = await RepositoryContext.Set<T>().FindAsync(id);
        if (!trackChanges)
        {
            RepositoryContext.Entry(entity).State = EntityState.Detached;
        }
        return entity;
    }

    public async Task<T> FindById(string id, bool trackChanges)
    {
        var entity = await RepositoryContext.Set<T>().FindAsync(id);
        if (!trackChanges)
        {
            RepositoryContext.Entry(entity).State = EntityState.Detached;
        }
        return entity;
    }

    public async Task<T> FindById(Guid id, bool trackChanges)
    {
        var entity = await RepositoryContext.Set<T>().FindAsync(id);
        if (!trackChanges)
        {
            RepositoryContext.Entry(entity).State = EntityState.Detached;
        }
        return entity;
    }

    public void Update(T entity) => RepositoryContext.Set<T>().Update(entity);

    public void UpdateRange(IEnumerable<T> entity) => RepositoryContext.Set<T>().UpdateRange(entity);
}
