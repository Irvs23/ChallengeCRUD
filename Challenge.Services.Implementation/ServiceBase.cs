using AutoMapper;
using Challenge.Core.Contracts;
using Challenge.Core.Contracts.Repository;

namespace Challenge.Services.Implementation;

public class ServiceBase
{
    protected readonly IRepositoryManager _repository;
    protected readonly ILoggerManager _logger;
    protected readonly IMapper _mapper;

    public ServiceBase(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }
}
