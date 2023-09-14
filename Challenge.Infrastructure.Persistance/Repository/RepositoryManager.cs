using Challenge.Core.Contracts.Repository;
using Challenge.Core.Domain.Entities;
using Challenge.Infrastructure.Persistance.DBContext;
using Challenge.Infrastructure.Persistance.Repository;
using PyMEFlow.Infrastructure.Persistance.Repository.Administracion;

namespace Challenge.Infrastructure.Persistance.Repository;

public class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryDbContext _repositoryContext;
    private Lazy<IBreedsRepository> _breeds;
    private Lazy<IFactsRepository> _facts;

    public RepositoryManager(RepositoryDbContext dbContext)
    {
        _repositoryContext = dbContext;

        _breeds = new Lazy<IBreedsRepository>(() => new BreedsRepository(dbContext));
        _facts = new Lazy<IFactsRepository>(() => new FactsRepository(dbContext));
    }
   
    public IBreedsRepository breedsRepository => _breeds.Value;
    public IFactsRepository factsRepository => _facts.Value;
    public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
}
