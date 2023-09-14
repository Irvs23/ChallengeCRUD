using Challenge.Core.Contracts.Repository;
using Challenge.Core.Domain.Entities;
using Challenge.Infrastructure.Persistance.DBContext;
using Challenge.Infrastructure.Persistance.Repository;

namespace PyMEFlow.Infrastructure.Persistance.Repository.Administracion;

internal class FactsRepository : RepositoryBase<Facts>, IFactsRepository
{
    public FactsRepository(RepositoryDbContext repositoryContext) : base(repositoryContext)
    {
    }
}