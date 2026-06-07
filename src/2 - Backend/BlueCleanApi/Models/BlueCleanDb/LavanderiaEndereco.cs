using System;
using System.Collections.Generic;

namespace BlueCleanApi.Models.BlueCleanDb;

public partial class LavanderiaEndereco
{
    public int LavanderiaEnderecoId { get; set; }

    public int LavanderiaId { get; set; }

    public string Cep { get; set; } = null!;

    public string Logradouro { get; set; } = null!;

    public string Numero { get; set; } = null!;

    public string Complemento { get; set; } = null!;

    public string Bairro { get; set; } = null!;

    public int MunicipioId { get; set; }

    public virtual Municipio Municipio { get; set; } = null!;
}
