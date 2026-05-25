using BlueCleanApi.Domains.Dtos.Login;

namespace BlueCleanApi.Domains.Interfaces
{
    public interface ILoginService
    {
        Task<LoginResponseDto?> AutenticarAsync(string email, string senha);
    }
}
