using System;
using System.Collections.Generic;

namespace Models.Entities.Domain.DBOctopus.OctopusEntities;

public partial class Sesion
{
    public int SesionId { get; set; }

    public int UsuarioId { get; set; }

    public string Token { get; set; } = null!;

    public DateTime FechaInicio { get; set; }

    public DateTime FechaExpiracion { get; set; }

    public string? DireccionIp { get; set; }

    public string? Dispositivo { get; set; }

    public bool Activa { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;
}
