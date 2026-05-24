using System.Net;
using System.Net.Http.Json;
using BlueCleanProxyApi.Domains.Interfaces;
using BlueCleanProxyApi.Extensions.Dtos;
using BlueCleanProxyApi.Extensions.Interfaces;
using BlueCleanProxyApi.Resources;
using BlueCleanProxyApi.Utils;

namespace BlueCleanProxyApi.Domains.Services;

public class ClienteService(
  INotificadorDominio notificadorDominio,
  IHttpClientFactory httpClientFactory) : IClienteService
{
  private readonly INotificadorDominio _notificadorDominio = notificadorDominio;
  private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

  public async Task<ClienteCadastroResponseDto?> CadastrarAsync(ClienteCadastroRequestDto request)
  {
    if (!ClienteCadastroValidacao.Validar(request, _notificadorDominio))
      return null;

    try
    {
      var httpClient = _httpClientFactory.CreateClient("BlueCleanApi");
      var response = await httpClient.PostAsJsonAsync("/api/Cliente/Cadastrar", request);

      if (response.IsSuccessStatusCode)
        return await response.Content.ReadFromJsonAsync<ClienteCadastroResponseDto>();

      if (response.StatusCode == HttpStatusCode.BadRequest)
      {
        var erros = await response.Content.ReadFromJsonAsync<List<string>>();

        if (erros is { Count: > 0 })
          _notificadorDominio.AdicionarNotificacoes(erros);
        else
          _notificadorDominio.AdicionarNotificacao(StringResources.ClienteErroComunicacaoBackend);

        return null;
      }

      _notificadorDominio.AdicionarNotificacao(StringResources.ClienteErroComunicacaoBackend);
      return null;
    }
    catch
    {
      _notificadorDominio.AdicionarNotificacao(StringResources.ClienteErroComunicacaoBackend);
      return null;
    }
  }
}
