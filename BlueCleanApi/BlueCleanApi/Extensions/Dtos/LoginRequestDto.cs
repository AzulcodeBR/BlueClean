namespace BlueCleanApi.Extensions.Dtos
{
    /// <summary>
    /// Modelo de requisição para autenticação de usuário
    /// </summary>
    public class LoginRequestDto
    {
        /// <summary>
        /// E-mail do usuário
        /// </summary>
        /// <example>usuario@blueclean.com</example>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Senha do usuário (mínimo de 10 caracteres)
        /// </summary>
        /// <example>senhaSegura123</example>
        public string Senha { get; set; } = string.Empty;
    }
}
