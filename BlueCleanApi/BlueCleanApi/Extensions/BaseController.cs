using BlueCleanApi.Extensions.Interfaces;
using BlueCleanApi.Resources;
using Microsoft.AspNetCore.Mvc;

namespace BlueCleanApi.Extensions
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class BaseController : ControllerBase
    {
        protected readonly INotificadorDominio _notificadorDominio;

        protected BaseController(INotificadorDominio notificadorDominio)
        {
            _notificadorDominio = notificadorDominio;
        }

        protected BadRequestObjectResult BadRequestResponse()
        {
            return BadRequest(_notificadorDominio.ObterNotificacoes().Distinct());
        }

        protected UnauthorizedObjectResult UnauthorizedResponse()
        {
            return Unauthorized(_notificadorDominio.ObterNotificacoes().Distinct());
        }

        protected NotFoundObjectResult NotFoundRequestResponse()
        {
            return NotFound(StringResources.NenhumRegistroEncontrado);
        }
    }
}
