using System;
using System.Collections.Generic;

namespace BlueCleanApi.Models.BlueCleanDb;

public partial class Municipio
{
    public int MunicipioId { get; set; }

    public int EstadoId { get; set; }

    public int IbgeId { get; set; }

    public string Nome { get; set; }
    
    public virtual Estado Estado { get; set; } = null!;

    public virtual ICollection<LavanderiaEndereco> LavanderiaEndereco { get; set; } = new List<LavanderiaEndereco>();
}

