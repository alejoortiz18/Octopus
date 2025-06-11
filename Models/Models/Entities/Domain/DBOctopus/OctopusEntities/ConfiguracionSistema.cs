using System;
using System.Collections.Generic;

namespace Models.Entities.Domain.DBOctopus.OctopusEntities;

public partial class ConfiguracionSistema
{
    public int ConfiguracionId { get; set; }

    public string Clave { get; set; } = null!;

    public string Valor { get; set; } = null!;

    public string? Descripcion { get; set; }

    public bool? Modificable { get; set; }
}
