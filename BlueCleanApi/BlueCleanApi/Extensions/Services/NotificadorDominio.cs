using BlueCleanApi.Extensions.Interfaces;

namespace BlueCleanApi.Extensions.Services
{
    public class NotificadorDominio : INotificadorDominio
    {
        private readonly List<string> _notificacoes;

        public NotificadorDominio()
        {
            _notificacoes = [];
        }

        public List<string> ObterNotificacoes()
        {
            return _notificacoes;
        }

        public bool VerificarOperacao()
        {
            return _notificacoes.Count == 0;
        }

        public void AdicionarNotificacao(string notificacao)
        {
            if (string.IsNullOrEmpty(notificacao))
                return;

            _notificacoes.Add(notificacao);
        }

        public void AdicionarNotificacoes(List<string> notificacoes)
        {
            _notificacoes.AddRange(notificacoes);
        }

        public void LimparNotificacoes()
        {
            _notificacoes.Clear();
        }
    }
}
