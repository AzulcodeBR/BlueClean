namespace BlueCleanApi.Extensions.Dtos
{
    /// <summary>
    /// Modelo de dados do usuário autenticado
    /// </summary>
    public class UsuarioLogadoDto
    {
        /// <summary>
        /// E-mail do usuário logado
        /// </summary>
        /// <example>usuario@blueclean.com</example>
        public string Email { get; set; } = string.Empty;
    }
}
