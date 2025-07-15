using System;
using System.Collections.Generic;

namespace webBotica2.Models;

public partial class Devolucione
{
    public int IdDevolucion { get; set; }

    public int? IdVenta { get; set; }

    public int? IdProd { get; set; }

    public int Cantidad { get; set; }

    public string? Motivo { get; set; }

    public DateOnly Fecha { get; set; }

    public string? Estado { get; set; }

    public virtual Producto? IdProdNavigation { get; set; }

    public virtual Venta? IdVentaNavigation { get; set; }
}
