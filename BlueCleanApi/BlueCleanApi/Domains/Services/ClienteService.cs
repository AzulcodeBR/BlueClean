using System.Security.Cryptography;
using System.Text;
using BlueCleanApi.Domains.Interfaces;
using BlueCleanApi.Extensions.Dtos;
using BlueCleanApi.Extensions.Interfaces;
using BlueCleanApi.Models.BlueCleanDb;
using BlueCleanApi.Resources;
using BlueCleanApi.Utils;
using Microsoft.EntityFrameworkCore;

namespace BlueCleanApi.Domains.Services;

public class ClienteService(
  INotificadorDominio notificadorDominio,
  LavanderiaContext context) : IClienteService
{
  private const int StatusClienteAguardandoConfirmacaoEmailId = 4;

  private readonly INotificadorDominio _notificadorDominio = notificadorDominio;
  private readonly LavanderiaContext _context = context;

  public async Task<ClienteCadastroResponseDto?> CadastrarAsync(ClienteCadastroRequestDto request)
  {
    if (!ClienteCadastroValidacao.Validar(request, _notificadorDominio))
      return null;

    var emailNormalizado = request.Email.Trim().ToLowerInvariant();
    var cpfCnpjNormalizado = Funcoes.RemoverMascara(request.CpfCnpj);
    var telefoneNormalizado = string.IsNullOrWhiteSpace(request.Telefone)
      ? null
      : Funcoes.RemoverMascara(request.Telefone);

    var emailExiste = await _context.Cliente
      .AnyAsync(c => c.Email == emailNormalizado);

    if (emailExiste)
    {
      _notificadorDominio.AdicionarNotificacao(StringResources.ClienteEmailJaCadastrado);
      return null;
    }

    var cpfCnpjExiste = await _context.Cliente
      .AnyAsync(c => c.CpfCnpj == cpfCnpjNormalizado);

    if (cpfCnpjExiste)
    {
      _notificadorDominio.AdicionarNotificacao(StringResources.ClienteCpfCnpjJaCadastrado);
      return null;
    }

    var dataServidor = DateTime.Now;

    var cliente = new Cliente
    {
      Nome = request.Nome.Trim(),
      Email = emailNormalizado,
      Telefone = telefoneNormalizado,
      CpfCnpj = cpfCnpjNormalizado,
      Senha = HashSenha(request.Senha),
      StatusClienteId = StatusClienteAguardandoConfirmacaoEmailId,
      DataCadastro = dataServidor,
      DataAtualizacao = dataServidor,
      Observacao = string.IsNullOrWhiteSpace(request.Observacao)
        ? null
        : request.Observacao.Trim()
    };

    await _context.Cliente.AddAsync(cliente);
    await _context.SaveChangesAsync();

    return new ClienteCadastroResponseDto
    {
      ClienteId = cliente.ClienteId
    };
  }

  private static string HashSenha(string senha)
  {
    var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(senha));
    return Convert.ToBase64String(bytes);
  }
}
