using System;
using System.Collections.Generic;

namespace Models.Entities.Domain.DBOctopus.OctopusEntities;

public partial class Notificacion
{
    public int NotificacionId { get; set; }

    public int UsuarioId { get; set; }

    public string Titulo { get; set; } = null!;

    public string Mensaje { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public bool Leida { get; set; }

    public DateTime? FechaLectura { get; set; }

    public string TipoNotificacion { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
