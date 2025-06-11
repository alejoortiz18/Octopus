using System;
using System.Collections.Generic;

namespace Models.Entities.Domain.DBOctopus.OctopusEntities;

public partial class VwRedReferido
{
    public int UsuarioId { get; set; }

    public string CodigoReferencia { get; set; } = null!;

    public string NombreCompleto { get; set; } = null!;

    public int ReferenteId { get; set; }

    public string CodigoReferente { get; set; } = null!;

    public string NombreReferente { get; set; } = null!;

    public int Nivel { get; set; }

    public DateTime FechaVinculacion { get; set; }

    public int? CantidadReferidosDirectos { get; set; }
}
