using System;
using System.Collections.Generic;

namespace Models.Entities.Domain.DBOctopus.OctopusEntities;

public partial class HistorialContrasena
{
    public int HistorialContrasenaId { get; set; }

    public int UsuarioId { get; set; }

    public byte[] ContrasenaHash { get; set; } = null!;

    public byte[] ContrasenaSalt { get; set; } = null!;

    public DateTime FechaCambio { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;
}
