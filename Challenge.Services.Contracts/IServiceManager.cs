namespace Challenge.Services.Contracts;

public interface IServiceManager
{
   
    IBreedsService breedsService { get; }

    IFactsService factsService { get; }

    IIdentityService identityService { get; }
}
