using BlueCleanProxyApi.Domains.Interfaces;
using BlueCleanProxyApi.Dtos;
using BlueCleanProxyApi.Extensions.Interfaces;
using BlueCleanProxyApi.Resources;

namespace BlueCleanProxyApi.Domains.Services;

public class ClienteService(
  INotificadorDominio notificadorDominio,
  IHttpClientFactory httpClientFactory) : IClienteService
{
    private readonly INotificadorDominio _notificadorDominio = notificadorDominio;
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

    public async Task<ClienteCadastroResponseDto?> CadastrarAsync(ClienteCadastroRequestDto request)
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient("BlueCleanApi");

            var response = await httpClient.PostAsJsonAsync("/api/Cliente/Cadastrar", request);

            if (response.IsSuccessStatusCode)
            {
                var retorno = await response.Content.ReadFromJsonAsync<ClienteCadastroResponseDto>();
                return retorno;
            }
            else
            {
                var listaErros = await response.Content.ReadFromJsonAsync<List<string>>();

                if (listaErros is { Count: > 0 })
                    _notificadorDominio.AdicionarNotificacoes(listaErros);
                else
                    _notificadorDominio.AdicionarNotificacao(StringResources.ClienteErroComunicacaoBackend);

                return null;
            }
        }
        catch
        {
            _notificadorDominio.AdicionarNotificacao(StringResources.ClienteErroComunicacaoBackend);
            return null;
        }
    }
}
