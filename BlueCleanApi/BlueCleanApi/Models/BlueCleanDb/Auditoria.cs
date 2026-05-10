using System;
using System.Collections.Generic;

namespace BlueCleanApi.Models.BlueCleanDb;

public partial class Auditoria
{
    public int AuditoriaId { get; set; }

    public int? UsuarioGerencialId { get; set; }

    public int? ClienteId { get; set; }

    public string Entidade { get; set; } = null!;

    public int? EntidadeId { get; set; }

    public string Acao { get; set; } = null!;

    public string? Descricao { get; set; }

    public string? ValorAnterior { get; set; }

    public string? ValorNovo { get; set; }

    public string? IpOrigem { get; set; }

    public string? UserAgent { get; set; }

    public DateTime DataCadastro { get; set; }

    public virtual Cliente? Cliente { get; set; }

    public virtual Usuario? UsuarioGerencial { get; set; }
}
