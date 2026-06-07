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

        /// <summary>
        /// Nome do usuário autenticado.
        /// </summary>
        public string NomeUsuario { get; set; } = string.Empty;

        /// <summary>
        /// Tipo de login autenticado (1 = Cliente, 2 = Gerencial).
        /// </summary>
        public int TipoLogin { get; set; }

        /// <summary>
        /// Data/hora UTC de expiração do token JWT.
        /// </summary>
        public DateTime ExpiraEmUtc { get; set; }
    }
}
