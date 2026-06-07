using System;
using System.Collections.Generic;

namespace BlueCleanApi.Models.BlueCleanDb;

public partial class MaquinaComando
{
    public int MaquinaComandoId { get; set; }

    public int? TransacaoId { get; set; }

    public int MaquinaId { get; set; }

    public string TipoComando { get; set; } = null!;

    public string? Payload { get; set; }

    public int StatusComandoId { get; set; }

    public int Tentativas { get; set; }

    public string? MensagemErro { get; set; }

    public DateTime? DataEnvio { get; set; }

    public DateTime? DataConfirmacao { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAtualizacao { get; set; }

    public virtual Maquina Maquina { get; set; } = null!;

    public virtual StatusComando StatusComando { get; set; } = null!;

    public virtual Transacao? Transacao { get; set; }
}
