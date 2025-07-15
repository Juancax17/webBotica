using System;
using System.Collections.Generic;

namespace webBotica2.Models;

public partial class Venta
{
    public int IdVenta { get; set; }

    public decimal Total { get; set; }

    public DateOnly Fecha { get; set; }

    public int? IdCliente { get; set; }

    public string EstadoVenta { get; set; } = null!;

    public virtual ICollection<Comprobante> Comprobantes { get; set; } = new List<Comprobante>();

    public virtual ICollection<DetalleVentum> DetalleVenta { get; set; } = new List<DetalleVentum>();

    public virtual ICollection<Devolucione> Devoluciones { get; set; } = new List<Devolucione>();

    public virtual Cliente? IdClienteNavigation { get; set; }
}
