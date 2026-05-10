using System;
using System.Collections.Generic;

namespace BlueCleanApi.Models.BlueCleanDb;

public partial class MaquinaAvaliacaoUso
{
    public int MaquinaAvaliacaoUsoId { get; set; }

    public int UsoMaquinaId { get; set; }

    public int ClienteId { get; set; }

    public int MaquinaId { get; set; }

    public int CicloId { get; set; }

    public int NotaGeral { get; set; }

    public int? NotaLimpeza { get; set; }

    public int? NotaFacilidadeUso { get; set; }

    public int? NotaTempo { get; set; }

    public string? Comentario { get; set; }

    public bool ProblemaRelatado { get; set; }

    public string? TipoProblema { get; set; }

    public bool Resolvido { get; set; }

    public DateTime? DataResolucao { get; set; }

    public DateTime DataCadastro { get; set; }

    public virtual Ciclo Ciclo { get; set; } = null!;

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual Maquina Maquina { get; set; } = null!;

    public virtual MaquinaUso UsoMaquina { get; set; } = null!;
}
