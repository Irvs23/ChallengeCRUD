using AutoMapper;
using Challenge.Core.Contracts;
using Challenge.Core.Contracts.Repository;
using Challenge.Services.Contracts;
using Challenge.Services.Implementation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Challenge.Services.Implementation;

public sealed class ServiceManager : IServiceManager
{
    private readonly Lazy<IBreedsService> _breedsService;
    private readonly Lazy<IFactsService> _factsService;
    private readonly Lazy<IIdentityService> _identityService;



    public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper, IConfiguration configuration, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _breedsService = new Lazy<IBreedsService>(() => new BreedsService(repositoryManager, logger, mapper));
        _factsService = new Lazy<IFactsService>(() => new FactsService(repositoryManager, logger, mapper));
        _identityService = new Lazy<IIdentityService>(() => new IdentityService(repositoryManager, logger, mapper, userManager, signInManager, configuration));
    }

    public IBreedsService breedsService => _breedsService.Value;
    public IFactsService factsService => _factsService.Value;
    public IIdentityService identityService => _identityService.Value;


}
