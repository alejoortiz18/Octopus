using System;
using System.Collections.Generic;

namespace Models.Entities.Domain.DBOctopus.OctopusEntities;

public partial class Pago
{
    public int PagoId { get; set; }

    public int UsuarioId { get; set; }

    public decimal Monto { get; set; }

    public DateTime FechaPago { get; set; }

    public int EstadoPagoId { get; set; }

    public string? Comprobante { get; set; }

    public DateTime? FechaAprobacion { get; set; }

    public int? AprobadoPor { get; set; }

    public string? Observaciones { get; set; }

    public virtual Usuario? AprobadoPorNavigation { get; set; }

    public virtual EstadoPago EstadoPago { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
