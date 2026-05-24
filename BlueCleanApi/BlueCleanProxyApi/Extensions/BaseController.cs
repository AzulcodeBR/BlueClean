using BlueCleanProxyApi.Extensions.Interfaces;
using BlueCleanProxyApi.Resources;
using Microsoft.AspNetCore.Mvc;

namespace BlueCleanProxyApi.Extensions;

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
