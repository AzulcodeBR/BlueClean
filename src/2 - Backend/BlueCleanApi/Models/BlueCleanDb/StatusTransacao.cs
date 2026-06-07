using System;
using System.Collections.Generic;

namespace BlueCleanApi.Models.BlueCleanDb;

public partial class StatusTransacao
{
    public int StatusTransacaoId { get; set; }

    public string Descricao { get; set; } = null!;

    public virtual ICollection<Transacao> Transacao { get; set; } = new List<Transacao>();
}

