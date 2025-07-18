using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace webBotica2.Models;

using System.ComponentModel.DataAnnotations;

public class Producto
{
    [Key]
    public int IdProd { get; set; }

    [Required(ErrorMessage = "El SKU es obligatorio")]
    [StringLength(50, ErrorMessage = "El SKU no puede exceder los 50 caracteres")]
    public string Sku { get; set; } = null!;

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
    public string Nombre { get; set; } = null!;

    [Required(ErrorMessage = "La presentación es obligatoria")]
    [StringLength(100, ErrorMessage = "La presentación no puede exceder los 100 caracteres")]
    public string Presentacion { get; set; } = null!;

    [Required(ErrorMessage = "El precio de compra es obligatorio")]
    [Range(0.01, 99999.99, ErrorMessage = "El precio de compra debe estar entre 0.01 y 99999.99")]
    public decimal PrecioCompra { get; set; }

    [Required(ErrorMessage = "El precio de venta es obligatorio")]
    [Range(0.01, 99999.99, ErrorMessage = "El precio de venta debe estar entre 0.01 y 99999.99")]
    public decimal PrecioVenta { get; set; }

    [Required(ErrorMessage = "La fecha de vencimiento es obligatoria")]
    [DataType(DataType.Date)]
    public DateOnly FechaVen { get; set; }

    [Required(ErrorMessage = "El stock es obligatorio")]
    [Range(0, int.MaxValue, ErrorMessage = "El stock debe ser un número positivo")]
    public int Stock { get; set; }

    [Required(ErrorMessage = "El stock mínimo es obligatorio")]
    [Range(0, int.MaxValue, ErrorMessage = "El stock mínimo debe ser un número positivo")]
    public int StockMinimo { get; set; }

    [Required(ErrorMessage = "Ningun Proveedor seleccionado")]
    public int? IdProveedor { get; set; }
    [Required(ErrorMessage = "Ningun Laboratorio Seleccionado")]
    public int? IdLaboratorio { get; set; }
    [Required(ErrorMessage = "Ninguna Categoria seleccionada")]
    public int? IdCategoria { get; set; }
    [Required(ErrorMessage = "Ninguna Marca seleccionada")]
    public int? IdMarca { get; set; }

    public byte[]? Foto { get; set; }

    public bool Estado { get; set; } = true;


    public virtual ICollection<DetalleCompra> DetalleCompras { get; set; } = new List<DetalleCompra>();
    public virtual ICollection<DetalleVentum> DetalleVenta { get; set; } = new List<DetalleVentum>();
    public virtual ICollection<Devolucione> Devoluciones { get; set; } = new List<Devolucione>();
    public virtual ICollection<HistorialVencimiento> HistorialVencimientos { get; set; } = new List<HistorialVencimiento>();

    public virtual Laboratorio? IdLaboratorioNavigation { get; set; }
    public virtual Categorium? IdCategoriaNavigation { get; set; }
    public virtual Marca? IdMarcaNavigation { get; set; }
    public virtual Proveedore? IdProveedorNavigation { get; set; }
}

