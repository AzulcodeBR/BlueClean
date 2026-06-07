using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace BlueCleanApi.Utils;

/// <summary>
/// Funções utilitárias reutilizáveis para validação de dados e transformações comuns do sistema.
/// </summary>
public static partial class Funcoes
{
    /// <summary>
    /// Valida se o CPF informado é válido, aplicando o algoritmo oficial dos dígitos verificadores.
    /// </summary>
    /// <param name="cpf">CPF com ou sem máscara (pontos e traço são ignorados).</param>
    /// <returns>
    /// <c>true</c> quando o CPF possui 11 dígitos, não é sequência repetida e os dígitos verificadores estão corretos;
    /// caso contrário, <c>false</c>.
    /// </returns>
    public static bool ValidarCpf(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
            return false;

        cpf = new string([.. cpf.Where(char.IsDigit)]);

        if (cpf.Length != 11)
            return false;

        if (cpf.All(c => c == cpf[0]))
            return false;

        int soma = 0;

        for (int i = 0; i < 9; i++)
            soma += int.Parse(cpf[i].ToString()) * (10 - i);

        int resto = soma % 11;
        int digitoVerificador1 = resto < 2 ? 0 : 11 - resto;

        if (int.Parse(cpf[9].ToString()) != digitoVerificador1)
            return false;

        soma = 0;

        for (int i = 0; i < 10; i++)
            soma += int.Parse(cpf[i].ToString()) * (11 - i);

        resto = soma % 11;

        int digitoVerificador2 = resto < 2 ? 0 : 11 - resto;

        if (int.Parse(cpf[10].ToString()) != digitoVerificador2)
            return false;

        return true;
    }

    /// <summary>
    /// Valida se o CNPJ informado é válido, aplicando o algoritmo oficial dos dígitos verificadores.
    /// </summary>
    /// <param name="cnpj">CNPJ com ou sem máscara (pontos, barra e traço são ignorados).</param>
    /// <returns>
    /// <c>true</c> quando o CNPJ possui 14 dígitos, não é sequência repetida e os dígitos verificadores estão corretos;
    /// caso contrário, <c>false</c>.
    /// </returns>
    public static bool ValidarCnpj(string cnpj)
    {
        if (string.IsNullOrWhiteSpace(cnpj))
            return false;

        cnpj = new string([.. cnpj.Where(char.IsDigit)]);

        if (cnpj.Length != 14)
            return false;

        if (cnpj.All(c => c == cnpj[0]))
            return false;

        int[] multiplicador1 = [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];

        int soma = 0;

        for (int i = 0; i < 12; i++)
            soma += int.Parse(cnpj[i].ToString()) * multiplicador1[i];

        int resto = soma % 11;
        int digitoVerificador1 = resto < 2 ? 0 : 11 - resto;

        if (int.Parse(cnpj[12].ToString()) != digitoVerificador1)
            return false;

        int[] multiplicador2 = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];

        soma = 0;

        for (int i = 0; i < 13; i++)
            soma += int.Parse(cnpj[i].ToString()) * multiplicador2[i];

        resto = soma % 11;

        int digitoVerificador2 = resto < 2 ? 0 : 11 - resto;

        if (int.Parse(cnpj[13].ToString()) != digitoVerificador2)
            return false;

