using System;
using System.Collections.Generic;

namespace BlueCleanApi.Models.BlueCleanDb;

public partial class Ciclo
{
    public int CicloId { get; set; }

    public int LavanderiaId { get; set; }

    public int TipoMaquinaId { get; set; }

    public string Nome { get; set; } = null!;

    public string? Descricao { get; set; }

    public int DuracaoMinutos { get; set; }

    public int? Temperatura { get; set; }

    public string? NivelAgua { get; set; }

    public bool Ativo { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAtualizacao { get; set; }

    public virtual Lavanderia Lavanderia { get; set; } = null!;

    public virtual ICollection<MaquinaAvaliacaoUso> MaquinaAvaliacaoUso { get; set; } = new List<MaquinaAvaliacaoUso>();

    public virtual ICollection<MaquinaUso> MaquinaUso { get; set; } = new List<MaquinaUso>();

    public virtual ICollection<Preco> Preco { get; set; } = new List<Preco>();

    public virtual TipoMaquina TipoMaquina { get; set; } = null!;

    public virtual ICollection<Transacao> Transacao { get; set; } = new List<Transacao>();
}

