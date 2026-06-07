using System;
using System.Collections.Generic;

namespace BlueCleanApi.Models.BlueCleanDb;

public partial class StatusComando
{
    public int StatusComandoId { get; set; }

    public string Descricao { get; set; } = null!;

    public virtual ICollection<MaquinaComando> MaquinaComando { get; set; } = new List<MaquinaComando>();
}

