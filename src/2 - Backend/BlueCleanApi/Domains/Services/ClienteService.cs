using BlueCleanApi.Domains.Dtos.Cliente;
using BlueCleanApi.Domains.Interfaces;
using BlueCleanApi.Enums;
using BlueCleanApi.Extensions.Interfaces;
using BlueCleanApi.Models.BlueCleanDb;
using BlueCleanApi.Resources;
using BlueCleanApi.Utils;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace BlueCleanApi.Domains.Services;

public partial class ClienteService(
  INotificadorDominio notificadorDominio,
  ILogger<ClienteService> logger,
  LavanderiaContext context) : IClienteService
{
    private readonly INotificadorDominio _notificadorDominio = notificadorDominio;
    private readonly ILogger<ClienteService> _logger = logger;
    private readonly LavanderiaContext _context = context;

    public async Task<ClienteCadastroResponseDto?> CadastrarAsync(ClienteCadastroRequestDto request)
    {
        if (!ValidarCadastroCliente(request))
            return null;

        try
        {
            var email = request.Email.Trim().ToUpperInvariant();
            var cpfCnpj = Funcoes.RemoverMascara(request.CpfCnpj);
            var telefone = Funcoes.RemoverMascara(request.Telefone);

            var emailExiste = await _context.Cliente.AnyAsync(c => c.Email == email);

            if (emailExiste)
            {
                _notificadorDominio.AdicionarNotificacao(StringResources.ClienteEmailJaCadastrado);
                return null;
            }

            var cpfCnpjExiste = await _context.Cliente.AnyAsync(c => c.CpfCnpj == cpfCnpj);

            if (cpfCnpjExiste)
            {
                _notificadorDominio.AdicionarNotificacao(StringResources.ClienteCpfCnpjJaCadastrado);
                return null;
            }

            var novoCliente = new Cliente
            {
                Nome = request.Nome.Trim().ToUpper(),
                Email = email,
                Telefone = telefone,
                CpfCnpj = cpfCnpj,
                Senha = Funcoes.ConvertToSHA256(request.Senha),
                StatusClienteId = EStatusCliente.AGUARDANDO_CONFIRMACAO_EMAIL.GetHashCode(),
                DataCadastro = DateTime.Now,
                DataAtualizacao = DateTime.Now,
            };

            await _context.Cliente.AddAsync(novoCliente);
            await _context.SaveChangesAsync();

            return new ClienteCadastroResponseDto
            {
                ClienteId = novoCliente.ClienteId
            };
        }
        catch (Exception ex)
        {
            _logger.LogError("Erro ao cadastrar cliente. Request: {@Request} - Exception: {Exception}", request, ex);
            _notificadorDominio.AdicionarNotificacao(StringResources.ClienteErroInesperado);
            return null;
        }
    }

    private bool ValidarCadastroCliente(ClienteCadastroRequestDto request)
    {
        if (string.IsNullOrWhiteSpace(request.Nome))
        {
            _notificadorDominio.AdicionarNotificacao(StringResources.ClienteNomeObrigatorio);
        }
        else
        {
            if (request.Nome.Trim().Length > 150)
                _notificadorDominio.AdicionarNotificacao(StringResources.ClienteNomeMaximoCaracteres);

            if (!Funcoes.ValidarNomeCompleto(request.Nome))
                _notificadorDominio.AdicionarNotificacao(StringResources.ClienteNomeDeveConterMaisDeUmNome);
        }

        if (string.IsNullOrWhiteSpace(request.Email))
        {
            _notificadorDominio.AdicionarNotificacao(StringResources.ClienteEmailObrigatorio);
        }
        else
        {
            if (!Funcoes.ValidarEmail(request.Email))
                _notificadorDominio.AdicionarNotificacao(StringResources.ClienteEmailInvalido);

            if (request.Email.Trim().Length > 150)
                _notificadorDominio.AdicionarNotificacao(StringResources.ClienteEmailMaximoCaracteres);
        }

        if (string.IsNullOrWhiteSpace(request.Telefone))
        {
            _notificadorDominio.AdicionarNotificacao(StringResources.ClienteTelefoneObrigatorio);
        }
        else
        {
            var telefone = Funcoes.RemoverMascara(request.Telefone);

            if (telefone.Length > 11)
                _notificadorDominio.AdicionarNotificacao(StringResources.ClienteTelefoneMaximoCaracteres);

            if (!Funcoes.ValidarTelefone(telefone))
                _notificadorDominio.AdicionarNotificacao(StringResources.ClienteTelefoneInvalido);
        }

        if (string.IsNullOrWhiteSpace(request.CpfCnpj))
        {
            _notificadorDominio.AdicionarNotificacao(StringResources.ClienteCpfCnpjObrigatorio);
        }
        else
        {
            var cpfCnpj = Funcoes.RemoverMascara(request.CpfCnpj);

            if (cpfCnpj.Length == 11 && !Funcoes.ValidarCpf(cpfCnpj))
                _notificadorDominio.AdicionarNotificacao(StringResources.ClienteCpfCnpjInvalido);

            if (cpfCnpj.Length == 14 && !Funcoes.ValidarCnpj(cpfCnpj))
                _notificadorDominio.AdicionarNotificacao(StringResources.ClienteCpfCnpjInvalido);

            if (cpfCnpj.Length is not (11 or 14))
                _notificadorDominio.AdicionarNotificacao(StringResources.ClienteCpfCnpjInvalido);
        }

        if (string.IsNullOrWhiteSpace(request.Senha))
        {
            _notificadorDominio.AdicionarNotificacao(StringResources.ClienteSenhaObrigatoria);
        }
        else
        {
            if (request.Senha.Length < 10)
                _notificadorDominio.AdicionarNotificacao(StringResources.SenhaDeveTerMinimoCaracteres);

            if (!PossuiLetras().IsMatch(request.Senha))
                _notificadorDominio.AdicionarNotificacao(StringResources.ClienteSenhaDeveConterLetras);

            if (!PossuiLetraMaiuscula().IsMatch(request.Senha))
                _notificadorDominio.AdicionarNotificacao(StringResources.ClienteSenhaDeveConterLetraMaiuscula);

            if (!PossuiCaracterEspecial().IsMatch(request.Senha))
                _notificadorDominio.AdicionarNotificacao(StringResources.ClienteSenhaDeveConterCaractereEspecial);

            if (Funcoes.ContemNumerosSequenciais(request.Senha))
                _notificadorDominio.AdicionarNotificacao(StringResources.ClienteSenhaNaoPodeConterNumerosSequenciais);
        }

        return _notificadorDominio.VerificarOperacao();
    }

    [GeneratedRegex(@"[^A-Za-z0-9]")]
    private static partial Regex PossuiCaracterEspecial();

    [GeneratedRegex(@"[A-Z]")]
    private static partial Regex PossuiLetraMaiuscula();

    [GeneratedRegex(@"[A-Za-z]")]
    private static partial Regex PossuiLetras();
}
