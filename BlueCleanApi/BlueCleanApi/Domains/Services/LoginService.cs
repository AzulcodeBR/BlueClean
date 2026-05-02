using BlueCleanApi.Domains.Interfaces;
using BlueCleanApi.Extensions.Dtos;
using BlueCleanApi.Extensions.Interfaces;
using BlueCleanApi.Resources;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlueCleanApi.Domains.Services
{
    public class LoginService : ILoginService
    {
        private readonly INotificadorDominio _notificadorDominio;
        private readonly IConfiguration _configuration;

        public LoginService(
            INotificadorDominio notificadorDominio,
            IConfiguration configuration)
        {
            _notificadorDominio = notificadorDominio;
            _configuration = configuration;
        }

        public async Task<LoginResponseDto?> AutenticarAsync(string email, string senha)
        {
            // Validações
            if (string.IsNullOrWhiteSpace(email))
            {
                _notificadorDominio.AdicionarNotificacao(StringResources.EmailObrigatorio);
                return null;
            }

            if (string.IsNullOrWhiteSpace(senha))
            {
                _notificadorDominio.AdicionarNotificacao(StringResources.SenhaObrigatoria);
                return null;
            }

            if (senha.Length < 10)
            {
                _notificadorDominio.AdicionarNotificacao(StringResources.SenhaDeveTerMinimoCaracteres);
                return null;
            }

            // Simular validação (futuramente será contra o banco de dados)
            // Por enquanto, qualquer e-mail e senha com mais de 10 caracteres é válido

            // Gerar JWT Token
            var token = GerarJwtToken(email);

            return await Task.FromResult(new LoginResponseDto
            {
                Token = token
            });
        }

        private string GerarJwtToken(string email)
        {
            var jwtKey = _configuration["Jwt:Key"];

            if (string.IsNullOrEmpty(jwtKey))
                throw new InvalidOperationException(StringResources.JwtKeyNaoConfigurado);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var expiresInMinutes = _configuration.GetValue<int>("Jwt:ExpiresInMinutes", 60);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiresInMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
