using BlueCleanProxyApi.Extensions.Dtos;

namespace BlueCleanProxyApi.Domains.Interfaces;

public interface IClienteService
{
  Task<ClienteCadastroResponseDto?> CadastrarAsync(ClienteCadastroRequestDto request);
}
