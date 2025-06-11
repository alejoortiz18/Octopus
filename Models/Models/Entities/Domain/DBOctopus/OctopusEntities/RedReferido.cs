using System;
using System.Collections.Generic;

namespace Models.Entities.Domain.DBOctopus.OctopusEntities;

public partial class RedReferido
{
    public int RedReferidosId { get; set; }

    public int UsuarioId { get; set; }

    public int ReferenteId { get; set; }

    public int Nivel { get; set; }

    public DateTime FechaVinculacion { get; set; }

    public virtual Usuario Referente { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
