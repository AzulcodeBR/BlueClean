using BlueCleanApi.Domains.Dtos.Login;
using BlueCleanApi.Domains.Interfaces;
using BlueCleanApi.Extensions;
using BlueCleanApi.Extensions.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlueCleanApi.Controllers
{
    /// <summary>
    /// Controller responsável pela autenticação de usuários
    /// </summary>
    public class LoginController : BaseController
    {
        private readonly ILoginService _loginService;

        public LoginController(
            INotificadorDominio notificadorDominio,
            ILoginService loginService) : base(notificadorDominio)
        {
            _loginService = loginService;
        }

        /// <summary>
        /// Autentica um usuário no sistema e retorna um token JWT
        /// </summary>
        /// <param name="request">Dados de login contendo email e senha</param>
        /// <returns>Token JWT e informações do usuário autenticado</returns>
        /// <response code="200">Autenticação realizada com sucesso</response>
        /// <response code="401">Credenciais inválidas ou usuário não autorizado</response>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///     POST /api/Login/Autenticar
        ///     {
        ///        "email": "usuario@blueclean.com",
        ///        "senha": "senhaSegura123"
        ///     }
        /// 
        /// A senha deve conter no mínimo 10 caracteres.
        /// </remarks>
        [HttpPost("Autenticar")]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Autenticar([FromBody] LoginRequestDto request)
        {
            var retorno = await _loginService.AutenticarAsync(request.Email, request.Senha);

            if (retorno == null || !_notificadorDominio.VerificarOperacao())
            {
                return UnauthorizedResponse();
            }

            return Ok(retorno);
        }
    }
}
