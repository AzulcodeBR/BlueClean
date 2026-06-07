using BlueCleanApi.Extensions.Dtos;

namespace BlueCleanApi.Domains.Interfaces
{
    public interface IUsuarioService
    {
        UsuarioLogadoDto? ObterUsuarioLogado();
    }
}
