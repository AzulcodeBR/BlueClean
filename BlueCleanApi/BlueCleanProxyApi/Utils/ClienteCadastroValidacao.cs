using System.Text.RegularExpressions;
using BlueCleanProxyApi.Extensions.Dtos;
using BlueCleanProxyApi.Extensions.Interfaces;
using BlueCleanProxyApi.Resources;

namespace BlueCleanProxyApi.Utils;

public static class ClienteCadastroValidacao
{
  public static bool Validar(ClienteCadastroRequestDto request, INotificadorDominio notificador)
  {
    if (string.IsNullOrWhiteSpace(request.Nome))
      notificador.AdicionarNotificacao(StringResources.ClienteNomeObrigatorio);
    else if (request.Nome.Trim().Length > 150)
      notificador.AdicionarNotificacao(StringResources.ClienteNomeMaximoCaracteres);
    else if (!Funcoes.ValidarNomeCompleto(request.Nome))
      notificador.AdicionarNotificacao(StringResources.ClienteNomeDeveConterMaisDeUmNome);

    if (string.IsNullOrWhiteSpace(request.Email))
      notificador.AdicionarNotificacao(StringResources.ClienteEmailObrigatorio);
    else if (!Funcoes.ValidarEmail(request.Email))
      notificador.AdicionarNotificacao(StringResources.ClienteEmailInvalido);
    else if (request.Email.Trim().Length > 150)
      notificador.AdicionarNotificacao(StringResources.ClienteEmailMaximoCaracteres);

    if (!string.IsNullOrWhiteSpace(request.Telefone))
    {
      var telefone = Funcoes.RemoverMascara(request.Telefone);

      if (telefone.Length > 11)
        notificador.AdicionarNotificacao(StringResources.ClienteTelefoneMaximoCaracteres);
      else if (!Funcoes.ValidarTelefone(request.Telefone))
        notificador.AdicionarNotificacao(StringResources.ClienteTelefoneInvalido);
    }

    if (string.IsNullOrWhiteSpace(request.CpfCnpj))
      notificador.AdicionarNotificacao(StringResources.ClienteCpfCnpjObrigatorio);
    else
    {
      var cpfCnpj = Funcoes.RemoverMascara(request.CpfCnpj);

      if (cpfCnpj.Length == 11 && !Funcoes.ValidarCpf(cpfCnpj))
        notificador.AdicionarNotificacao(StringResources.ClienteCpfCnpjInvalido);
      else if (cpfCnpj.Length == 14 && !Funcoes.ValidarCnpj(cpfCnpj))
        notificador.AdicionarNotificacao(StringResources.ClienteCpfCnpjInvalido);
      else if (cpfCnpj.Length is not (11 or 14))
        notificador.AdicionarNotificacao(StringResources.ClienteCpfCnpjInvalido);
    }

    if (string.IsNullOrWhiteSpace(request.Senha))
      notificador.AdicionarNotificacao(StringResources.ClienteSenhaObrigatoria);
    else
    {
      if (request.Senha.Length < 10)
        notificador.AdicionarNotificacao(StringResources.SenhaDeveTerMinimoCaracteres);

      if (!Regex.IsMatch(request.Senha, @"[A-Za-z]"))
        notificador.AdicionarNotificacao(StringResources.ClienteSenhaDeveConterLetras);

      if (!Regex.IsMatch(request.Senha, @"[A-Z]"))
        notificador.AdicionarNotificacao(StringResources.ClienteSenhaDeveConterLetraMaiuscula);

      if (!Regex.IsMatch(request.Senha, @"[^A-Za-z0-9]"))
        notificador.AdicionarNotificacao(StringResources.ClienteSenhaDeveConterCaractereEspecial);

      if (Funcoes.ContemNumerosSequenciais(request.Senha))
        notificador.AdicionarNotificacao(StringResources.ClienteSenhaNaoPodeConterNumerosSequenciais);
    }

    if (!string.IsNullOrWhiteSpace(request.Observacao) && request.Observacao.Trim().Length > 500)
      notificador.AdicionarNotificacao(StringResources.ClienteObservacaoMaximoCaracteres);

    return notificador.VerificarOperacao();
  }
}
