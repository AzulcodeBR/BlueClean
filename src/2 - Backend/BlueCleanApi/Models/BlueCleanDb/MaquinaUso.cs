using System;
using System.Collections.Generic;

namespace BlueCleanApi.Models.BlueCleanDb;

public partial class MaquinaUso
{
    public int MaquinaUsoId { get; set; }

    public int TransacaoId { get; set; }

    public int ClienteId { get; set; }

    public int LavanderiaId { get; set; }

    public int MaquinaId { get; set; }

    public int CicloId { get; set; }

    public DateTime DataInicio { get; set; }

    public DateTime? DataFim { get; set; }

    public int DuracaoPrevistaMinutos { get; set; }

    public int? DuracaoRealMinutos { get; set; }

    public decimal ValorCobrado { get; set; }

    public decimal? ValorPago { get; set; }

    public string StatusUso { get; set; } = null!;

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAtualizacao { get; set; }

    public virtual Ciclo Ciclo { get; set; } = null!;

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual Lavanderia Lavanderia { get; set; } = null!;

    public virtual Maquina Maquina { get; set; } = null!;

    public virtual MaquinaAvaliacaoUso? MaquinaAvaliacaoUso { get; set; }

    public virtual Transacao Transacao { get; set; } = null!;
}
