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
            entity.HasKey(e => e.BackupId).HasName("PK__BackupRe__EB9069E2E5576FD9");

            entity.ToTable("BackupRegistro");

            entity.Property(e => e.BackupId).HasColumnName("BackupID");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.FechaBackup).HasColumnType("datetime");
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
                .HasConstraintName("FK_BackupRegistro_Usuario");
        });

        modelBuilder.Entity<Banco>(entity =>
        {
            entity.HasKey(e => e.BancoId).HasName("PK__Banco__4A8BAC15808CB759");

            entity.ToTable("Banco");

            entity.Property(e => e.BancoId).HasColumnName("BancoID");
            entity.Property(e => e.Codigo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Comision>(entity =>
        {
            entity.HasKey(e => e.ComisionId).HasName("PK__Comision__A014A7120CCFDB5B");

            entity.ToTable("Comision");

            entity.Property(e => e.ComisionId).HasColumnName("ComisionID");
            entity.Property(e => e.EstadoPagoId).HasColumnName("EstadoPagoID");
            entity.Property(e => e.FechaGeneracion).HasColumnType("datetime");
            entity.Property(e => e.FechaPago).HasColumnType("datetime");
            entity.Property(e => e.Monto).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ReferidoId).HasColumnName("ReferidoID");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.EstadoPago).WithMany(p => p.Comisions)
                .HasForeignKey(d => d.EstadoPagoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comision_EstadoPago");

            entity.HasOne(d => d.Referido).WithMany(p => p.ComisionReferidos)
                .HasForeignKey(d => d.ReferidoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comision_Usuario1");

            entity.HasOne(d => d.Usuario).WithMany(p => p.ComisionUsuarios)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comision_Usuario");
        });

        modelBuilder.Entity<ConfiguracionSistema>(entity =>
        {
            entity.HasKey(e => e.ConfiguracionId).HasName("PK__Configur__9B95E056C9EEF648");

            entity.ToTable("ConfiguracionSistema");

            entity.HasIndex(e => e.Clave, "UQ__Configur__E8181E112FB06DF8").IsUnique();

            entity.Property(e => e.ConfiguracionId).HasColumnName("ConfiguracionID");
            entity.Property(e => e.Clave)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Valor)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EstadoPago>(entity =>
        {
            entity.HasKey(e => e.EstadoPagoId).HasName("PK__EstadoPa__63AD30BDA06631C0");

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
            entity.HasKey(e => e.EstadoUsuarioId).HasName("PK__EstadoUs__BAA0F882D693DE64");

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
            entity.HasKey(e => e.HistorialContrasenaId).HasName("PK__Historia__DD19B6F59F244290");

            entity.ToTable("HistorialContrasena");

            entity.Property(e => e.HistorialContrasenaId).HasColumnName("HistorialContrasenaID");
            entity.Property(e => e.ContrasenaHash).HasMaxLength(256);
            entity.Property(e => e.ContrasenaSalt).HasMaxLength(256);
            entity.Property(e => e.FechaCambio).HasColumnType("datetime");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Usuario).WithMany(p => p.HistorialContrasenas)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HistorialContrasena_Usuario");
        });

        modelBuilder.Entity<LogActividad>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__LogActiv__5E5499A83D8C0CCD");

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
                .HasConstraintName("FK_LogActividad_Usuario");
        });

        modelBuilder.Entity<Notificacion>(entity =>
        {
            entity.HasKey(e => e.NotificacionId).HasName("PK__Notifica__BCC120C41EB5306B");

            entity.ToTable("Notificacion");

            entity.Property(e => e.NotificacionId).HasColumnName("NotificacionID");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
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
                .HasConstraintName("FK_Notificacion_Usuario");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.PagoId).HasName("PK__Pago__F00B6158D6EE5F86");

            entity.ToTable("Pago");

            entity.Property(e => e.PagoId).HasColumnName("PagoID");
            entity.Property(e => e.Comprobante)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EstadoPagoId).HasColumnName("EstadoPagoID");
            entity.Property(e => e.FechaAprobacion).HasColumnType("datetime");
            entity.Property(e => e.FechaPago).HasColumnType("datetime");
            entity.Property(e => e.Monto).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.EstadoPago).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.EstadoPagoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pago_EstadoPago");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pago_Usuario");
        });

        modelBuilder.Entity<RedReferido>(entity =>
        {
            entity.HasKey(e => e.RedReferidosId).HasName("PK__RedRefer__201311682C415EC5");

            entity.Property(e => e.RedReferidosId).HasColumnName("RedReferidosID");
            entity.Property(e => e.FechaVinculacion).HasColumnType("datetime");
            entity.Property(e => e.ReferenteId).HasColumnName("ReferenteID");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Referente).WithMany(p => p.RedReferidoReferentes)
                .HasForeignKey(d => d.ReferenteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RedReferidos_Usuario");

            entity.HasOne(d => d.Usuario).WithMany(p => p.RedReferidoUsuarios)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RedReferidos_Usuario1");
        });

        modelBuilder.Entity<Reporte>(entity =>
        {
            entity.HasKey(e => e.ReporteId).HasName("PK__Reporte__0B29EA4E742094A4");

            entity.ToTable("Reporte");

            entity.Property(e => e.ReporteId).HasColumnName("ReporteID");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
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
                .HasConstraintName("FK_Reporte_Usuario");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.RolId).HasName("PK__Rol__F92302D1AE3C2F86");

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
            entity.HasKey(e => e.SesionId).HasName("PK__Sesion__52FD7C06C0FBD50E");

            entity.ToTable("Sesion");

            entity.Property(e => e.SesionId).HasColumnName("SesionID");
            entity.Property(e => e.DireccionIp)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DireccionIP");
            entity.Property(e => e.Dispositivo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaExpiracion).HasColumnType("datetime");
            entity.Property(e => e.FechaInicio).HasColumnType("datetime");
            entity.Property(e => e.Token)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Sesions)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sesion_Usuario");
        });

        modelBuilder.Entity<TipoCuentaBancarium>(entity =>
        {
            entity.HasKey(e => e.TipoCuentaBancariaId).HasName("PK__TipoCuen__F11492A1232B6249");

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
            entity.HasKey(e => e.TipoDocumentoId).HasName("PK__TipoDocu__A329EAA7B273A7E7");

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

            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");
            entity.Property(e => e.BancoId).HasColumnName("BancoID");
            entity.Property(e => e.Bloqueado).HasDefaultValue(false);
            entity.Property(e => e.CambioContrasena).HasDefaultValue(false);
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
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuario__RolID__4E88ABD4");

            entity.HasOne(d => d.TipoCuentaBancaria).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.TipoCuentaBancariaId)
                .HasConstraintName("FK__Usuario__TipoCue__4BAC3F29");

            entity.HasOne(d => d.TipoDocumento).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.TipoDocumentoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
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
