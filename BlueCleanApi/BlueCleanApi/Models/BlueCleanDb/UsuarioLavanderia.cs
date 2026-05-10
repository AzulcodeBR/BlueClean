using System;
using System.Collections.Generic;

namespace BlueCleanApi.Models.BlueCleanDb;

public partial class UsuarioLavanderia
{
    public int UsuarioLavanderiaId { get; set; }

    public int UsuarioId { get; set; }

    public int LavanderiaId { get; set; }

    public virtual Lavanderia Lavanderia { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
