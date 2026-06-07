using BlueCleanProxyApi.Domains.Interfaces;
using BlueCleanProxyApi.Dtos;
using BlueCleanProxyApi.Extensions.Interfaces;
using BlueCleanProxyApi.Resources;

namespace BlueCleanProxyApi.Domains.Services;

public class CepService(
  INotificadorDominio notificadorDominio,
  IHttpClientFactory httpClientFactory) : ICepService
{
    private readonly INotificadorDominio _notificadorDominio = notificadorDominio;
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

    public async Task<CepResponseDto?> ConsultarCepAsync(string cep)
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient("BlueCleanApi");

            var response = await httpClient.GetAsync($"/api/Cep/{cep}");

            if (response.IsSuccessStatusCode)
            {
                var retorno = await response.Content.ReadFromJsonAsync<CepResponseDto>();
                return retorno;
            }
            else
            {
                var listaErros = await response.Content.ReadFromJsonAsync<List<string>>();

                if (listaErros is { Count: > 0 })
                    _notificadorDominio.AdicionarNotificacoes(listaErros);
                else
                    _notificadorDominio.AdicionarNotificacao(StringResources.CepErroComunicacaoBackend);

                return null;
            }
        }
        catch
        {
            _notificadorDominio.AdicionarNotificacao(StringResources.CepErroComunicacaoBackend);
            return null;
        }
    }
}
