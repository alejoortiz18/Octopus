using System;
using System.Collections.Generic;

namespace Models.Entities.Domain.DBOctopus.OctopusEntities;

public partial class Reporte
{
    public int ReporteId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string QuerySql { get; set; } = null!;

    public int CreadoPor { get; set; }

    public DateTime FechaCreacion { get; set; }

    public string? Parametros { get; set; }

    public virtual Usuario CreadoPorNavigation { get; set; } = null!;
}
