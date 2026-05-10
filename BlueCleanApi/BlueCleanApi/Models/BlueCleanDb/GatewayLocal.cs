using System;
using System.Collections.Generic;

namespace BlueCleanApi.Models.BlueCleanDb;

public partial class GatewayLocal
{
    public int GatewayLocalId { get; set; }

    public int LavanderiaId { get; set; }

    public string Codigo { get; set; } = null!;

    public string? Nome { get; set; }

    public string? Descricao { get; set; }

    public string IdentificadorDispositivo { get; set; } = null!;

    public string? VersaoFirmware { get; set; }

    public string? VersaoAplicacao { get; set; }

    public string? IpLocal { get; set; }

    public string? IpPublico { get; set; }

    public string? MacAddress { get; set; }

    public int StatusGatewayId { get; set; }

    public DateTime? UltimaComunicacao { get; set; }

    public string? TokenAutenticacaoHash { get; set; }

    public string? ChavePublica { get; set; }

    public bool Ativo { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAtualizacao { get; set; }

    public virtual Lavanderia Lavanderia { get; set; } = null!;

    public virtual ICollection<Maquina> Maquinas { get; set; } = new List<Maquina>();

    public virtual StatusGateway StatusGateway { get; set; } = null!;
}
