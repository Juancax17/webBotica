using System;
using System.Collections.Generic;

namespace webBotica2.Models;

public partial class Producto
{
    public int IdProd { get; set; }

    public string Sku { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Presentacion { get; set; } = null!;

    public decimal PrecioCompra { get; set; }

    public decimal PrecioVenta { get; set; }

    public DateOnly FechaVen { get; set; }

    public int Stock { get; set; }

    public int? IdProveedor { get; set; }

    public int? IdCategoria { get; set; }

    public int? IdMarca { get; set; }

    public byte[]? Foto { get; set; }

    public bool Estado { get; set; } 

    public virtual ICollection<DetalleCompra> DetalleCompras { get; set; } = new List<DetalleCompra>();

    public virtual ICollection<DetalleVentum> DetalleVenta { get; set; } = new List<DetalleVentum>();

    public virtual ICollection<Devolucione> Devoluciones { get; set; } = new List<Devolucione>();

    public virtual ICollection<HistorialVencimiento> HistorialVencimientos { get; set; } = new List<HistorialVencimiento>();

    public virtual Categorium? IdCategoriaNavigation { get; set; }

    public virtual Marca? IdMarcaNavigation { get; set; }

    public virtual Proveedore? IdProveedorNavigation { get; set; }
}
