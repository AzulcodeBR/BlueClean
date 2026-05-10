using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BlueCleanApi.Models.BlueCleanDb;

public partial class LavanderiaContext : DbContext
{
    public LavanderiaContext()
    {
    }

    public LavanderiaContext(DbContextOptions<LavanderiaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Auditoria> Auditoria { get; set; }

    public virtual DbSet<Ciclo> Ciclo { get; set; }

    public virtual DbSet<Cliente> Cliente { get; set; }

    public virtual DbSet<ClienteHistorico> ClienteHistorico { get; set; }

    public virtual DbSet<Estado> Estado { get; set; }

    public virtual DbSet<GatewayLocal> GatewayLocal { get; set; }

    public virtual DbSet<IpBloqueio> IpBloqueio { get; set; }

    public virtual DbSet<LavanderiaEndereco> LavanderiaEndereco { get; set; }

    public virtual DbSet<Lavanderia> Lavanderia { get; set; }

    public virtual DbSet<Manutencao> Manutencao { get; set; }

    public virtual DbSet<Maquina> Maquina { get; set; }

    public virtual DbSet<MaquinaAvaliacaoUso> MaquinaAvaliacaoUso { get; set; }

    public virtual DbSet<MaquinaComando> MaquinaComando { get; set; }

    public virtual DbSet<MaquinaEvento> MaquinaEvento { get; set; }

    public virtual DbSet<MaquinaUso> MaquinaUso { get; set; }

    public virtual DbSet<MetodoPagamento> MetodoPagamento { get; set; }

    public virtual DbSet<Municipio> Municipio { get; set; }

    public virtual DbSet<Notificacao> Notificacao { get; set; }

    public virtual DbSet<Pagamento> Pagamento { get; set; }

    public virtual DbSet<PerfilUsuario> PerfilUsuario { get; set; }

    public virtual DbSet<Preco> Preco { get; set; }

    public virtual DbSet<StatusCliente> StatusCliente { get; set; }

    public virtual DbSet<StatusComando> StatusComando { get; set; }

    public virtual DbSet<StatusGateway> StatusGateway { get; set; }

    public virtual DbSet<StatusManutencao> StatusManutencao { get; set; }

    public virtual DbSet<StatusMaquina> StatusMaquina { get; set; }

    public virtual DbSet<StatusPagamento> StatusPagamento { get; set; }

    public virtual DbSet<StatusTransacao> StatusTransacao { get; set; }

    public virtual DbSet<StatusUsuarioGerencial> StatusUsuarioGerencial { get; set; }

    public virtual DbSet<TipoMaquina> TipoMaquina { get; set; }

    public virtual DbSet<Transacao> Transacao { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    public virtual DbSet<UsuarioHistorico> UsuarioHistorico { get; set; }

    public virtual DbSet<UsuarioLavanderia> UsuarioLavanderia { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // A connection string é configurada via Program.cs usando DI
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Auditoria>(entity =>
        {
            entity.HasKey(e => e.AuditoriaId).HasName("PK__Auditori__095694C3332C128E");

            entity.Property(e => e.Acao)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.DataCadastro).HasColumnType("datetime");
            entity.Property(e => e.Descricao)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Entidade)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IpOrigem)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserAgent)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.ValorAnterior).IsUnicode(false);
            entity.Property(e => e.ValorNovo).IsUnicode(false);

            entity.HasOne(d => d.Cliente).WithMany(p => p.Auditoria)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("FK_Auditoria_Cliente");

