using AutoMapper;
using Challenge.Core.Contracts;
using Challenge.Core.Contracts.Repository;
using Challenge.Core.Domain.Entities;
using Challenge.Services.Contracts;

namespace Challenge.Services.Implementation;

internal class FactsService : ServiceBase, IFactsService
{
    public FactsService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper) : base(repository, logger, mapper)
    {
    }

    public async Task<Facts> Create(Facts newObj)
    {
        _repository.factsRepository.Create(newObj);
        await _repository.SaveAsync();
        return newObj;
    }

    public async Task Delete(string id)
    {
        _repository.factsRepository.Delete(await _repository.factsRepository.FindById(id, false));
        await _repository.SaveAsync();
    }

    public IQueryable<Facts> Find(bool trackChanges) => _repository.factsRepository.FindAll(trackChanges);

    public async Task<Facts> FindByID(string id, bool trackChanges) => await _repository.factsRepository.FindById(id, trackChanges);

    public async Task Update(Facts objUpd)
    {
        _repository.factsRepository.Update(objUpd);
        await _repository.SaveAsync();
    }
}