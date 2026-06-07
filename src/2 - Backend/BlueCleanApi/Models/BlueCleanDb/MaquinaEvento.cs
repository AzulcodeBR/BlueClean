using System;
using System.Collections.Generic;

namespace BlueCleanApi.Models.BlueCleanDb;

public partial class MaquinaEvento
{
    public int MaquinaEventoId { get; set; }

    public int MaquinaId { get; set; }

    public int? TransacaoId { get; set; }

    public string TipoEvento { get; set; } = null!;

    public int? StatusMaquinaId { get; set; }

    public int? TempoRestanteMinutos { get; set; }

    public string? CodigoErro { get; set; }

    public string? Mensagem { get; set; }

    public string? Payload { get; set; }

    public DateTime DataEvento { get; set; }

    public DateTime DataCadastro { get; set; }

    public virtual Maquina Maquina { get; set; } = null!;

    public virtual StatusMaquina? StatusMaquina { get; set; }

    public virtual Transacao? Transacao { get; set; }
}
