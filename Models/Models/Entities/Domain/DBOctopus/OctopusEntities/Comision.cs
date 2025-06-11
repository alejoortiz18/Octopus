using System;
using System.Collections.Generic;

namespace Models.Entities.Domain.DBOctopus.OctopusEntities;

public partial class Comision
{
    public int ComisionId { get; set; }

    public int UsuarioId { get; set; }

    public int ReferidoId { get; set; }

    public decimal Monto { get; set; }

    public DateTime FechaGeneracion { get; set; }

    public DateTime? FechaPago { get; set; }

    public int Nivel { get; set; }

    public int EstadoPagoId { get; set; }

    public virtual EstadoPago EstadoPago { get; set; } = null!;

    public virtual Usuario Referido { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
