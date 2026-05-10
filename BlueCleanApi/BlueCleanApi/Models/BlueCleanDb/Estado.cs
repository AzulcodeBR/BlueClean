using System;
using System.Collections.Generic;

namespace BlueCleanApi.Models.BlueCleanDb;

public partial class Estado
{
    public int EstadoId { get; set; }

    public string Nome { get; set; } = null!;

    public string Sigla { get; set; } = null!;

    public virtual ICollection<Municipio> Municipios { get; set; } = new List<Municipio>();
}
