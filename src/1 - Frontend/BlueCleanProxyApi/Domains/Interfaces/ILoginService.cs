using BlueCleanProxyApi.Dtos;

namespace BlueCleanProxyApi.Domains.Interfaces;

public interface ILoginService
{
  Task<LoginResponseDto?> AutenticarAsync(string identificador, string senha, int tipoLogin);
}
