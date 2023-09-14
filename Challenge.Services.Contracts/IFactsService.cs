using Challenge.Core.Domain.Entities;

namespace Challenge.Services.Contracts;

public interface IFactsService
{
    Task<Facts> FindByID(string id, bool trackChanges);
    IQueryable<Facts> Find(bool trackChanges);
    Task<Facts> Create(Facts newObj);
    Task Update(Facts objUpd);
    Task Delete(string id);
}