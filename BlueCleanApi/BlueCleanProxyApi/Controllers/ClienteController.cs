using BlueCleanProxyApi.Domains.Interfaces;
using BlueCleanProxyApi.Extensions;
using BlueCleanProxyApi.Extensions.Dtos;
using BlueCleanProxyApi.Extensions.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlueCleanProxyApi.Controllers;

public class ClienteController(
  INotificadorDominio notificadorDominio,
  IClienteService clienteService) : BaseController(notificadorDominio)
{
  private readonly IClienteService _clienteService = clienteService;

  [HttpPost("Cadastrar")]
  [ProducesResponseType(typeof(ClienteCadastroResponseDto), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> Cadastrar([FromBody] ClienteCadastroRequestDto request)
  {
    var retorno = await _clienteService.CadastrarAsync(request);

    if (retorno == null || !_notificadorDominio.VerificarOperacao())
      return BadRequestResponse();

    return Ok(retorno);
  }
}
