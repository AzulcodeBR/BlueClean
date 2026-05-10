using System;
using System.Collections.Generic;

namespace BlueCleanApi.Models.BlueCleanDb;

public partial class Lavanderia
{
    public int LavanderiaId { get; set; }

    public string NomeFantasia { get; set; } = null!;

    public string RazaoSocial { get; set; } = null!;

    public string Cnpj { get; set; } = null!;

    public string Telefone { get; set; } = null!;

    public string WhatsApp { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool Ativa { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime DataAtualizacao { get; set; }

    public virtual ICollection<Ciclo> Ciclo { get; set; } = new List<Ciclo>();

    public virtual ICollection<GatewayLocal> GatewayLocal { get; set; } = new List<GatewayLocal>();

    public virtual ICollection<MaquinaUso> MaquinaUso { get; set; } = new List<MaquinaUso>();

    public virtual ICollection<Maquina> Maquina { get; set; } = new List<Maquina>();

    public virtual ICollection<Preco> Preco { get; set; } = new List<Preco>();

    public virtual ICollection<Transacao> Transacao { get; set; } = new List<Transacao>();

    public virtual ICollection<UsuarioLavanderia> UsuarioLavanderia { get; set; } = new List<UsuarioLavanderia>();
}

