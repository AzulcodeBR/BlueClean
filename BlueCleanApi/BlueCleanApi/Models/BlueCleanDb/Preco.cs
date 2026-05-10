using System;
using System.Collections.Generic;

namespace BlueCleanApi.Models.BlueCleanDb;

public partial class Preco
{
    public int PrecoId { get; set; }

    public int LavanderiaId { get; set; }

    public int CicloId { get; set; }

    public decimal Valor { get; set; }

    public DateTime InicioVigencia { get; set; }

    public DateTime? FimVigencia { get; set; }

    public bool Ativo { get; set; }

    public DateTime DataCadastro { get; set; }

    public virtual Ciclo Ciclo { get; set; } = null!;

    public virtual Lavanderium Lavanderia { get; set; } = null!;
}
