using BlueCleanApi.Extensions.Dtos;

namespace BlueCleanApi.Domains.Interfaces
{
    public interface ILoginService
    {
        Task<LoginResponseDto?> AutenticarAsync(string email, string senha);
    }
}
