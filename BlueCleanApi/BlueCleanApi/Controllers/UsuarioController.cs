using BlueCleanApi.Domains.Interfaces;
using BlueCleanApi.Extensions;
using BlueCleanApi.Extensions.Dtos;
using BlueCleanApi.Extensions.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlueCleanApi.Controllers
{
    /// <summary>
    /// Controller responsável por operações relacionadas ao usuário autenticado
    /// </summary>
    public class UsuarioController : BaseController
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(
            INotificadorDominio notificadorDominio,
            IUsuarioService usuarioService) : base(notificadorDominio)
        {
            _usuarioService = usuarioService;
        }

        /// <summary>
        /// Obtém os dados do usuário logado através do token JWT
        /// </summary>
        /// <returns>Informações do usuário autenticado</returns>
        /// <response code="200">Dados do usuário retornados com sucesso</response>
        /// <response code="401">Token inválido, ausente ou usuário não autenticado</response>
        /// <remarks>
        /// Este endpoint requer autenticação via Bearer Token.
        /// 
        /// Exemplo de uso:
        /// 
        ///     GET /api/Usuario/ObterUsuarioLogado
        ///     Authorization: Bearer {seu_token_jwt}
        /// 
        /// </remarks>
        [Authorize]
        [HttpGet("ObterUsuarioLogado")]
        [ProducesResponseType(typeof(UsuarioLogadoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status401Unauthorized)]
        public IActionResult ObterUsuarioLogado()
        {
            var retorno = _usuarioService.ObterUsuarioLogado();

            if (retorno == null || !_notificadorDominio.VerificarOperacao())
            {
                return UnauthorizedResponse();
            }

            return Ok(retorno);
        }
    }
}
