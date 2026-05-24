namespace BlueCleanApi.Extensions.Dtos;

/// <summary>
/// Dados para cadastro de cliente.
/// </summary>
public class ClienteCadastroRequestDto
{
  public string Nome { get; set; } = string.Empty;

  public string Email { get; set; } = string.Empty;

  public string? Telefone { get; set; }

  public string CpfCnpj { get; set; } = string.Empty;

  public string Senha { get; set; } = string.Empty;

  public string? Observacao { get; set; }
}
