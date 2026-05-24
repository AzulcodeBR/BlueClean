using System.Text.RegularExpressions;

namespace BlueCleanApi.Utils
{
    /// <summary>
    /// Classe com funções auxiliares gerais
    /// </summary>
    public static class Funcoes
    {
        /// <summary>
        /// Valida se o CPF é válido
        /// </summary>
        /// <param name="cpf">CPF a ser validado (com ou sem formatação)</param>
        /// <returns>True se o CPF for válido, False caso contrário</returns>
        public static bool ValidarCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            cpf = new string(cpf.Where(char.IsDigit).ToArray());

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
        /// Valida se o CNPJ é válido
        /// </summary>
        /// <param name="cnpj">CNPJ a ser validado (com ou sem formatação)</param>
        /// <returns>True se o CNPJ for válido, False caso contrário</returns>
        public static bool ValidarCnpj(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
                return false;

            cnpj = new string(cnpj.Where(char.IsDigit).ToArray());

            if (cnpj.Length != 14)
                return false;

            if (cnpj.All(c => c == cnpj[0]))
                return false;

            int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            int soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(cnpj[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            int digitoVerificador1 = resto < 2 ? 0 : 11 - resto;

            if (int.Parse(cnpj[12].ToString()) != digitoVerificador1)
                return false;

            int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            
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
        /// Valida se o telefone está no formato brasileiro válido (DDD + 8 ou 9 dígitos)
        /// </summary>
        /// <param name="telefone">Número de telefone a ser validado</param>
        /// <returns>True se o telefone for válido, False caso contrário</returns>
        public static bool ValidarTelefone(string telefone)
        {
            if (string.IsNullOrWhiteSpace(telefone))
                return false;

            telefone = Regex.Replace(telefone, @"[^\d]", "");

            if (telefone.Length != 10 && telefone.Length != 11)
                return false;

            var ddd = int.Parse(telefone.Substring(0, 2));

            var ddsValidos = new[]
            {
                11, 12, 13, 14, 15, 16, 17, 18, 19, // São Paulo
                21, 22, 24, // Rio de Janeiro
                27, 28, // Espírito Santo
                31, 32, 33, 34, 35, 37, 38, // Minas Gerais
                41, 42, 43, 44, 45, 46, // Paraná
                47, 48, 49, // Santa Catarina
                51, 53, 54, 55, // Rio Grande do Sul
                61, // Distrito Federal
                62, 64, // Goiás
                63, // Tocantins
                65, 66, // Mato Grosso
                67, // Mato Grosso do Sul
                68, // Acre
                69, // Rondônia
                71, 73, 74, 75, 77, // Bahia
                79, // Sergipe
                81, 87, // Pernambuco
                82, // Alagoas
                83, // Paraíba
                84, // Rio Grande do Norte
                85, 88, // Ceará
                86, 89, // Piauí
                91, 93, 94, // Pará
                92, 97, // Amazonas
                95, // Roraima
                96, // Amapá
                98, 99  // Maranhão
            };

            if (!ddsValidos.Contains(ddd))
                return false;

            if (telefone.Length == 11 && telefone[2] != '9')
                return false;

            var numeroSemDdd = telefone.Substring(2);

            if (numeroSemDdd.All(c => c == numeroSemDdd[0]))
                return false;

            return true;
        }

        /// <summary>
        /// Valida se o e-mail possui um formato válido
        /// </summary>
        /// <param name="email">E-mail a ser validado</param>
        /// <returns>True se o e-mail for válido, False caso contrário</returns>
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
        /// Remove todos os caracteres não numéricos de uma string (remove máscara)
        /// </summary>
        /// <param name="valor">String com máscara a ser removida</param>
        /// <returns>String contendo apenas dígitos numéricos</returns>
        public static string RemoverMascara(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return string.Empty;

            return Regex.Replace(valor, @"[^\d]", string.Empty);
        }

        /// <summary>
        /// Valida se o nome possui mais de um nome (nome e sobrenome).
        /// </summary>
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
        /// Valida a senha do cliente conforme regras de segurança do cadastro.
        /// </summary>
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
        /// Verifica se a string contém sequência numérica ascendente ou descendente.
        /// </summary>
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
}