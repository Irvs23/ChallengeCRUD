using Challenge.Core.Domain.Entities;

namespace Challenge.Services.Contracts;

public interface IBreedsService
{
    Task<Breeds> FindByID(string id, bool trackChanges);
    IQueryable<Breeds> Find(bool trackChanges);
    Task<Breeds> Create(Breeds newObj);
    Task Update(Breeds objUpd);
    Task Delete(string id);
}