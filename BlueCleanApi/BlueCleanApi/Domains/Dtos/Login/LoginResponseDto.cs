namespace BlueCleanApi.Domains.Dtos.Login
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
    }
}
