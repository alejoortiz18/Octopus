using System;
using System.Collections.Generic;

namespace Models.Entities.Domain.DBOctopus.OctopusEntities;

public partial class VwUsuariosPendiente
{
    public int UsuarioId { get; set; }

    public string CodigoReferencia { get; set; } = null!;

    public string NombreCompleto { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string NumeroCelular { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public string TipoDocumento { get; set; } = null!;

    public string NumeroDocumento { get; set; } = null!;

    public string TipoCuenta { get; set; } = null!;

    public string Banco { get; set; } = null!;

    public string NumeroCuentaBancaria { get; set; } = null!;

    public string? CodigoReferente { get; set; }

    public string? NombreReferente { get; set; }
}
