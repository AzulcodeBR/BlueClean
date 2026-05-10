using BlueCleanApi.Domains.Dtos.Endereco;
using BlueCleanApi.Domains.Interfaces;
using BlueCleanApi.Extensions.Interfaces;
using BlueCleanApi.Resources;
using BlueCleanApi.Utils;
using RestSharp;

namespace BlueCleanApi.Domains.Services
{
    /// <summary>
    /// Serviço para operações relacionadas a endereço
    /// </summary>
    public class CepService(INotificadorDominio notificadorDominio) : ICepService
    {
        private readonly INotificadorDominio _notificadorDominio = notificadorDominio;
        private readonly RestClient _restClient = new("https://viacep.com.br");

        /// <summary>
        /// Consulta um CEP na API ViaCEP
        /// </summary>
        /// <param name="cep">CEP a ser consultado (com ou sem formatação)</param>
        /// <returns>Dados do endereço ou null se não encontrado</returns>
        public async Task<CepResponseDto?> ConsultarCepAsync(string cep)
        {
            if (string.IsNullOrWhiteSpace(cep))
            {
                _notificadorDominio.AdicionarNotificacao(StringResources.CepObrigatorio);
                return null;
            }

            var cepLimpo = Funcoes.RemoverMascara(cep);

            if (cepLimpo.Length != 8)
            {
                _notificadorDominio.AdicionarNotificacao(StringResources.CepDeveConterOitoDigitos);
                return null;
            }

            if (!cepLimpo.All(char.IsDigit))
            {
                _notificadorDominio.AdicionarNotificacao(StringResources.CepDeveConterApenasNumeros);
                return null;
            }

            try
            {
                var request = new RestRequest($"/ws/{cepLimpo}/json/", Method.Get);

                var response = await _restClient.ExecuteAsync<CepResponseDto>(request);

                if (!response.IsSuccessful)
                {
                    _notificadorDominio.AdicionarNotificacao(StringResources.ErroConsultarCepApi);
                    return null;
                }

                var retorno = response.Data;

                if (retorno?.Erro == true)
                {
                    _notificadorDominio.AdicionarNotificacao(StringResources.CepNaoEncontrado);
                    return null;
                }

                if (retorno != null)
                {
                    retorno.Logradouro = retorno.Logradouro?.ToUpper() ?? string.Empty;
                    retorno.Complemento = retorno.Complemento?.ToUpper() ?? string.Empty;
                    retorno.Unidade = retorno.Unidade?.ToUpper() ?? string.Empty;
                    retorno.Bairro = retorno.Bairro?.ToUpper() ?? string.Empty;
                    retorno.Localidade = retorno.Localidade?.ToUpper() ?? string.Empty;
                    retorno.Uf = retorno.Uf?.ToUpper() ?? string.Empty;
                }

                return retorno;
            }
            catch (Exception ex)
            {
                _notificadorDominio.AdicionarNotificacao($"{StringResources.ErroConsultarCepApi}: {ex.Message}");
                return null;
            }
        }
    }
}
