namespace BlueCleanApi.Resources;

public class StringResources
{
    public static string FormatarResource(string principal, params object[] args)
    {
        return string.Format(principal, args);
    }

    public static string FormatarResourceToLower(string principal, params string[] args)
    {
        if (args == null)
            return principal;

        return FormatarResource(principal, args.Select(x => x.ToLower()).ToArray());
    }

    public const string HorizonteAzulConnection = "HorizonteAzulConnection";
    public const string JwtKeyNaoConfigurado = "Jwt:Key não está configurado";

    public const string NenhumRegistroEncontrado = "Nenhum Registro Encontrado.";
    public const string EmailOuSenhaInvalidos = "Email ou senha inválidos.";
    public const string UsuarioNaoEstaAtivo = "O Usuário não está ativo, contate o administrador do sistema.";

    // Validações de Login
    public const string EmailObrigatorio = "E-mail é obrigatório.";
    public const string SenhaObrigatoria = "Senha é obrigatória.";
    public const string SenhaDeveTerMinimoCaracteres = "A senha deve possuir no mínimo 10 caracteres.";
    public const string UsuarioNaoAutenticado = "Usuário não autenticado.";
    public const string EmailUsuarioNaoEncontradoToken = "E-mail do usuário não encontrado no token.";

    // Erros de Autenticação JWT
    public const string TokenNaoInformado = "Token de autenticação não informado.";
    public const string TokenInvalido = "Token de autenticação inválido.";
    public const string TokenExpirado = "Token de autenticação expirado.";
}
