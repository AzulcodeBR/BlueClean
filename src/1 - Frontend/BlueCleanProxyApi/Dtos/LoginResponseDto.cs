namespace BlueCleanProxyApi.Dtos;

public class LoginResponseDto
{
  public string Token { get; set; } = string.Empty;

  public string NomeUsuario { get; set; } = string.Empty;

  public int TipoLogin { get; set; }

  public DateTime ExpiraEmUtc { get; set; }
}