            entity.HasOne(d => d.UsuarioGerencial).WithMany(p => p.Auditoria)
                .HasForeignKey(d => d.UsuarioGerencialId)
                .HasConstraintName("FK_Auditoria_UsuarioGerencial");
        });

        modelBuilder.Entity<Ciclo>(entity =>
        {
            entity.HasKey(e => e.CicloId).HasName("PK__Ciclo__C99E4490F56A1B09");

            entity.ToTable("Ciclo");

            entity.Property(e => e.Ativo).HasDefaultValue(true, "DF_Ciclo_Ativo");
            entity.Property(e => e.DataAtualizacao).HasColumnType("datetime");
            entity.Property(e => e.DataCadastro).HasColumnType("datetime");
            entity.Property(e => e.Descricao)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.NivelAgua)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Lavanderia).WithMany(p => p.Ciclos)
                .HasForeignKey(d => d.LavanderiaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ciclo_Lavanderia");

            entity.HasOne(d => d.TipoMaquina).WithMany(p => p.Ciclos)
                .HasForeignKey(d => d.TipoMaquinaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ciclo_TipoMaquina");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.ClienteId).HasName("PK__Cliente__71ABD0871F43717C");

            entity.ToTable("Cliente");

            entity.HasIndex(e => e.StatusClienteId, "IX_Cliente_StatusClienteId");

            entity.HasIndex(e => e.CpfCnpj, "UX_Cliente_CpfCnpj").IsUnique();

            entity.HasIndex(e => e.Email, "UX_Cliente_Email").IsUnique();

            entity.Property(e => e.CpfCnpj)
                .HasMaxLength(14)
                .IsUnicode(false);
            entity.Property(e => e.DataAtualizacao).HasColumnType("datetime");
            entity.Property(e => e.DataCadastro).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Observacao)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Senha).IsUnicode(false);
            entity.Property(e => e.Telefone)
                .HasMaxLength(11)
                .IsUnicode(false);

            entity.HasOne(d => d.StatusCliente).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.StatusClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cliente_StatusCliente");
        });

        modelBuilder.Entity<ClienteHistorico>(entity =>
        {
            entity.HasKey(e => e.ClienteHistoricoLoginId).HasName("PK__ClienteH__44FBA40EAC72529B");

            entity.ToTable("ClienteHistorico");

            entity.HasIndex(e => new { e.ClienteId, e.DataLogin }, "IX_ClienteHistoricoLogin_ClienteId_DataLogin").IsDescending(false, true);

            entity.Property(e => e.DataLogin)
                .HasDefaultValueSql("(getdate())", "DF_ClienteHistoricoLogin_DataLogin")
                .HasColumnType("datetime");
            entity.Property(e => e.IpOrigem)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.JwtToken)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Navegador)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SistemaOperacional)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UserAgent)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.Cliente).WithMany(p => p.ClienteHistoricos)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClienteHistoricoLogin_Cliente");
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.ToTable("Estado");

            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Sigla)
                .HasMaxLength(2)
                .IsUnicode(false);
        });

        modelBuilder.Entity<GatewayLocal>(entity =>
        {
            entity.HasKey(e => e.GatewayLocalId).HasName("PK__GatewayL__40280F5C1A680774");

            entity.ToTable("GatewayLocal");

            entity.Property(e => e.Ativo).HasDefaultValue(true, "DF_GatewayLocal_Ativo");
            entity.Property(e => e.ChavePublica).IsUnicode(false);
            entity.Property(e => e.Codigo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DataAtualizacao).HasColumnType("datetime");
            entity.Property(e => e.DataCadastro).HasColumnType("datetime");
            entity.Property(e => e.Descricao)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.IdentificadorDispositivo)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.IpLocal)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IpPublico)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MacAddress)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TokenAutenticacaoHash)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UltimaComunicacao).HasColumnType("datetime");
            entity.Property(e => e.VersaoAplicacao)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.VersaoFirmware)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Lavanderia).WithMany(p => p.GatewayLocals)
                .HasForeignKey(d => d.LavanderiaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GatewayLocal_Lavanderia");

            entity.HasOne(d => d.StatusGateway).WithMany(p => p.GatewayLocals)
                .HasForeignKey(d => d.StatusGatewayId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GatewayLocal_StatusGateway");
        });

        modelBuilder.Entity<IpBloqueio>(entity =>
        {
            entity.ToTable("IpBloqueio");

            entity.Property(e => e.DataBloqueio).HasColumnType("datetime");
            entity.Property(e => e.DataExpiracao).HasColumnType("datetime");
            entity.Property(e => e.Ip)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Navegador)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.SistemaOperacional)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UserAgent)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<LavanderiaEndereco>(entity =>
        {
            entity.HasKey(e => e.LavanderiaEnderecoId).HasName("PK__Lavander__1744DF82431905DD");

            entity.ToTable("LavanderiaEndereco");

            entity.HasIndex(e => e.LavanderiaId, "IX_LavanderiaEndereco_LavanderiaId");

            entity.Property(e => e.Bairro)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Cep)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.Complemento)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Logradouro)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Numero)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Municipio).WithMany(p => p.LavanderiaEnderecos)
                .HasForeignKey(d => d.MunicipioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LavanderiaEndereco_Lavanderia");
        });

        modelBuilder.Entity<Lavanderia>(entity =>
        {
            entity.HasKey(e => e.LavanderiaId).HasName("PK__Lavander__C4446E664208718C");

            entity.Property(e => e.Ativa).HasDefaultValue(true, "DF_Lavanderia_Ativa");
            entity.Property(e => e.Cnpj)
                .HasMaxLength(14)
                .IsUnicode(false);
            entity.Property(e => e.DataAtualizacao).HasColumnType("datetime");
            entity.Property(e => e.DataCadastro).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.NomeFantasia)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.RazaoSocial)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Telefone)
                .HasMaxLength(11)
                .IsUnicode(false);
            entity.Property(e => e.WhatsApp)
                .HasMaxLength(11)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Manutencao>(entity =>
        {
            entity.HasKey(e => e.ManutencaoId).HasName("PK__Manutenc__8F43B8F2C863EA0D");

            entity.ToTable("Manutencao");

            entity.Property(e => e.DataAbertura)
                .HasDefaultValueSql("(getdate())", "DF_Manutencao_DataAbertura")
                .HasColumnType("datetime");
            entity.Property(e => e.DataAtualizacao).HasColumnType("datetime");
            entity.Property(e => e.DataCadastro).HasColumnType("datetime");
            entity.Property(e => e.DataConclusao).HasColumnType("datetime");
            entity.Property(e => e.Descricao).IsUnicode(false);
            entity.Property(e => e.Responsavel)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.TipoManutencao)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Maquina).WithMany(p => p.Manutencaos)
                .HasForeignKey(d => d.MaquinaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Manutencao_Maquina");

            entity.HasOne(d => d.StatusManutencao).WithMany(p => p.Manutencaos)
                .HasForeignKey(d => d.StatusManutencaoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Manutencao_StatusManutencao");
        });

        modelBuilder.Entity<Maquina>(entity =>
        {
            entity.HasKey(e => e.MaquinaId).HasName("PK__Maquina__5D47B895DA892199");

            entity.ToTable("Maquina");

            entity.HasIndex(e => e.GatewayLocalId, "IX_Maquina_GatewayLocalId");

            entity.HasIndex(e => e.LavanderiaId, "IX_Maquina_LavanderiaId");

            entity.Property(e => e.Codigo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DataAtualizacao).HasColumnType("datetime");
            entity.Property(e => e.DataCadastro).HasColumnType("datetime");
            entity.Property(e => e.Fabricante)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Modelo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NumeroSerie)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UltimaComunicacao).HasColumnType("datetime");

            entity.HasOne(d => d.GatewayLocal).WithMany(p => p.Maquinas)
                .HasForeignKey(d => d.GatewayLocalId)
                .HasConstraintName("FK_Maquina_GatewayLocal");

            entity.HasOne(d => d.Lavanderia).WithMany(p => p.Maquinas)
                .HasForeignKey(d => d.LavanderiaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Maquina_Lavanderia");

            entity.HasOne(d => d.StatusMaquina).WithMany(p => p.Maquinas)
                .HasForeignKey(d => d.StatusMaquinaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Maquina_StatusMaquina");

            entity.HasOne(d => d.TipoMaquina).WithMany(p => p.Maquinas)
                .HasForeignKey(d => d.TipoMaquinaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Maquina_TipoMaquina");
        });

        modelBuilder.Entity<MaquinaAvaliacaoUso>(entity =>
        {
            entity.HasKey(e => e.MaquinaAvaliacaoUsoId).HasName("PK__Avaliaca__28F15144577E46F1");

            entity.ToTable("MaquinaAvaliacaoUso");

            entity.HasIndex(e => new { e.ClienteId, e.DataCadastro }, "IX_AvaliacaoUsoMaquina_ClienteId_DataCadastro").IsDescending(false, true);

            entity.HasIndex(e => new { e.MaquinaId, e.CicloId }, "IX_AvaliacaoUsoMaquina_MaquinaId_CicloId");

            entity.HasIndex(e => e.UsoMaquinaId, "UK_AvaliacaoUsoMaquina_UsoMaquina").IsUnique();

            entity.Property(e => e.Comentario)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.DataCadastro).HasColumnType("datetime");
            entity.Property(e => e.DataResolucao).HasColumnType("datetime");
            entity.Property(e => e.TipoProblema)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Ciclo).WithMany(p => p.MaquinaAvaliacaoUsos)
                .HasForeignKey(d => d.CicloId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AvaliacaoUsoMaquina_Ciclo");

            entity.HasOne(d => d.Cliente).WithMany(p => p.MaquinaAvaliacaoUsos)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AvaliacaoUsoMaquina_Cliente");

            entity.HasOne(d => d.Maquina).WithMany(p => p.MaquinaAvaliacaoUsos)
                .HasForeignKey(d => d.MaquinaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AvaliacaoUsoMaquina_Maquina");

            entity.HasOne(d => d.UsoMaquina).WithOne(p => p.MaquinaAvaliacaoUso)
                .HasForeignKey<MaquinaAvaliacaoUso>(d => d.UsoMaquinaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AvaliacaoUsoMaquina_UsoMaquina");
        });

        modelBuilder.Entity<MaquinaComando>(entity =>
        {
            entity.HasKey(e => e.MaquinaComandoId).HasName("PK__ComandoM__CEF68A1F8F542F68");

            entity.ToTable("MaquinaComando");

            entity.HasIndex(e => new { e.MaquinaId, e.StatusComandoId }, "IX_ComandoMaquina_MaquinaId_StatusComandoId");

            entity.Property(e => e.DataAtualizacao).HasColumnType("datetime");
            entity.Property(e => e.DataCadastro).HasColumnType("datetime");
            entity.Property(e => e.DataConfirmacao).HasColumnType("datetime");
            entity.Property(e => e.DataEnvio).HasColumnType("datetime");
            entity.Property(e => e.MensagemErro).IsUnicode(false);
            entity.Property(e => e.Payload).IsUnicode(false);
            entity.Property(e => e.TipoComando)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Maquina).WithMany(p => p.MaquinaComandos)
                .HasForeignKey(d => d.MaquinaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ComandoMaquina_Maquina");

            entity.HasOne(d => d.StatusComando).WithMany(p => p.MaquinaComandos)
                .HasForeignKey(d => d.StatusComandoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ComandoMaquina_StatusComando");

            entity.HasOne(d => d.Transacao).WithMany(p => p.MaquinaComandos)
                .HasForeignKey(d => d.TransacaoId)
                .HasConstraintName("FK_ComandoMaquina_Transacao");
        });

        modelBuilder.Entity<MaquinaEvento>(entity =>
        {
            entity.HasKey(e => e.MaquinaEventoId).HasName("PK__EventoMa__85667A217A0B6F30");

            entity.ToTable("MaquinaEvento");

            entity.HasIndex(e => new { e.MaquinaId, e.DataEvento }, "IX_EventoMaquina_MaquinaId_DataEvento").IsDescending(false, true);

            entity.Property(e => e.CodigoErro)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DataCadastro).HasColumnType("datetime");
            entity.Property(e => e.DataEvento).HasColumnType("datetime");
            entity.Property(e => e.Mensagem)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Payload).IsUnicode(false);
            entity.Property(e => e.TipoEvento)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Maquina).WithMany(p => p.MaquinaEventos)
                .HasForeignKey(d => d.MaquinaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventoMaquina_Maquina");

            entity.HasOne(d => d.StatusMaquina).WithMany(p => p.MaquinaEventos)
                .HasForeignKey(d => d.StatusMaquinaId)
                .HasConstraintName("FK_EventoMaquina_StatusMaquina");

            entity.HasOne(d => d.Transacao).WithMany(p => p.MaquinaEventos)
                .HasForeignKey(d => d.TransacaoId)
                .HasConstraintName("FK_EventoMaquina_Transacao");
        });

        modelBuilder.Entity<MaquinaUso>(entity =>
        {
            entity.HasKey(e => e.MaquinaUsoId).HasName("PK__UsoMaqui__E5BAC74083496B2E");

            entity.ToTable("MaquinaUso");

            entity.HasIndex(e => new { e.ClienteId, e.DataInicio }, "IX_UsoMaquina_ClienteId_DataInicio").IsDescending(false, true);

            entity.HasIndex(e => new { e.LavanderiaId, e.DataInicio }, "IX_UsoMaquina_LavanderiaId_DataInicio").IsDescending(false, true);

            entity.HasIndex(e => new { e.MaquinaId, e.DataInicio }, "IX_UsoMaquina_MaquinaId_DataInicio").IsDescending(false, true);

            entity.HasIndex(e => e.TransacaoId, "UX_UsoMaquina_TransacaoId").IsUnique();

            entity.Property(e => e.DataAtualizacao).HasColumnType("datetime");
            entity.Property(e => e.DataCadastro).HasColumnType("datetime");
            entity.Property(e => e.DataFim).HasColumnType("datetime");
            entity.Property(e => e.DataInicio).HasColumnType("datetime");
            entity.Property(e => e.StatusUso)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ValorCobrado).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ValorPago).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Ciclo).WithMany(p => p.MaquinaUsos)
                .HasForeignKey(d => d.CicloId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsoMaquina_Ciclo");

            entity.HasOne(d => d.Cliente).WithMany(p => p.MaquinaUsos)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsoMaquina_Cliente");

            entity.HasOne(d => d.Lavanderia).WithMany(p => p.MaquinaUsos)
                .HasForeignKey(d => d.LavanderiaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsoMaquina_Lavanderia");

            entity.HasOne(d => d.Maquina).WithMany(p => p.MaquinaUsos)
                .HasForeignKey(d => d.MaquinaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsoMaquina_Maquina");

            entity.HasOne(d => d.Transacao).WithOne(p => p.MaquinaUso)
                .HasForeignKey<MaquinaUso>(d => d.TransacaoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsoMaquina_Transacao");
        });

        modelBuilder.Entity<MetodoPagamento>(entity =>
        {
            entity.HasKey(e => e.MetodoPagamentoId).HasName("PK__MetodoPa__5E2C40FE1114458A");

            entity.ToTable("MetodoPagamento");

            entity.Property(e => e.Descricao)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Municipio>(entity =>
        {
            entity.ToTable("Municipio");

            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Estado).WithMany(p => p.Municipios)
                .HasForeignKey(d => d.EstadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Municipio_Estado");
        });

        modelBuilder.Entity<Notificacao>(entity =>
        {
            entity.HasKey(e => e.NotificacaoId).HasName("PK__Notifica__FB9B787CBB2D05C2");

            entity.ToTable("Notificacao");

            entity.Property(e => e.DataCadastro).HasColumnType("datetime");
            entity.Property(e => e.DataEnvio).HasColumnType("datetime");
            entity.Property(e => e.DataLeitura).HasColumnType("datetime");
            entity.Property(e => e.Mensagem)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.TipoNotificacao)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Titulo)
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.HasOne(d => d.Cliente).WithMany(p => p.Notificacaos)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("FK_Notificacao_Cliente");

            entity.HasOne(d => d.UsuarioGerencial).WithMany(p => p.Notificacaos)
                .HasForeignKey(d => d.UsuarioGerencialId)
                .HasConstraintName("FK_Notificacao_UsuarioGerencial");
        });

        modelBuilder.Entity<Pagamento>(entity =>
        {
            entity.HasKey(e => e.PagamentoId).HasName("PK__Pagament__977DE7F373106802");

            entity.ToTable("Pagamento");

            entity.HasIndex(e => e.TransacaoId, "IX_Pagamento_TransacaoId");

            entity.Property(e => e.CodigoPixCopiaCola).IsUnicode(false);
            entity.Property(e => e.DataAtualizacao).HasColumnType("datetime");
            entity.Property(e => e.DataCadastro).HasColumnType("datetime");
            entity.Property(e => e.DataExpiracao).HasColumnType("datetime");
            entity.Property(e => e.DataPagamento).HasColumnType("datetime");
            entity.Property(e => e.IdentificadorExterno)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Provedor)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.QrCodeBase64).IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.MetodoPagamento).WithMany(p => p.Pagamentos)
                .HasForeignKey(d => d.MetodoPagamentoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pagamento_MetodoPagamento");

            entity.HasOne(d => d.StatusPagamento).WithMany(p => p.Pagamentos)
                .HasForeignKey(d => d.StatusPagamentoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pagamento_StatusPagamento");

            entity.HasOne(d => d.Transacao).WithMany(p => p.Pagamentos)
                .HasForeignKey(d => d.TransacaoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pagamento_Transacao");
        });

        modelBuilder.Entity<PerfilUsuario>(entity =>
        {
            entity.HasKey(e => e.PerfilUsuarioId).HasName("PK__PerfilUs__63500838FB7447EA");

            entity.ToTable("PerfilUsuario");

            entity.Property(e => e.Descricao)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Preco>(entity =>
        {
            entity.HasKey(e => e.PrecoId).HasName("PK__Preco__0C5FF1F0442FFE2A");

            entity.ToTable("Preco");

            entity.Property(e => e.Ativo).HasDefaultValue(true, "DF_Preco_Ativo");
            entity.Property(e => e.DataCadastro).HasColumnType("datetime");
            entity.Property(e => e.FimVigencia).HasColumnType("datetime");
            entity.Property(e => e.InicioVigencia).HasColumnType("datetime");
            entity.Property(e => e.Valor).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Ciclo).WithMany(p => p.Precos)
                .HasForeignKey(d => d.CicloId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Preco_Ciclo");

            entity.HasOne(d => d.Lavanderia).WithMany(p => p.Precos)
                .HasForeignKey(d => d.LavanderiaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Preco_Lavanderia");
        });

        modelBuilder.Entity<StatusCliente>(entity =>
        {
            entity.HasKey(e => e.StatusClienteId).HasName("PK__StatusCl__6C361235860E7EA7");

            entity.ToTable("StatusCliente");

            entity.Property(e => e.StatusClienteId).ValueGeneratedNever();
            entity.Property(e => e.Descricao)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<StatusComando>(entity =>
        {
            entity.HasKey(e => e.StatusComandoId).HasName("PK__StatusCo__D8DB29C4E7E94C8D");

            entity.ToTable("StatusComando");

            entity.Property(e => e.Descricao)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<StatusGateway>(entity =>
        {
            entity.HasKey(e => e.StatusGatewayId).HasName("PK__StatusGa__DE52008EF2A20C9B");

            entity.ToTable("StatusGateway");

            entity.Property(e => e.Descricao)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<StatusManutencao>(entity =>
        {
            entity.HasKey(e => e.StatusManutencaoId).HasName("PK__StatusMa__CFADE678F0682005");

            entity.ToTable("StatusManutencao");

            entity.Property(e => e.Descricao)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<StatusMaquina>(entity =>
        {
            entity.HasKey(e => e.StatusMaquinaId).HasName("PK__StatusMa__B6644D053C95928C");

            entity.ToTable("StatusMaquina");

            entity.Property(e => e.Descricao)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<StatusPagamento>(entity =>
        {
            entity.HasKey(e => e.StatusPagamentoId).HasName("PK__StatusPa__8112C2A517FB9BC3");

            entity.ToTable("StatusPagamento");

            entity.Property(e => e.Descricao)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<StatusTransacao>(entity =>
        {
            entity.HasKey(e => e.StatusTransacaoId).HasName("PK__StatusTr__CA601BF8D3959B39");

            entity.ToTable("StatusTransacao");

            entity.Property(e => e.Descricao)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<StatusUsuarioGerencial>(entity =>
        {
            entity.HasKey(e => e.StatusUsuarioGerencialId).HasName("PK__StatusUs__59305662595DA879");

            entity.ToTable("StatusUsuarioGerencial");

            entity.Property(e => e.StatusUsuarioGerencialId).ValueGeneratedNever();
            entity.Property(e => e.Descricao)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoMaquina>(entity =>
        {
            entity.HasKey(e => e.TipoMaquinaId).HasName("PK__TipoMaqu__9E83D8D371CB4A97");

            entity.ToTable("TipoMaquina");

            entity.Property(e => e.Descricao)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Transacao>(entity =>
        {
            entity.HasKey(e => e.TransacaoId).HasName("PK__Transaca__5582353064A7273F");

            entity.ToTable("Transacao");

            entity.HasIndex(e => e.ClienteId, "IX_Transacao_ClienteId");

            entity.HasIndex(e => e.MaquinaId, "IX_Transacao_MaquinaId");

            entity.HasIndex(e => e.StatusTransacaoId, "IX_Transacao_StatusTransacaoId");

            entity.Property(e => e.DataAtualizacao).HasColumnType("datetime");
            entity.Property(e => e.DataCadastro).HasColumnType("datetime");
            entity.Property(e => e.DataFim).HasColumnType("datetime");
            entity.Property(e => e.DataInicio).HasColumnType("datetime");
            entity.Property(e => e.Valor).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Ciclo).WithMany(p => p.Transacaos)
                .HasForeignKey(d => d.CicloId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transacao_Ciclo");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Transacaos)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transacao_Cliente");

            entity.HasOne(d => d.Lavanderia).WithMany(p => p.Transacaos)
                .HasForeignKey(d => d.LavanderiaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transacao_Lavanderia");

            entity.HasOne(d => d.Maquina).WithMany(p => p.Transacaos)
                .HasForeignKey(d => d.MaquinaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transacao_Maquina");

            entity.HasOne(d => d.StatusTransacao).WithMany(p => p.Transacaos)
                .HasForeignKey(d => d.StatusTransacaoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transacao_StatusTransacao");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__UsuarioG__59D1BFC9394F923F");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.StatusUsuarioGerencialId, "IX_UsuarioGerencial_StatusUsuarioGerencialId");

            entity.HasIndex(e => e.Cpf, "UX_UsuarioGerencial_Cpf").IsUnique();

            entity.Property(e => e.Cpf)
                .HasMaxLength(11)
                .IsUnicode(false);
            entity.Property(e => e.DataAtualizacao).HasColumnType("datetime");
            entity.Property(e => e.DataCadastro).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Observacao)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Senha).IsUnicode(false);
            entity.Property(e => e.Telefone)
                .HasMaxLength(11)
                .IsUnicode(false);

            entity.HasOne(d => d.PerfilUsuario).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.PerfilUsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsuarioGerencial_PerfilUsuario");

            entity.HasOne(d => d.StatusUsuarioGerencial).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.StatusUsuarioGerencialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsuarioGerencial_StatusUsuarioGerencial");
        });

        modelBuilder.Entity<UsuarioHistorico>(entity =>
        {
            entity.HasKey(e => e.UsuarioGerencialHistoricoLoginId).HasName("PK__UsuarioG__7AB0879E8BE146A5");

            entity.ToTable("UsuarioHistorico");

            entity.HasIndex(e => new { e.UsuarioGerencialId, e.DataLogin }, "IX_UsuarioGerencialHistoricoLogin_UsuarioGerencialId_DataLogin").IsDescending(false, true);

            entity.Property(e => e.DataLogin)
                .HasDefaultValueSql("(getdate())", "DF_UsuarioGerencialHistoricoLogin_DataLogin")
                .HasColumnType("datetime");
            entity.Property(e => e.IpOrigem)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.JwtToken)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Navegador)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SistemaOperacional)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UserAgent)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.UsuarioGerencial).WithMany(p => p.UsuarioHistoricos)
                .HasForeignKey(d => d.UsuarioGerencialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsuarioGerencialHistoricoLogin_UsuarioGerencial");
        });

        modelBuilder.Entity<UsuarioLavanderia>(entity =>
        {
            entity.HasKey(e => e.UsuarioLavanderiaId);

            entity.HasOne(d => d.Lavanderia).WithMany(p => p.UsuarioLavanderia)
                .HasForeignKey(d => d.LavanderiaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsuarioLavanderia_Lavanderia");

            entity.HasOne(d => d.Usuario).WithMany(p => p.UsuarioLavanderia)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsuarioLavanderia_Usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
