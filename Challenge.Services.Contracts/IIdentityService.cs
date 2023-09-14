using Challenge.Core.Shared.DataTransferObjects;


namespace Challenge.Services.Contracts;

public interface IIdentityService
{

    Task LogOut();

    Task<bool> TryLogin(AuthenticateUserDTO user);

    Task<bool> ValidateUser(AuthenticateUserDTO userForAuth);

    Task<string> CreateToken();
}