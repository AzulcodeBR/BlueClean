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
        /// ⚠️ **Este endpoint requer autenticação via Bearer Token.**
        /// 
        /// ### Como autenticar:
        /// 
        /// 1. Execute o endpoint POST /api/Login/Autenticar para obter o token
        /// 2. Copie o valor do campo "token" da resposta
        /// 3. No Scalar, clique no botão de autenticação (cadeado) no topo da página
        /// 4. Cole o token no campo "Token" e salve
        /// 5. Agora você pode executar este endpoint
        /// 
        /// **Ou adicione manualmente o header:**
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
