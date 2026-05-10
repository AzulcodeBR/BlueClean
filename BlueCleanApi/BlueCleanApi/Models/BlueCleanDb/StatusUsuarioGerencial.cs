using System;
using System.Collections.Generic;

namespace BlueCleanApi.Models.BlueCleanDb;

public partial class StatusUsuarioGerencial
{
    public int StatusUsuarioGerencialId { get; set; }

    public string Descricao { get; set; } = null!;

    public virtual ICollection<Usuario> Usuario { get; set; } = new List<Usuario>();
}

