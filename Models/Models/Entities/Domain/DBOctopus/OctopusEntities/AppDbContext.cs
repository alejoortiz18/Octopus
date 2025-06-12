using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Models.Entities.Domain.DBOctopus.OctopusEntities;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BackupRegistro> BackupRegistros { get; set; }

    public virtual DbSet<Banco> Bancos { get; set; }

    public virtual DbSet<Comision> Comisions { get; set; }

    public virtual DbSet<ConfiguracionSistema> ConfiguracionSistemas { get; set; }

    public virtual DbSet<EstadoPago> EstadoPagos { get; set; }

    public virtual DbSet<EstadoUsuario> EstadoUsuarios { get; set; }

    public virtual DbSet<HistorialContrasena> HistorialContrasenas { get; set; }

    public virtual DbSet<LogActividad> LogActividads { get; set; }

    public virtual DbSet<Notificacion> Notificacions { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<RedReferido> RedReferidos { get; set; }

    public virtual DbSet<Reporte> Reportes { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Sesion> Sesions { get; set; }

    public virtual DbSet<TipoCuentaBancarium> TipoCuentaBancaria { get; set; }

    public virtual DbSet<TipoDocumento> TipoDocumentos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<VwComisionesPendiente> VwComisionesPendientes { get; set; }

    public virtual DbSet<VwRedReferido> VwRedReferidos { get; set; }

    public virtual DbSet<VwUsuariosPendiente> VwUsuariosPendientes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKALEJO\\SQLEXPRESS;Database=Octopus;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BackupRegistro>(entity =>
        {
            entity.HasKey(e => e.BackupId).HasName("PK__BackupRe__EB9069E281B97C2E");

            entity.ToTable("BackupRegistro");

            entity.Property(e => e.BackupId).HasColumnName("BackupID");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.FechaBackup)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NombreArchivo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Ruta)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.TamanoMb)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("TamanoMB");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Usuario).WithMany(p => p.BackupRegistros)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BackupReg__Usuar__7E37BEF6");
        });

        modelBuilder.Entity<Banco>(entity =>
        {
            entity.HasKey(e => e.BancoId).HasName("PK__Banco__4A8BAC15F5081EF0");

            entity.ToTable("Banco");

            entity.Property(e => e.BancoId).HasColumnName("BancoID");
            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Codigo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Comision>(entity =>
        {
            entity.HasKey(e => e.ComisionId).HasName("PK__Comision__A014A712D11646D7");

            entity.ToTable("Comision");

            entity.HasIndex(e => e.EstadoPagoId, "idx_Comision_Estado");

            entity.HasIndex(e => e.FechaGeneracion, "idx_Comision_Fecha");

            entity.HasIndex(e => e.ReferidoId, "idx_Comision_ReferidoID");

            entity.HasIndex(e => e.UsuarioId, "idx_Comision_UsuarioID");

            entity.Property(e => e.ComisionId).HasColumnName("ComisionID");
            entity.Property(e => e.EstadoPagoId).HasColumnName("EstadoPagoID");
            entity.Property(e => e.FechaGeneracion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaPago).HasColumnType("datetime");
            entity.Property(e => e.Monto).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ReferidoId).HasColumnName("ReferidoID");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.EstadoPago).WithMany(p => p.Comisions)
                .HasForeignKey(d => d.EstadoPagoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comision__Estado__619B8048");

            entity.HasOne(d => d.Referido).WithMany(p => p.ComisionReferidos)
                .HasForeignKey(d => d.ReferidoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comision__Referi__60A75C0F");

            entity.HasOne(d => d.Usuario).WithMany(p => p.ComisionUsuarios)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comision__Usuari__5FB337D6");
        });

        modelBuilder.Entity<ConfiguracionSistema>(entity =>
        {
            entity.HasKey(e => e.ConfiguracionId).HasName("PK__Configur__9B95E0569473C1C2");

            entity.ToTable("ConfiguracionSistema");

            entity.HasIndex(e => e.Clave, "UQ__Configur__E8181E11305558A4").IsUnique();

            entity.Property(e => e.ConfiguracionId).HasColumnName("ConfiguracionID");
            entity.Property(e => e.Clave)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Modificable).HasDefaultValue(true);
            entity.Property(e => e.Valor)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EstadoPago>(entity =>
        {
            entity.HasKey(e => e.EstadoPagoId).HasName("PK__EstadoPa__63AD30BD3C35980B");

            entity.ToTable("EstadoPago");

            entity.Property(e => e.EstadoPagoId).HasColumnName("EstadoPagoID");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EstadoUsuario>(entity =>
        {
            entity.HasKey(e => e.EstadoUsuarioId).HasName("PK__EstadoUs__BAA0F882C6B73FEE");

            entity.ToTable("EstadoUsuario");

            entity.Property(e => e.EstadoUsuarioId).HasColumnName("EstadoUsuarioID");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<HistorialContrasena>(entity =>
        {
            entity.HasKey(e => e.HistorialContrasenaId).HasName("PK__Historia__DD19B6F5BD823744");

            entity.ToTable("HistorialContrasena");

            entity.Property(e => e.HistorialContrasenaId).HasColumnName("HistorialContrasenaID");
            entity.Property(e => e.ContrasenaHash).HasMaxLength(256);
            entity.Property(e => e.ContrasenaSalt).HasMaxLength(256);
            entity.Property(e => e.FechaCambio)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Usuario).WithMany(p => p.HistorialContrasenas)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Historial__Usuar__6A30C649");
        });

        modelBuilder.Entity<LogActividad>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__LogActiv__5E5499A81ACFE7A7");

            entity.ToTable("LogActividad");

            entity.Property(e => e.LogId).HasColumnName("LogID");
            entity.Property(e => e.Accion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Detalles)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.DireccionIp)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DireccionIP");
            entity.Property(e => e.FechaHora)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Usuario).WithMany(p => p.LogActividads)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__LogActivi__Usuar__71D1E811");
        });

        modelBuilder.Entity<Notificacion>(entity =>
        {
            entity.HasKey(e => e.NotificacionId).HasName("PK__Notifica__BCC120C472827D74");

            entity.ToTable("Notificacion");

            entity.HasIndex(e => e.Leida, "idx_Notificacion_Leida");

            entity.HasIndex(e => e.TipoNotificacion, "idx_Notificacion_Tipo");

            entity.HasIndex(e => e.UsuarioId, "idx_Notificacion_UsuarioID");

            entity.Property(e => e.NotificacionId).HasColumnName("NotificacionID");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaLectura).HasColumnType("datetime");
            entity.Property(e => e.Mensaje)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.TipoNotificacion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Titulo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Notificacions)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notificac__Usuar__66603565");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.PagoId).HasName("PK__Pago__F00B615884A93C82");

            entity.ToTable("Pago");

            entity.HasIndex(e => e.EstadoPagoId, "idx_Pago_Estado");

            entity.HasIndex(e => e.FechaPago, "idx_Pago_Fecha");

            entity.HasIndex(e => e.UsuarioId, "idx_Pago_UsuarioID");

            entity.Property(e => e.PagoId).HasColumnName("PagoID");
            entity.Property(e => e.Comprobante)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EstadoPagoId).HasColumnName("EstadoPagoID");
            entity.Property(e => e.FechaAprobacion).HasColumnType("datetime");
            entity.Property(e => e.FechaPago)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Monto).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.AprobadoPorNavigation).WithMany(p => p.PagoAprobadoPorNavigations)
                .HasForeignKey(d => d.AprobadoPor)
                .HasConstraintName("FK__Pago__AprobadoPo__5BE2A6F2");

            entity.HasOne(d => d.EstadoPago).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.EstadoPagoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pago__EstadoPago__5AEE82B9");

            entity.HasOne(d => d.Usuario).WithMany(p => p.PagoUsuarios)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pago__UsuarioID__59FA5E80");
        });

        modelBuilder.Entity<RedReferido>(entity =>
        {
            entity.HasKey(e => e.RedReferidosId).HasName("PK__RedRefer__201311684EAF4493");

            entity.HasIndex(e => e.Nivel, "idx_RedReferidos_Nivel");

            entity.HasIndex(e => e.ReferenteId, "idx_RedReferidos_ReferenteID");

            entity.HasIndex(e => e.UsuarioId, "idx_RedReferidos_UsuarioID");

            entity.Property(e => e.RedReferidosId).HasColumnName("RedReferidosID");
            entity.Property(e => e.FechaVinculacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ReferenteId).HasColumnName("ReferenteID");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Referente).WithMany(p => p.RedReferidoReferentes)
                .HasForeignKey(d => d.ReferenteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RedReferi__Refer__5441852A");

            entity.HasOne(d => d.Usuario).WithMany(p => p.RedReferidoUsuarios)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RedReferi__Usuar__534D60F1");
        });

        modelBuilder.Entity<Reporte>(entity =>
        {
            entity.HasKey(e => e.ReporteId).HasName("PK__Reporte__0B29EA4ED46BAE54");

            entity.ToTable("Reporte");

            entity.Property(e => e.ReporteId).HasColumnName("ReporteID");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Parametros)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.QuerySql)
                .HasColumnType("text")
                .HasColumnName("QuerySQL");

            entity.HasOne(d => d.CreadoPorNavigation).WithMany(p => p.Reportes)
                .HasForeignKey(d => d.CreadoPor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reporte__CreadoP__75A278F5");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.RolId).HasName("PK__Rol__F92302D1F0E9A9AF");

            entity.ToTable("Rol");

            entity.Property(e => e.RolId).HasColumnName("RolID");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Sesion>(entity =>
        {
            entity.HasKey(e => e.SesionId).HasName("PK__Sesion__52FD7C060F25BEC4");

            entity.ToTable("Sesion");

            entity.Property(e => e.SesionId).HasColumnName("SesionID");
            entity.Property(e => e.Activa).HasDefaultValue(true);
            entity.Property(e => e.DireccionIp)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DireccionIP");
            entity.Property(e => e.Dispositivo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaExpiracion).HasColumnType("datetime");
            entity.Property(e => e.FechaInicio)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Token)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Sesions)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Sesion__UsuarioI__7A672E12");
        });

        modelBuilder.Entity<TipoCuentaBancarium>(entity =>
        {
            entity.HasKey(e => e.TipoCuentaBancariaId).HasName("PK__TipoCuen__F11492A19D7BAAFA");

            entity.Property(e => e.TipoCuentaBancariaId).HasColumnName("TipoCuentaBancariaID");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoDocumento>(entity =>
        {
            entity.HasKey(e => e.TipoDocumentoId).HasName("PK__TipoDocu__A329EAA789A75E61");

            entity.ToTable("TipoDocumento");

            entity.Property(e => e.TipoDocumentoId).HasColumnName("TipoDocumentoID");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuario__2B3DE7988667F779");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.CodigoReferencia, "UQ__Usuario__4C628957038D8343").IsUnique();

            entity.HasIndex(e => e.NumeroDocumento, "UQ__Usuario__A42025882DB1B198").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Usuario__A9D1053447D241C3").IsUnique();

            entity.HasIndex(e => e.CodigoReferencia, "idx_Usuario_CodigoReferencia");

            entity.HasIndex(e => e.Email, "idx_Usuario_Email");

            entity.HasIndex(e => e.EstadoUsuarioId, "idx_Usuario_Estado");

            entity.HasIndex(e => e.NumeroDocumento, "idx_Usuario_NumeroDocumento");

            entity.HasIndex(e => e.ReferenteId, "idx_Usuario_ReferenteID");

            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");
            entity.Property(e => e.BancoId).HasColumnName("BancoID");
            entity.Property(e => e.Bloqueado).HasDefaultValue(false);
            entity.Property(e => e.CodigoReferencia)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ContrasenaHash).HasMaxLength(256);
            entity.Property(e => e.ContrasenaSalt).HasMaxLength(256);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EstadoUsuarioId)
                .HasDefaultValue(1)
                .HasColumnName("EstadoUsuarioID");
            entity.Property(e => e.FechaExpiracionToken).HasColumnType("datetime");
            entity.Property(e => e.FechaHabilitacion).HasColumnType("datetime");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaUltimoAcceso).HasColumnType("datetime");
            entity.Property(e => e.IntentosFallidos).HasDefaultValue(0);
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NumeroCelular)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.NumeroCuentaBancaria)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NumeroDocumento)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ReferenteId).HasColumnName("ReferenteID");
            entity.Property(e => e.RolId)
                .HasDefaultValue(2)
                .HasColumnName("RolID");
            entity.Property(e => e.TipoCuentaBancariaId).HasColumnName("TipoCuentaBancariaID");
            entity.Property(e => e.TipoDocumentoId).HasColumnName("TipoDocumentoID");
            entity.Property(e => e.TokenVerificacion)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Banco).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.BancoId)
                .HasConstraintName("FK__Usuario__BancoID__4CA06362");

            entity.HasOne(d => d.EstadoUsuario).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.EstadoUsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuario__EstadoU__4F7CD00D");

            entity.HasOne(d => d.Referente).WithMany(p => p.InverseReferente)
                .HasForeignKey(d => d.ReferenteId)
                .HasConstraintName("FK__Usuario__Referen__4D94879B");

            entity.HasOne(d => d.Rol).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.RolId)
                .HasConstraintName("FK__Usuario__RolID__4E88ABD4");

            entity.HasOne(d => d.TipoCuentaBancaria).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.TipoCuentaBancariaId)
                .HasConstraintName("FK__Usuario__TipoCue__4BAC3F29");

            entity.HasOne(d => d.TipoDocumento).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.TipoDocumentoId)
                .HasConstraintName("FK__Usuario__TipoDoc__4AB81AF0");
        });

        modelBuilder.Entity<VwComisionesPendiente>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwComisionesPendientes");

            entity.Property(e => e.CodigoReferencia)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CodigoReferido)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ComisionId).HasColumnName("ComisionID");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaGeneracion).HasColumnType("datetime");
            entity.Property(e => e.Monto).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NombreReferido)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ReferidoId).HasColumnName("ReferidoID");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");
        });

        modelBuilder.Entity<VwRedReferido>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwRedReferidos");

            entity.Property(e => e.CodigoReferencia)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CodigoReferente)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.FechaVinculacion).HasColumnType("datetime");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NombreReferente)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ReferenteId).HasColumnName("ReferenteID");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");
        });

        modelBuilder.Entity<VwUsuariosPendiente>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwUsuariosPendientes");

            entity.Property(e => e.Banco)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CodigoReferencia)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CodigoReferente)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NombreReferente)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NumeroCelular)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.NumeroCuentaBancaria)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NumeroDocumento)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TipoCuenta)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TipoDocumento)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
