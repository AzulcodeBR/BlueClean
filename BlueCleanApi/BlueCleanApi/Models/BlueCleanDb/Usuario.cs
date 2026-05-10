using System;
using System.Collections.Generic;

namespace BlueCleanApi.Models.BlueCleanDb;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string Nome { get; set; } = null!;

    public string Cpf { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Telefone { get; set; } = null!;

    public string Senha { get; set; } = null!;

    public int PerfilUsuarioId { get; set; }

    public int StatusUsuarioGerencialId { get; set; }

    public string? Observacao { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime DataAtualizacao { get; set; }

    public virtual ICollection<Auditoria> Auditoria { get; set; } = new List<Auditoria>();

    public virtual ICollection<Notificacao> Notificacao { get; set; } = new List<Notificacao>();

    public virtual PerfilUsuario PerfilUsuario { get; set; } = null!;

    public virtual StatusUsuarioGerencial StatusUsuarioGerencial { get; set; } = null!;

    public virtual ICollection<UsuarioHistorico> UsuarioHistorico { get; set; } = new List<UsuarioHistorico>();

    public virtual ICollection<UsuarioLavanderia> UsuarioLavanderia { get; set; } = new List<UsuarioLavanderia>();
}

