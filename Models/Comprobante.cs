using System;
using System.Collections.Generic;

namespace webBotica2.Models;

public partial class Comprobante
{
    public int IdComprobante { get; set; }

    public string Tipo { get; set; } = null!;

    public string Numero { get; set; } = null!;

    public DateOnly Fecha { get; set; }

    public int? IdVenta { get; set; }

    public string Estado { get; set; } = null!;

    public virtual Venta? IdVentaNavigation { get; set; }
}
