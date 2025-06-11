using System;
using System.Collections.Generic;

namespace Models.Entities.Domain.DBOctopus.OctopusEntities;

public partial class BackupRegistro
{
    public int BackupId { get; set; }

    public string NombreArchivo { get; set; } = null!;

    public string Ruta { get; set; } = null!;

    public decimal TamanoMb { get; set; }

    public DateTime FechaBackup { get; set; }

    public int UsuarioId { get; set; }

    public string Estado { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
