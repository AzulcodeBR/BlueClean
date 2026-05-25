using BlueCleanProxyApi.Domains.Interfaces;
using BlueCleanProxyApi.Dtos;
using BlueCleanProxyApi.Extensions.Interfaces;
using BlueCleanProxyApi.Resources;

namespace BlueCleanProxyApi.Domains.Services;

public class LoginService(
  INotificadorDominio notificadorDominio,
  IHttpClientFactory httpClientFactory) : ILoginService
{
    private readonly INotificadorDominio _notificadorDominio = notificadorDominio;
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

    public async Task<LoginResponseDto?> AutenticarAsync(string email, string senha)
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient("BlueCleanApi");

            var request = new { Email = email, Senha = senha };
            var response = await httpClient.PostAsJsonAsync("/api/Login/Autenticar", request);

            if (response.IsSuccessStatusCode)
            {
                var retorno = await response.Content.ReadFromJsonAsync<LoginResponseDto>();
                return retorno;
            }
            else
            {
                var listaErros = await response.Content.ReadFromJsonAsync<List<string>>();

                if (listaErros is { Count: > 0 })
                    _notificadorDominio.AdicionarNotificacoes(listaErros);
                else
                    _notificadorDominio.AdicionarNotificacao(StringResources.LoginErroComunicacaoBackend);

                return null;
            }
        }
        catch
        {
            _notificadorDominio.AdicionarNotificacao(StringResources.LoginErroComunicacaoBackend);
            return null;
        }
    }
}
