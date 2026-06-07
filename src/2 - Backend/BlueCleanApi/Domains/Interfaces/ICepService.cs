using BlueCleanApi.Domains.Dtos.Endereco;

namespace BlueCleanApi.Domains.Interfaces
{
    /// <summary>
    /// Interface para serviços relacionados a endereço
    /// </summary>
    public interface ICepService
    {
        /// <summary>
        /// Consulta um CEP na API ViaCEP
        /// </summary>
        /// <param name="cep">CEP a ser consultado (com ou sem formatação)</param>
        /// <returns>Dados do endereço ou null se não encontrado</returns>
        Task<CepResponseDto?> ConsultarCepAsync(string cep);
    }
}
