using System;
using System.Collections.Generic;

namespace BlueCleanApi.Models.BlueCleanDb;

public partial class Pagamento
{
    public int PagamentoId { get; set; }

    public int TransacaoId { get; set; }

    public int MetodoPagamentoId { get; set; }

    public int StatusPagamentoId { get; set; }

    public decimal Valor { get; set; }

    public string? Provedor { get; set; }

    public string? IdentificadorExterno { get; set; }

    public string? CodigoPixCopiaCola { get; set; }

    public string? QrCodeBase64 { get; set; }

    public DateTime? DataPagamento { get; set; }

    public DateTime? DataExpiracao { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAtualizacao { get; set; }

    public virtual MetodoPagamento MetodoPagamento { get; set; } = null!;

    public virtual StatusPagamento StatusPagamento { get; set; } = null!;

    public virtual Transacao Transacao { get; set; } = null!;
}
