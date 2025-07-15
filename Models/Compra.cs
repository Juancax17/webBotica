using System;
using System.Collections.Generic;

namespace webBotica2.Models;

public partial class Compra
{
    public int IdCompra { get; set; }

    public DateOnly Fecha { get; set; }

    public decimal Total { get; set; }

    public int? IdProveedor { get; set; }

    public int? IdUsuario { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<DetalleCompra> DetalleCompras { get; set; } = new List<DetalleCompra>();

    public virtual Proveedore? IdProveedorNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
