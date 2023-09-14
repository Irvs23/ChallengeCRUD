using AutoMapper;
using Challenge.Core.Contracts;
using Challenge.Core.Contracts.Repository;
using Challenge.Core.Shared.DataTransferObjects;
using Challenge.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Challenge.Services.Implementation;

internal class IdentityService : ServiceBase, IIdentityService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IConfiguration _configuration;
    private IdentityUser? _user;
    public IdentityService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration) : base(repository, logger, mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    public async Task<bool> TryLogin(AuthenticateUserDTO userDTO)
    {
        var result = await _signInManager.PasswordSignInAsync(userDTO.UserName, userDTO.Password, userDTO.RememberMe, false);
        return result.Succeeded;
    }

    public async Task LogOut()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<bool> ValidateUser(AuthenticateUserDTO userForAuth)
    {
        _user = await _userManager.FindByNameAsync(userForAuth.UserName);
        var result = (_user is not null && await _userManager.CheckPasswordAsync(_user, userForAuth.Password));
        if (!result)
            _logger.LogWarn($"{nameof(ValidateUser)}: Authentication failed. Wrong user name or password.");

        return result;
    }

    public async Task<string> CreateToken()
    {
        var signingCredentials = GetSigningCredentials();
        var claims = await GetClaims();
        var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }

    private SigningCredentials GetSigningCredentials()
    {
        var key = Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value);
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private async Task<List<Claim>> GetClaims()
    {
        var claims = (await _userManager.GetClaimsAsync(_user)).ToList();

        claims.Add(new Claim(ClaimTypes.Name, _user.UserName));

        var roles = await _userManager.GetRolesAsync(_user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        return claims;
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var tokenOptions = new JwtSecurityToken
        (
            issuer: jwtSettings["validIssuer"],
            audience: jwtSettings["validAudience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
            signingCredentials: signingCredentials
        );
        return tokenOptions;
    }

}