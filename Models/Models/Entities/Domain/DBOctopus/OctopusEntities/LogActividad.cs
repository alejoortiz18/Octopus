using System;
using System.Collections.Generic;

namespace Models.Entities.Domain.DBOctopus.OctopusEntities;

public partial class LogActividad
{
    public int LogId { get; set; }

    public int? UsuarioId { get; set; }

    public string Accion { get; set; } = null!;

    public string? Detalles { get; set; }

    public DateTime FechaHora { get; set; }

    public string? DireccionIp { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
