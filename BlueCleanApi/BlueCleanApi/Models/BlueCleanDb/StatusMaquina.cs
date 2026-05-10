using System;
using System.Collections.Generic;

namespace BlueCleanApi.Models.BlueCleanDb;

public partial class StatusMaquina
{
    public int StatusMaquinaId { get; set; }

    public string Descricao { get; set; } = null!;

    public virtual ICollection<MaquinaEvento> MaquinaEventos { get; set; } = new List<MaquinaEvento>();

    public virtual ICollection<Maquina> Maquinas { get; set; } = new List<Maquina>();
}
