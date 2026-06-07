using System;
using System.Collections.Generic;

namespace BlueCleanApi.Models.BlueCleanDb;

public partial class UsuarioHistorico
{
    public int UsuarioGerencialHistoricoLoginId { get; set; }

    public int UsuarioGerencialId { get; set; }

    public string JwtToken { get; set; } = null!;

    public DateTime DataLogin { get; set; }

    public string? IpOrigem { get; set; }

    public string? UserAgent { get; set; }

    public string? SistemaOperacional { get; set; }

    public string? Navegador { get; set; }

    public virtual Usuario UsuarioGerencial { get; set; } = null!;
}
