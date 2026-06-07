using System;
using System.Collections.Generic;

namespace BlueCleanApi.Models.BlueCleanDb;

public partial class TipoMaquina
{
    public int TipoMaquinaId { get; set; }

    public string Descricao { get; set; } = null!;

    public virtual ICollection<Ciclo> Ciclo { get; set; } = new List<Ciclo>();

    public virtual ICollection<Maquina> Maquina { get; set; } = new List<Maquina>();
}

