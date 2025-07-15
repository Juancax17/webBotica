using System;
using System.Collections.Generic;

namespace webBotica2.Models;

public partial class DetalleVentum
{
    public int IdDetalle { get; set; }

    public int Cant { get; set; }

    public decimal PrecioVenta { get; set; }

    public decimal Subtotal { get; set; }

    public int? IdProd { get; set; }

    public int? IdVenta { get; set; }

    public virtual Producto? IdProdNavigation { get; set; }

    public virtual Venta? IdVentaNavigation { get; set; }
}
