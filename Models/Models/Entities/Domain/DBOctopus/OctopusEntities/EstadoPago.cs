using System;
using System.Collections.Generic;

namespace Models.Entities.Domain.DBOctopus.OctopusEntities;

public partial class EstadoPago
{
    public int EstadoPagoId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<Comision> Comisions { get; set; } = new List<Comision>();

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
