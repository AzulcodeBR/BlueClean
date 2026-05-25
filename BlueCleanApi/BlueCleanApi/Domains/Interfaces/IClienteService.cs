using BlueCleanApi.Domains.Dtos.Cliente;

namespace BlueCleanApi.Domains.Interfaces;

public interface IClienteService
{
  Task<ClienteCadastroResponseDto?> CadastrarAsync(ClienteCadastroRequestDto request);
}
