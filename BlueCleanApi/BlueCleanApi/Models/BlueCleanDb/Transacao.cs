using System;
using System.Collections.Generic;

namespace BlueCleanApi.Models.BlueCleanDb;

public partial class Transacao
{
    public int TransacaoId { get; set; }

    public int ClienteId { get; set; }

    public int LavanderiaId { get; set; }

    public int MaquinaId { get; set; }

    public int CicloId { get; set; }

    public decimal Valor { get; set; }

    public int StatusTransacaoId { get; set; }

    public DateTime? DataInicio { get; set; }

    public DateTime? DataFim { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAtualizacao { get; set; }

    public virtual Ciclo Ciclo { get; set; } = null!;

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual Lavanderium Lavanderia { get; set; } = null!;

    public virtual Maquina Maquina { get; set; } = null!;

    public virtual ICollection<MaquinaComando> MaquinaComandos { get; set; } = new List<MaquinaComando>();

    public virtual ICollection<MaquinaEvento> MaquinaEventos { get; set; } = new List<MaquinaEvento>();

    public virtual MaquinaUso? MaquinaUso { get; set; }

    public virtual ICollection<Pagamento> Pagamentos { get; set; } = new List<Pagamento>();

    public virtual StatusTransacao StatusTransacao { get; set; } = null!;
}
