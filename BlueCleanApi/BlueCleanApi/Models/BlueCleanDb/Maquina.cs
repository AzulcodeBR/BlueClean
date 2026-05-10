using System;
using System.Collections.Generic;

namespace BlueCleanApi.Models.BlueCleanDb;

public partial class Maquina
{
    public int MaquinaId { get; set; }

    public int LavanderiaId { get; set; }

    public int? GatewayLocalId { get; set; }

    public string Codigo { get; set; } = null!;

    public string? Nome { get; set; }

    public int TipoMaquinaId { get; set; }

    public string? Modelo { get; set; }

    public string? Fabricante { get; set; }

    public string? NumeroSerie { get; set; }

    public int StatusMaquinaId { get; set; }

    public int? TempoRestanteMinutos { get; set; }

    public DateTime? UltimaComunicacao { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAtualizacao { get; set; }

    public virtual GatewayLocal? GatewayLocal { get; set; }

    public virtual Lavanderia Lavanderia { get; set; } = null!;

    public virtual ICollection<Manutencao> Manutencaos { get; set; } = new List<Manutencao>();

    public virtual ICollection<MaquinaAvaliacaoUso> MaquinaAvaliacaoUsos { get; set; } = new List<MaquinaAvaliacaoUso>();

    public virtual ICollection<MaquinaComando> MaquinaComandos { get; set; } = new List<MaquinaComando>();

    public virtual ICollection<MaquinaEvento> MaquinaEventos { get; set; } = new List<MaquinaEvento>();

    public virtual ICollection<MaquinaUso> MaquinaUsos { get; set; } = new List<MaquinaUso>();

    public virtual StatusMaquina StatusMaquina { get; set; } = null!;

    public virtual TipoMaquina TipoMaquina { get; set; } = null!;

    public virtual ICollection<Transacao> Transacaos { get; set; } = new List<Transacao>();
}
