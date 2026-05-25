using BlueCleanProxyApi.Domains.Interfaces;
using BlueCleanProxyApi.Dtos;
using BlueCleanProxyApi.Extensions;
using BlueCleanProxyApi.Extensions.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlueCleanProxyApi.Controllers;

public class LoginController(
  INotificadorDominio notificadorDominio,
  ILoginService loginService) : BaseController(notificadorDominio)
{
    private readonly ILoginService _loginService = loginService;

    [HttpPost("Autenticar")]
    [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Autenticar([FromBody] LoginRequestDto request)
    {
        var retorno = await _loginService.AutenticarAsync(request.Email, request.Senha);

        if (retorno == null || !_notificadorDominio.VerificarOperacao())
            return BadRequestResponse();

        return Ok(retorno);
    }
}
