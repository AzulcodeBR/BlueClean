namespace BlueCleanApi.Domains.Dtos.Login
{
    /// <summary>
    /// Modelo de requisição para autenticação de usuário
    /// </summary>
    public class LoginRequestDto
    {
        /// <summary>
        /// E-mail ou CPF/CNPJ do usuário
        /// </summary>
        /// <example>usuario@blueclean.com</example>
        public string Identificador { get; set; } = string.Empty;

        /// <summary>
        /// Tipo de login (1 = Cliente, 2 = Gerencial)
        /// </summary>
        /// <example>1</example>
        public int TipoLogin { get; set; }

        /// <summary>
        /// Senha do usuário (mínimo de 10 caracteres)
        /// </summary>
        /// <example>senhaSegura123</example>
        public string Senha { get; set; } = string.Empty;
    }
}
