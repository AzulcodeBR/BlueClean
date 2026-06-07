namespace BlueCleanApi.Extensions.Interfaces
{
    public interface INotificadorDominio
    {
        public List<string> ObterNotificacoes();
        public bool VerificarOperacao();
        public void AdicionarNotificacao(string notificacao);
        public void AdicionarNotificacoes(List<string> notificacoes);
        public void LimparNotificacoes();
    }
}
