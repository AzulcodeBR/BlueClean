namespace BlueCleanProxyApi.Resources;

public class StringResources
{
  public const string NenhumRegistroEncontrado = "Nenhum Registro Encontrado.";

  public const string SenhaDeveTerMinimoCaracteres = "A senha deve possuir no mínimo 10 caracteres.";
  public const string ClienteSenhaObrigatoria = "A senha do cliente é obrigatória.";
  public const string ClienteSenhaDeveConterLetras = "A senha deve conter letras.";
  public const string ClienteSenhaDeveConterLetraMaiuscula = "A senha deve conter pelo menos uma letra maiúscula.";
  public const string ClienteSenhaDeveConterCaractereEspecial = "A senha deve conter pelo menos um caractere especial.";
  public const string ClienteSenhaNaoPodeConterNumerosSequenciais = "A senha não pode conter números sequenciais (ex.: 1234).";

  public const string ClienteNomeObrigatorio = "O nome do cliente é obrigatório.";
  public const string ClienteNomeMaximoCaracteres = "O nome do cliente deve possuir no máximo 150 caracteres.";
  public const string ClienteNomeDeveConterMaisDeUmNome = "Informe nome e sobrenome (mais de um nome).";
  public const string ClienteEmailObrigatorio = "O e-mail do cliente é obrigatório.";
  public const string ClienteEmailInvalido = "O e-mail informado é inválido.";
  public const string ClienteEmailMaximoCaracteres = "O e-mail do cliente deve possuir no máximo 150 caracteres.";
  public const string ClienteTelefoneInvalido = "O telefone informado é inválido.";
  public const string ClienteTelefoneMaximoCaracteres = "O telefone deve possuir no máximo 11 dígitos.";
  public const string ClienteCpfCnpjObrigatorio = "O CPF ou CNPJ é obrigatório.";
  public const string ClienteCpfCnpjInvalido = "O CPF ou CNPJ informado é inválido.";
  public const string ClienteObservacaoMaximoCaracteres = "A observação deve possuir no máximo 500 caracteres.";
  public const string ClienteErroComunicacaoBackend = "Erro ao comunicar com a API de backend.";
}
