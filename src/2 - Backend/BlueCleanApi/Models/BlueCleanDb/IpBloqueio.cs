using System;
using System.Collections.Generic;

namespace BlueCleanApi.Models.BlueCleanDb;

public partial class IpBloqueio
{
    public int IpBloqueioId { get; set; }

    public string Ip { get; set; } = null!;

    public string UserAgent { get; set; } = null!;

    public string Navegador { get; set; } = null!;

    public string SistemaOperacional { get; set; } = null!;

    public DateTime DataBloqueio { get; set; }

    public DateTime DataExpiracao { get; set; }
}
