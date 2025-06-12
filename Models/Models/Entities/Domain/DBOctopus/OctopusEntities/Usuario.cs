using System;
using System.Collections.Generic;

namespace Models.Entities.Domain.DBOctopus.OctopusEntities;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public int? ReferenteId { get; set; }

    public int? BancoId { get; set; }

    public int? RolId { get; set; }

    public int EstadoUsuarioId { get; set; }

    public int? TipoDocumentoId { get; set; }

    public int? TipoCuentaBancariaId { get; set; }

    public string? CodigoReferencia { get; set; }

    public string NombreCompleto { get; set; } = null!;

    public string? NumeroDocumento { get; set; }

    public string Email { get; set; } = null!;

    public byte[] ContrasenaHash { get; set; } = null!;

    public byte[] ContrasenaSalt { get; set; } = null!;

    public string? NumeroCelular { get; set; }

    public string? NumeroCuentaBancaria { get; set; }

    public DateTime FechaRegistro { get; set; }

    public DateTime? FechaUltimoAcceso { get; set; }

    public bool CambioContrasena { get; set; }

    public string? TokenVerificacion { get; set; }

    public DateTime? FechaExpiracionToken { get; set; }

    public int? IntentosFallidos { get; set; }

    public bool? Bloqueado { get; set; }

    public DateTime? FechaHabilitacion { get; set; }

    public virtual ICollection<BackupRegistro> BackupRegistros { get; set; } = new List<BackupRegistro>();

    public virtual Banco? Banco { get; set; }

    public virtual ICollection<Comision> ComisionReferidos { get; set; } = new List<Comision>();

    public virtual ICollection<Comision> ComisionUsuarios { get; set; } = new List<Comision>();

    public virtual EstadoUsuario EstadoUsuario { get; set; } = null!;

    public virtual ICollection<HistorialContrasena> HistorialContrasenas { get; set; } = new List<HistorialContrasena>();

    public virtual ICollection<Usuario> InverseReferente { get; set; } = new List<Usuario>();

    public virtual ICollection<LogActividad> LogActividads { get; set; } = new List<LogActividad>();

    public virtual ICollection<Notificacion> Notificacions { get; set; } = new List<Notificacion>();

    public virtual ICollection<Pago> PagoAprobadoPorNavigations { get; set; } = new List<Pago>();

    public virtual ICollection<Pago> PagoUsuarios { get; set; } = new List<Pago>();

    public virtual ICollection<RedReferido> RedReferidoReferentes { get; set; } = new List<RedReferido>();

    public virtual ICollection<RedReferido> RedReferidoUsuarios { get; set; } = new List<RedReferido>();

    public virtual Usuario? Referente { get; set; }

    public virtual ICollection<Reporte> Reportes { get; set; } = new List<Reporte>();

    public virtual Rol? Rol { get; set; }

    public virtual ICollection<Sesion> Sesions { get; set; } = new List<Sesion>();

    public virtual TipoCuentaBancarium? TipoCuentaBancaria { get; set; }

    public virtual TipoDocumento? TipoDocumento { get; set; }
}
