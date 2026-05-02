namespace BlueCleanApi.Extensions.Dtos
{
    /// <summary>
    /// Modelo de resposta após autenticação bem-sucedida
    /// </summary>
    public class LoginResponseDto
    {
        /// <summary>
        /// Token JWT para autenticação nas próximas requisições
        /// </summary>
        /// <example>eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...</example>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// E-mail do usuário autenticado
        /// </summary>
        /// <example>usuario@blueclean.com</example>
        public string Email { get; set; } = string.Empty;
    }
}
