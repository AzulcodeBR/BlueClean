using System;
using System.Collections.Generic;

namespace BlueCleanApi.Models.BlueCleanDb;

public partial class StatusManutencao
{
    public int StatusManutencaoId { get; set; }

    public string Descricao { get; set; } = null!;

    public virtual ICollection<Manutencao> Manutencao { get; set; } = new List<Manutencao>();
}

