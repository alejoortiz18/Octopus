using System;
using System.Collections.Generic;

namespace Models.Entities.Domain.DBOctopus.OctopusEntities;

public partial class VwComisionesPendiente
{
    public int ComisionId { get; set; }

    public int UsuarioId { get; set; }

    public string CodigoReferencia { get; set; } = null!;

    public string NombreCompleto { get; set; } = null!;

    public int ReferidoId { get; set; }

    public string CodigoReferido { get; set; } = null!;

    public string NombreReferido { get; set; } = null!;

    public decimal Monto { get; set; }

    public DateTime FechaGeneracion { get; set; }

    public int Nivel { get; set; }

    public string Estado { get; set; } = null!;
}
