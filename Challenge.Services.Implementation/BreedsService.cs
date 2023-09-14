using AutoMapper;
using Challenge.Core.Contracts;
using Challenge.Core.Contracts.Repository;
using Challenge.Core.Domain.Entities;
using Challenge.Services.Contracts;

namespace Challenge.Services.Implementation;

internal class BreedsService : ServiceBase, IBreedsService
{
    public BreedsService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper) : base(repository, logger, mapper)
    {
    }

    public async Task<Breeds> Create(Breeds newObj)
    {
        _repository.breedsRepository.Create(newObj);
        await _repository.SaveAsync();
        return newObj;
    }

    public async Task Delete(string id)
    {
        _repository.breedsRepository.Delete(await _repository.breedsRepository.FindById(id, false));
        await _repository.SaveAsync();
    }

    public IQueryable<Breeds> Find(bool trackChanges) => _repository.breedsRepository.FindAll(trackChanges);

    public async Task<Breeds> FindByID(string id, bool trackChanges) => await _repository.breedsRepository.FindById(id, trackChanges);

    public async Task Update(Breeds objUpd)
    {
        _repository.breedsRepository.Update(objUpd);
        await _repository.SaveAsync();
    }
}