using System;
using System.Collections.Generic;

namespace BlueCleanApi.Models.BlueCleanDb;

public partial class Manutencao
{
    public int ManutencaoId { get; set; }

    public int MaquinaId { get; set; }

    public string TipoManutencao { get; set; } = null!;

    public string? Descricao { get; set; }

    public int StatusManutencaoId { get; set; }

    public string? Responsavel { get; set; }

    public DateTime DataAbertura { get; set; }

    public DateTime? DataConclusao { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAtualizacao { get; set; }

    public virtual Maquina Maquina { get; set; } = null!;

    public virtual StatusManutencao StatusManutencao { get; set; } = null!;
}
