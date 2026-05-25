using BlueCleanProxyApi.Dtos;

namespace BlueCleanProxyApi.Domains.Interfaces;

public interface ICepService
{
  Task<CepResponseDto?> ConsultarCepAsync(string cep);
}
