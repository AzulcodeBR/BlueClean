using BlueCleanApi.Domains.Interfaces;
using BlueCleanApi.Extensions.Dtos;
using BlueCleanApi.Extensions.Interfaces;
using BlueCleanApi.Resources;
using System.Security.Claims;

namespace BlueCleanApi.Domains.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly INotificadorDominio _notificadorDominio;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsuarioService(
            INotificadorDominio notificadorDominio,
            IHttpContextAccessor httpContextAccessor)
        {
            _notificadorDominio = notificadorDominio;
            _httpContextAccessor = httpContextAccessor;
        }

        public UsuarioLogadoDto? ObterUsuarioLogado()
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (user?.Identity?.IsAuthenticated != true)
            {
                _notificadorDominio.AdicionarNotificacao(StringResources.UsuarioNaoAutenticado);
                return null;
            }

            var email = user.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(email))
            {
                _notificadorDominio.AdicionarNotificacao(StringResources.EmailUsuarioNaoEncontradoToken);
                return null;
            }

            return new UsuarioLogadoDto
            {
                Email = email
            };
        }
    }
}
