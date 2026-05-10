using System;
using System.Collections.Generic;

namespace BlueCleanApi.Models.BlueCleanDb;

public partial class Lavanderium
{
    public int LavanderiaId { get; set; }

    public string Nome { get; set; } = null!;

    public string? Cnpj { get; set; }

    public string? Telefone { get; set; }

    public string? Email { get; set; }

    public bool Ativa { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAtualizacao { get; set; }

    public virtual ICollection<Ciclo> Ciclos { get; set; } = new List<Ciclo>();

    public virtual ICollection<GatewayLocal> GatewayLocals { get; set; } = new List<GatewayLocal>();

    public virtual ICollection<LavanderiaEndereco> LavanderiaEnderecos { get; set; } = new List<LavanderiaEndereco>();

    public virtual ICollection<MaquinaUso> MaquinaUsos { get; set; } = new List<MaquinaUso>();

    public virtual ICollection<Maquina> Maquinas { get; set; } = new List<Maquina>();

    public virtual ICollection<Preco> Precos { get; set; } = new List<Preco>();

    public virtual ICollection<Transacao> Transacaos { get; set; } = new List<Transacao>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
