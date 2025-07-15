using System;
using System.Collections.Generic;

namespace webBotica2.Models;

public partial class DetalleCompra
{
    public int IdDetalleCompra { get; set; }

    public int? IdCompra { get; set; }

    public int? IdProd { get; set; }

    public int Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public decimal Subtotal { get; set; }

    public virtual Compra? IdCompraNavigation { get; set; }

    public virtual Producto? IdProdNavigation { get; set; }
}
