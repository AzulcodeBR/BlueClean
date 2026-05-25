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

        return FormatarResource(principal, [.. args.Select(x => x.ToLower())]);
    }

    public const string HorizonteAzulConnection = "HorizonteAzulConnection";
    public const string JwtKeyNaoConfigurado = "Jwt:Key não está configurado";

    public const string NenhumRegistroEncontrado = "Nenhum Registro Encontrado.";
    public const string EmailOuSenhaInvalidos = "Email ou senha inválidos.";
    public const string UsuarioNaoEstaAtivo = "O Usuário não está ativo, contate o administrador do sistema.";

    // Validações de Login
    public const string EmailObrigatorio = "O E-mail ou a Senha é ínválido!";
    public const string SenhaObrigatoria = "O E-mail ou a Senha é ínválido!";
    public const string SenhaDeveTerMinimoCaracteres = "A senha deve possuir no mínimo 10 caracteres.";
    public const string UsuarioNaoAutenticado = "Usuário não autenticado.";
    public const string EmailUsuarioNaoEncontradoToken = "E-mail do usuário não encontrado no token.";

    // Erros de Autenticação JWT
    public const string TokenNaoInformado = "Token de autenticação não informado.";
    public const string TokenInvalido = "Token de autenticação inválido.";
    public const string TokenExpirado = "Token de autenticação expirado.";

    // Validações de CEP
    public const string CepObrigatorio = "CEP é obrigatório";
    public const string CepDeveConterOitoDigitos = "CEP deve conter 8 dígitos";
    public const string CepDeveConterApenasNumeros = "CEP deve conter apenas números";
    public const string ErroConsultarCepApi = "Erro ao consultar CEP na API ViaCEP";
    public const string CepNaoEncontrado = "CEP não encontrado";

    // Validações de Cliente
    public const string ClienteErroInesperado = "Ocorreu um erro inesperado ao processar o cadastro, por favor tente novamente!";
    public const string ClienteNomeObrigatorio = "O nome é obrigatório.";
    public const string ClienteNomeMaximoCaracteres = "O nome deve possuir no máximo 150 caracteres.";
    public const string ClienteNomeDeveConterMaisDeUmNome = "Informe nome e sobrenome (mais de um nome).";
    public const string ClienteEmailObrigatorio = "O e-mail é obrigatório.";
    public const string ClienteEmailInvalido = "O e-mail informado é inválido.";
    public const string ClienteEmailMaximoCaracteres = "O e-mail deve possuir no máximo 150 caracteres.";
    public const string ClienteEmailJaCadastrado = "O e-mail informado já está cadastrado.";
    public const string ClienteTelefoneObrigatorio = "O telefone é obrigatório.";
    public const string ClienteTelefoneInvalido = "O telefone informado é inválido.";
    public const string ClienteTelefoneMaximoCaracteres = "O telefone deve possuir no máximo 11 dígitos.";
    public const string ClienteCpfCnpjObrigatorio = "O CPF/CNPJ é obrigatório.";
    public const string ClienteCpfCnpjInvalido = "O CPF/CNPJ informado é inválido.";
    public const string ClienteCpfCnpjJaCadastrado = "O CPF/CNPJ informado já está cadastrado.";
    public const string ClienteObservacaoMaximoCaracteres = "A observação deve possuir no máximo 500 caracteres.";
    public const string ClienteStatusInvalido = "Status de cliente padrão não encontrado.";
    public const string ClienteSenhaObrigatoria = "A senha é obrigatória.";
    public const string ClienteSenhaDeveConterLetras = "A senha deve conter letras.";
    public const string ClienteSenhaDeveConterLetraMaiuscula = "A senha deve conter pelo menos uma letra maiúscula.";
    public const string ClienteSenhaDeveConterCaractereEspecial = "A senha deve conter pelo menos um caractere especial.";
    public const string ClienteSenhaNaoPodeConterNumerosSequenciais = "A senha não pode conter números sequenciais (ex.: 1234).";
    public const string ClienteErroComunicacaoBackend = "Erro ao comunicar com a API de backend.";
}
