using BlueCleanApi.Domains.Dtos.Login;
using BlueCleanApi.Domains.Interfaces;
using BlueCleanApi.Enums;
using BlueCleanApi.Extensions.Interfaces;
using BlueCleanApi.Models.BlueCleanDb;
using BlueCleanApi.Resources;
using BlueCleanApi.Utils;
using Microsoft.EntityFrameworkCore;
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
        private readonly LavanderiaContext _context;

        public LoginService(
            INotificadorDominio notificadorDominio,
            IConfiguration configuration,
            LavanderiaContext context)
        {
            _notificadorDominio = notificadorDominio;
            _configuration = configuration;
            _context = context;
        }

        public async Task<LoginResponseDto?> AutenticarAsync(string identificador, string senha, int tipoLogin)
        {
            if (!Enum.IsDefined(typeof(ETipoLogin), tipoLogin))
            {
                _notificadorDominio.AdicionarNotificacao(StringResources.LoginTipoInvalido);
                return null;
            }

            var identificadorNormalizado = (identificador ?? string.Empty).Trim();

            if (string.IsNullOrWhiteSpace(identificadorNormalizado))
            {
                _notificadorDominio.AdicionarNotificacao(StringResources.EmailObrigatorio);
                return null;
            }

            if (string.IsNullOrWhiteSpace(senha))
            {
                _notificadorDominio.AdicionarNotificacao(StringResources.SenhaObrigatoria);
                return null;
            }

            var documento = Funcoes.RemoverMascara(identificadorNormalizado);
            var ehEmail = Funcoes.ValidarEmail(identificadorNormalizado);
            var ehCpfValido = documento.Length == 11 && Funcoes.ValidarCpf(documento);
            var ehCnpjValido = documento.Length == 14 && Funcoes.ValidarCnpj(documento);

            if (!ehEmail && !ehCpfValido && !ehCnpjValido)
            {
                _notificadorDominio.AdicionarNotificacao(StringResources.LoginIdentificadorInvalido);
                return null;
            }

            var tipo = (ETipoLogin)tipoLogin;

            return tipo switch
            {
                ETipoLogin.CLIENTE => await AutenticarClienteAsync(
                    identificadorNormalizado,
                    documento,
                    ehEmail,
                    senha),
                ETipoLogin.GERENCIAL => await AutenticarGerencialAsync(
                    identificadorNormalizado,
                    documento,
                    ehEmail,
                    senha),
                _ => null
            };
        }

        private async Task<LoginResponseDto?> AutenticarClienteAsync(
            string identificador,
            string documento,
            bool ehEmail,
            string senha)
        {
            Cliente? cliente = ehEmail
                ? await _context.Cliente
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.Email == identificador.ToUpperInvariant())
                : await _context.Cliente
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.CpfCnpj == documento);

            if (cliente == null || !SenhaCorresponde(senha, cliente.Senha))
            {
                _notificadorDominio.AdicionarNotificacao(
                    ehEmail ? StringResources.EmailOuSenhaInvalidos : StringResources.CpfCnpjOuSenhaInvalidos);
                return null;
            }

            if (cliente.StatusClienteId is (int)EStatusCliente.BLOQUEADO_TEMPORARIAMENTE
                or (int)EStatusCliente.BLOQUEADO_DEFINITIVO)
            {
                var observacao = string.IsNullOrWhiteSpace(cliente.Observacao)
                    ? string.Empty
                    : $"{cliente.Observacao.Trim()} ";

                _notificadorDominio.AdicionarNotificacao(
                    $"A conta foi bloqueada. {observacao}{StringResources.ContaClienteBloqueadaMensagemFinal}");
                return null;
            }

            if (cliente.StatusClienteId == (int)EStatusCliente.AGUARDANDO_CONFIRMACAO_EMAIL)
            {
                _notificadorDominio.AdicionarNotificacao(StringResources.CadastroClienteNaoAtivoAguardandoEmail);
                return null;
            }

            if (cliente.StatusClienteId != (int)EStatusCliente.ATIVO)
            {
                _notificadorDominio.AdicionarNotificacao(StringResources.ContaClienteNaoAtiva);
                return null;
            }

            return GerarLoginResponse(
                cliente.Email,
                cliente.Nome,
                (int)ETipoLogin.CLIENTE);
        }

        private async Task<LoginResponseDto?> AutenticarGerencialAsync(
            string identificador,
            string documento,
            bool ehEmail,
            string senha)
        {
            Usuario? usuario = ehEmail
                ? await _context.Usuario
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.Email == identificador.ToUpperInvariant())
                : await _context.Usuario
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.Cpf == documento);

            if (usuario == null || !SenhaCorresponde(senha, usuario.Senha))
            {
                _notificadorDominio.AdicionarNotificacao(
                    ehEmail ? StringResources.EmailOuSenhaInvalidos : StringResources.CpfCnpjOuSenhaInvalidos);
                return null;
            }

            if (usuario.StatusUsuarioGerencialId is (int)EStatusUsuarioGerencial.BLOQUEADO_TEMPORARIAMENTE
                or (int)EStatusUsuarioGerencial.BLOQUEADO_DEFINITIVO)
            {
                _notificadorDominio.AdicionarNotificacao(StringResources.ContaGerencialNaoAtiva);
                return null;
            }

            if (usuario.StatusUsuarioGerencialId != (int)EStatusUsuarioGerencial.ATIVO)
            {
                _notificadorDominio.AdicionarNotificacao(StringResources.ContaGerencialNaoAtiva);
                return null;
            }

            return GerarLoginResponse(
                usuario.Email,
                usuario.Nome,
                (int)ETipoLogin.GERENCIAL);
        }

        private static bool SenhaCorresponde(string senhaInformada, string hashPersistida)
        {
            return string.Equals(
                Funcoes.ConvertToSHA256(senhaInformada),
                hashPersistida,
                StringComparison.Ordinal);
        }

        private LoginResponseDto GerarLoginResponse(string email, string nomeUsuario, int tipoLogin)
        {
            var expiresInMinutes = _configuration.GetValue<int>("Jwt:ExpiresInMinutes", 60);
            var expiraEmUtc = DateTime.UtcNow.AddMinutes(expiresInMinutes);

            var token = GerarJwtToken(email, nomeUsuario, tipoLogin, expiraEmUtc);

            return new LoginResponseDto
            {
                Token = token,
                NomeUsuario = nomeUsuario,
                TipoLogin = tipoLogin,
                ExpiraEmUtc = expiraEmUtc
            };
        }

        private string GerarJwtToken(string email, string nomeUsuario, int tipoLogin, DateTime expiraEmUtc)
        {
            var jwtKey = _configuration["Jwt:Key"];

            if (string.IsNullOrEmpty(jwtKey))
                throw new InvalidOperationException(StringResources.JwtKeyNaoConfigurado);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Name, nomeUsuario),
                new Claim("tipo_login", tipoLogin.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiraEmUtc,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
