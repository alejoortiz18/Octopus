using System;
using System.Collections.Generic;

namespace Models.Entities.Domain.DBOctopus.OctopusEntities;

public partial class Rol
{
    public int RolId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public int NivelAcceso { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
