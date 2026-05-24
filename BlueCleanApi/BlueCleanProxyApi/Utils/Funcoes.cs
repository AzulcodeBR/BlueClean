using System.Text.RegularExpressions;

namespace BlueCleanProxyApi.Utils;

public static class Funcoes
{
  public static bool ValidarCpf(string cpf)
  {
    if (string.IsNullOrWhiteSpace(cpf))
      return false;

    cpf = new string(cpf.Where(char.IsDigit).ToArray());

    if (cpf.Length != 11)
      return false;

    if (cpf.All(c => c == cpf[0]))
      return false;

    var soma = 0;

    for (var i = 0; i < 9; i++)
      soma += int.Parse(cpf[i].ToString()) * (10 - i);

    var resto = soma % 11;
    var digitoVerificador1 = resto < 2 ? 0 : 11 - resto;

    if (int.Parse(cpf[9].ToString()) != digitoVerificador1)
      return false;

    soma = 0;

    for (var i = 0; i < 10; i++)
      soma += int.Parse(cpf[i].ToString()) * (11 - i);

    resto = soma % 11;

    var digitoVerificador2 = resto < 2 ? 0 : 11 - resto;

    return int.Parse(cpf[10].ToString()) == digitoVerificador2;
  }

  public static bool ValidarCnpj(string cnpj)
  {
    if (string.IsNullOrWhiteSpace(cnpj))
      return false;

    cnpj = new string(cnpj.Where(char.IsDigit).ToArray());

    if (cnpj.Length != 14)
      return false;

    if (cnpj.All(c => c == cnpj[0]))
      return false;

    int[] multiplicador1 = [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];

    var soma = 0;

    for (var i = 0; i < 12; i++)
      soma += int.Parse(cnpj[i].ToString()) * multiplicador1[i];

    var resto = soma % 11;
    var digitoVerificador1 = resto < 2 ? 0 : 11 - resto;

    if (int.Parse(cnpj[12].ToString()) != digitoVerificador1)
      return false;

    int[] multiplicador2 = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];

    soma = 0;

    for (var i = 0; i < 13; i++)
      soma += int.Parse(cnpj[i].ToString()) * multiplicador2[i];

    resto = soma % 11;

    var digitoVerificador2 = resto < 2 ? 0 : 11 - resto;

    return int.Parse(cnpj[13].ToString()) == digitoVerificador2;
  }

  public static bool ValidarTelefone(string telefone)
  {
    if (string.IsNullOrWhiteSpace(telefone))
      return false;

    telefone = Regex.Replace(telefone, @"[^\d]", string.Empty);

    if (telefone.Length != 10 && telefone.Length != 11)
      return false;

    var ddd = int.Parse(telefone[..2]);

    int[] ddsValidos =
    [
      11, 12, 13, 14, 15, 16, 17, 18, 19,
      21, 22, 24,
      27, 28,
      31, 32, 33, 34, 35, 37, 38,
      41, 42, 43, 44, 45, 46,
      47, 48, 49,
      51, 53, 54, 55,
      61,
      62, 64,
      63,
      65, 66,
      67,
      68,
      69,
      71, 73, 74, 75, 77,
      79,
      81, 87,
      82,
      83,
      84,
      85, 88,
      86, 89,
      91, 93, 94,
      92, 97,
      95,
      96,
      98, 99
    ];

    if (!ddsValidos.Contains(ddd))
      return false;

    if (telefone.Length == 11 && telefone[2] != '9')
      return false;

    var numeroSemDdd = telefone[2..];

    return !numeroSemDdd.All(c => c == numeroSemDdd[0]);
  }

  public static bool ValidarEmail(string email)
  {
    if (string.IsNullOrWhiteSpace(email))
      return false;

    email = email.Trim();

    const string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

    if (!Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase))
      return false;

    try
    {
      var addr = new System.Net.Mail.MailAddress(email);
      return addr.Address == email;
    }
    catch
    {
      return false;
    }
  }

  public static string RemoverMascara(string valor)
  {
    if (string.IsNullOrWhiteSpace(valor))
      return string.Empty;

    return Regex.Replace(valor, @"[^\d]", string.Empty);
  }

  public static bool ValidarNomeCompleto(string nome)
  {
    if (string.IsNullOrWhiteSpace(nome))
      return false;

    var partes = nome.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

    if (partes.Length < 2)
      return false;

    return partes.All(parte =>
      parte.Length >= 2 &&
      parte.All(c => char.IsLetter(c) || c is '-' or '\''));
  }

  public static bool ValidarSenhaCliente(string senha)
  {
    if (string.IsNullOrEmpty(senha) || senha.Length < 10)
      return false;

    if (!Regex.IsMatch(senha, @"[A-Za-z]"))
      return false;

    if (!Regex.IsMatch(senha, @"[A-Z]"))
      return false;

    if (!Regex.IsMatch(senha, @"[^A-Za-z0-9]"))
      return false;

    return !ContemNumerosSequenciais(senha, 4);
  }

  public static bool ContemNumerosSequenciais(string valor, int tamanhoMinimo = 4)
  {
    if (string.IsNullOrEmpty(valor) || valor.Length < tamanhoMinimo)
      return false;

    for (var i = 0; i <= valor.Length - tamanhoMinimo; i++)
    {
      if (!char.IsDigit(valor[i]))
        continue;

      var ascendente = true;
      var descendente = true;

      for (var j = 1; j < tamanhoMinimo; j++)
      {
        if (!char.IsDigit(valor[i + j]))
        {
          ascendente = false;
          descendente = false;
          break;
        }

        if (valor[i + j] - valor[i + j - 1] != 1)
          ascendente = false;

        if (valor[i + j - 1] - valor[i + j] != 1)
          descendente = false;
      }

      if (ascendente || descendente)
        return true;
    }

    return false;
  }
}
