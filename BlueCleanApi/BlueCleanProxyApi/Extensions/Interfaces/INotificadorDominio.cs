namespace BlueCleanProxyApi.Extensions.Interfaces;

public interface INotificadorDominio
{
  List<string> ObterNotificacoes();
  bool VerificarOperacao();
  void AdicionarNotificacao(string notificacao);
  void AdicionarNotificacoes(List<string> notificacoes);
  void LimparNotificacoes();
}
