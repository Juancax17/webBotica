using System;
using System.Collections.Generic;

namespace webBotica2.Models;

public partial class HistorialVencimiento
{
    public int IdVencimiento { get; set; }

    public int? IdProd { get; set; }

    public DateOnly FechaVen { get; set; }

    public bool Estado { get; set; }

    public virtual Producto? IdProdNavigation { get; set; }
}
