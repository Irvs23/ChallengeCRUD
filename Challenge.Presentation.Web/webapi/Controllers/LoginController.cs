
using Challenge.Core.Shared.DataTransferObjects;
using Challenge.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IServiceManager _service;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public LoginController(IServiceManager service, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Authenticate([FromBody] AuthenticateUserDTO user)
    {
        var usuario = await _userManager.FindByNameAsync(user.UserName);
        if (usuario == null)
            return Unauthorized("La combinación de usuario y contraseña ingresados no es correcta");
        var result = await _signInManager.PasswordSignInAsync(usuario, user.Password, user.RememberMe, false);
        var test = usuario.Id;

        if (!await _service.identityService.ValidateUser(user))
            return Ok("La combinación de usuario y contraseña ingresados no es correcta");

        var AuthToken = await _service.identityService.CreateToken();

        return Ok(new { AuthToken, usuario.Id });
    }
}
