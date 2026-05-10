using System;
using System.Collections.Generic;

namespace BlueCleanApi.Models.BlueCleanDb;

public partial class MetodoPagamento
{
    public int MetodoPagamentoId { get; set; }

    public string Descricao { get; set; } = null!;

    public virtual ICollection<Pagamento> Pagamento { get; set; } = new List<Pagamento>();
}

