using Challenge.Core.Contracts.Repository;

namespace Challenge.Core.Contracts.Repository;

public interface IRepositoryManager
{
    IFactsRepository factsRepository { get; }
    IBreedsRepository breedsRepository { get; }
    Task SaveAsync();

}