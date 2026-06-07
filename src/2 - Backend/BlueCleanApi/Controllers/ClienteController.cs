using BlueCleanApi.Domains.Dtos.Cliente;
using BlueCleanApi.Domains.Interfaces;
using BlueCleanApi.Extensions;
using BlueCleanApi.Extensions.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlueCleanApi.Controllers;

/// <summary>
/// Operações de cadastro e gestão de clientes.
/// </summary>
public class ClienteController(
  INotificadorDominio notificadorDominio,
  IClienteService clienteService) : BaseController(notificadorDominio)
{
    private readonly IClienteService _clienteService = clienteService;

    /// <summary>
    /// Cadastra um novo cliente.
    /// </summary>
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
