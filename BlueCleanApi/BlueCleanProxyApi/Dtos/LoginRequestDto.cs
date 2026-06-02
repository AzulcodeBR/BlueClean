namespace BlueCleanProxyApi.Dtos;

public class LoginRequestDto
{
  public string Identificador { get; set; } = string.Empty;

  public int TipoLogin { get; set; }

  public string Senha { get; set; } = string.Empty;
}
