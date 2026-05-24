using BlueCleanApi.Extensions.Dtos;

namespace BlueCleanApi.Domains.Interfaces;

public interface IClienteService
{
  Task<ClienteCadastroResponseDto?> CadastrarAsync(ClienteCadastroRequestDto request);
}
