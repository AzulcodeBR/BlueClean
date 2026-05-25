using BlueCleanApi.Extensions.Interfaces;
using BlueCleanApi.Resources;
using Microsoft.AspNetCore.Mvc;

namespace BlueCleanApi.Extensions
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class BaseController(INotificadorDominio notificadorDominio) : ControllerBase
    {
        protected readonly INotificadorDominio _notificadorDominio = notificadorDominio;

        protected BadRequestObjectResult BadRequestResponse() =>
          BadRequest(_notificadorDominio.ObterNotificacoes().Distinct());

        protected UnauthorizedObjectResult UnauthorizedResponse() =>
          Unauthorized(_notificadorDominio.ObterNotificacoes().Distinct());

        protected NotFoundObjectResult NotFoundRequestResponse() =>
          NotFound(StringResources.NenhumRegistroEncontrado);
    }
}
