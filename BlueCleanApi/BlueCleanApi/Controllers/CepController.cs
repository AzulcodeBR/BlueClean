using BlueCleanApi.Domains.Dtos.Endereco;
using BlueCleanApi.Domains.Interfaces;
using BlueCleanApi.Extensions;
using BlueCleanApi.Extensions.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlueCleanApi.Controllers
{
    /// <summary>
    /// Controller para operações relacionadas a CEP
    /// </summary>
    [ApiController]
    public class CepController : BaseController
    {
        private readonly ICepService _cepService;

        public CepController(
            INotificadorDominio notificadorDominio,
            ICepService cepService) : base(notificadorDominio)
        {
            _cepService = cepService;
        }

        /// <summary>
        /// Consulta um CEP na API ViaCEP
        /// </summary>
        /// <param name="cep">CEP a ser consultado (com ou sem formatação)</param>
        /// <returns>Dados do endereço</returns>
        /// <response code="200">Retorna os dados do endereço</response>
        /// <response code="400">CEP inválido ou não encontrado</response>
        [Authorize]
        [HttpGet("{cep}")]
        [ProducesResponseType(typeof(CepResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ConsultarCep(string cep)
        {
            var retorno = await _cepService.ConsultarCepAsync(cep);

            if (retorno == null)
                return BadRequestResponse();

            return Ok(retorno);
        }
    }
}
