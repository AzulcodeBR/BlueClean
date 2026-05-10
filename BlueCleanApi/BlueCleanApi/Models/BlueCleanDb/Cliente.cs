using System;
using System.Collections.Generic;

namespace BlueCleanApi.Models.BlueCleanDb;

public partial class Cliente
{
    public int ClienteId { get; set; }

    public string Nome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Telefone { get; set; }

    public string CpfCnpj { get; set; } = null!;

    public string Senha { get; set; } = null!;

    public int StatusClienteId { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAtualizacao { get; set; }

    public string? Observacao { get; set; }

    public virtual ICollection<Auditoria> Auditoria { get; set; } = new List<Auditoria>();

    public virtual ICollection<ClienteHistorico> ClienteHistoricos { get; set; } = new List<ClienteHistorico>();

    public virtual ICollection<MaquinaAvaliacaoUso> MaquinaAvaliacaoUsos { get; set; } = new List<MaquinaAvaliacaoUso>();

    public virtual ICollection<MaquinaUso> MaquinaUsos { get; set; } = new List<MaquinaUso>();

    public virtual ICollection<Notificacao> Notificacaos { get; set; } = new List<Notificacao>();

    public virtual StatusCliente StatusCliente { get; set; } = null!;

    public virtual ICollection<Transacao> Transacaos { get; set; } = new List<Transacao>();
}
