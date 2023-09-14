using System.Linq.Expressions;

namespace Challenge.Core.Contracts.Repository;

public interface IRepositoryBase<T>
{
    IQueryable<T> FindAll(bool trackChanges);
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
    Task<T> FindById(int id, bool trackChanges);
    Task<T> FindById(Int64 id, bool trackChanges);
    Task<T> FindById(string id, bool trackChanges);
    Task<T> FindById(Guid id, bool trackChanges);
    void Create(T entity);
    void CreateRange(IEnumerable<T> entity);
    void Update(T entity);
    void UpdateRange(IEnumerable<T> entity);
    void Delete(T entity);
    void DeleteRange(IEnumerable<T> entity);
}
