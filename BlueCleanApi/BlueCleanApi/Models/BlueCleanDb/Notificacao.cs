using System;
using System.Collections.Generic;

namespace BlueCleanApi.Models.BlueCleanDb;

public partial class Notificacao
{
    public int NotificacaoId { get; set; }

    public int? ClienteId { get; set; }

    public int? UsuarioGerencialId { get; set; }

    public string TipoNotificacao { get; set; } = null!;

    public string Titulo { get; set; } = null!;

    public string Mensagem { get; set; } = null!;

    public bool Lida { get; set; }

    public bool Enviada { get; set; }

    public DateTime? DataEnvio { get; set; }

    public DateTime? DataLeitura { get; set; }

    public DateTime DataCadastro { get; set; }

    public virtual Cliente? Cliente { get; set; }

    public virtual Usuario? UsuarioGerencial { get; set; }
}
