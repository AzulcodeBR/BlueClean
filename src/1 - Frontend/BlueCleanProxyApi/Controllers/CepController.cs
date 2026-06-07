using BlueCleanProxyApi.Domains.Interfaces;
using BlueCleanProxyApi.Dtos;
using BlueCleanProxyApi.Extensions;
using BlueCleanProxyApi.Extensions.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlueCleanProxyApi.Controllers;

public class CepController(
  INotificadorDominio notificadorDominio,
  ICepService cepService) : BaseController(notificadorDominio)
{
    private readonly ICepService _cepService = cepService;

    [HttpGet("{cep}")]
    [ProducesResponseType(typeof(CepResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ConsultarCep(string cep)
    {
        var retorno = await _cepService.ConsultarCepAsync(cep);

        if (retorno == null || !_notificadorDominio.VerificarOperacao())
            return BadRequestResponse();

        return Ok(retorno);
    }
}
