using BlueCleanProxyApi.Dtos;

namespace BlueCleanProxyApi.Domains.Interfaces;

public interface ILoginService
{
  Task<LoginResponseDto?> AutenticarAsync(string email, string senha);
}