        return true;
    }

    /// <summary>
    /// Valida se o telefone está em formato brasileiro aceito pelo sistema.
    /// </summary>
    /// <param name="telefone">Número de telefone com ou sem máscara.</param>
    /// <returns>
    /// <c>true</c> quando o telefone possui DDD válido no Brasil e 8 ou 9 dígitos no número local
    /// (celular com 11 dígitos deve iniciar com 9 após o DDD); caso contrário, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// Rejeita números com todos os dígitos locais iguais e DDD fora da lista de códigos brasileiros suportados.
    /// </remarks>
    public static bool ValidarTelefone(string telefone)
    {
        if (string.IsNullOrWhiteSpace(telefone))
            return false;

        telefone = Regex.Replace(telefone, @"[^\d]", "");

        if (telefone.Length != 10 && telefone.Length != 11)
            return false;

        var ddd = int.Parse(telefone[0..2]);

        var ddsValidos = new[]
        {
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
        };

        if (!ddsValidos.Contains(ddd))
            return false;

        if (telefone.Length == 11 && telefone[2] != '9')
            return false;

        var numeroSemDdd = telefone[2..];

        if (numeroSemDdd.All(c => c == numeroSemDdd[0]))
            return false;

        return true;
    }

    /// <summary>
    /// Valida se o e-mail possui formato sintático válido.
    /// </summary>
    /// <param name="email">Endereço de e-mail a ser validado.</param>
    /// <returns>
    /// <c>true</c> quando o e-mail atende ao padrão esperado e é aceito por <see cref="System.Net.Mail.MailAddress"/>;
    /// caso contrário, <c>false</c>.
    /// </returns>
    public static bool ValidarEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        email = email.Trim();

        var pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

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

    /// <summary>
    /// Remove caracteres não numéricos de uma string, útil para normalizar CPF, CNPJ e telefone.
    /// </summary>
    /// <param name="valor">Texto de entrada, geralmente com máscara de formatação.</param>
    /// <returns>
    /// String contendo apenas dígitos. Retorna <see cref="string.Empty"/> quando o valor informado for nulo,
    /// vazio ou apenas espaços em branco.
    /// </returns>
    public static string RemoverMascara(string valor)
    {
        if (string.IsNullOrWhiteSpace(valor))
            return string.Empty;

        return Regex.Replace(valor, @"[^\d]", string.Empty);
    }

    /// <summary>
    /// Valida se o nome informado representa um nome completo (nome e sobrenome).
    /// </summary>
    /// <param name="nome">Nome completo do usuário ou cliente.</param>
    /// <returns>
    /// <c>true</c> quando existem ao menos duas partes com 2 ou mais caracteres, compostas apenas por letras,
    /// hífen ou apóstrofo; caso contrário, <c>false</c>.
    /// </returns>
    public static bool ValidarNomeCompleto(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            return false;

        var partes = nome.Trim()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (partes.Length < 2)
            return false;

        return partes.All(parte =>
            parte.Length >= 2 &&
            parte.All(c => char.IsLetter(c) || c is '-' or '\''));
    }

    /// <summary>
    /// Valida a senha do cliente conforme as regras de segurança do cadastro.
    /// </summary>
    /// <param name="senha">Senha em texto puro informada no cadastro.</param>
    /// <returns>
    /// <c>true</c> quando a senha atende a todos os critérios; caso contrário, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// Critérios avaliados: mínimo de 10 caracteres, presença de letras, ao menos uma letra maiúscula,
    /// ao menos um caractere especial e ausência de sequências numéricas (ex.: 1234 ou 4321).
    /// </remarks>
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

        if (ContemNumerosSequenciais(senha, 4))
            return false;

        return true;
    }

    /// <summary>
    /// Verifica se a string contém sequência numérica consecutiva ascendente ou descendente.
    /// </summary>
    /// <param name="valor">Texto a ser analisado (ex.: senha).</param>
    /// <param name="tamanhoMinimo">Quantidade mínima de dígitos consecutivos para considerar sequência inválida. Padrão: 4.</param>
    /// <returns>
    /// <c>true</c> quando encontra sequência como 1234 ou 4321; caso contrário, <c>false</c>.
    /// </returns>
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

    /// <summary>
    /// Gera o hash SHA-256 de um texto e retorna o resultado codificado em Base64.
    /// </summary>
    /// <param name="valor">Texto de entrada (ex.: senha em texto puro).</param>
    /// <returns>Representação Base64 do hash SHA-256 calculado com codificação UTF-8.</returns>
    /// <remarks>
    /// Utilizado para persistência segura de senhas. Não é reversível para o texto original.
    /// </remarks>
    public static string ConvertToSHA256(string valor)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(valor));
        return Convert.ToBase64String(bytes);
    }
}
