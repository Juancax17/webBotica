using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace webBotica2.Models;

public partial class Proveedore
{
    public int IdProveedor { get; set; }

    [Required(ErrorMessage = "El RUC es obligatorio")]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "El RUC debe tener 11 dígitos")]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "El RUC debe contener solo números")]
    public string Ruc { get; set; } = null!;

    public string RazonSocial { get; set; } = null!;

    public string Tipo { get; set; } = null!;

    [StringLength(11, MinimumLength = 9, ErrorMessage = "El Telefono debe tener 9 dígitos")]
    public string Telefono { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public bool Estado { get; set; }= true;

    public int? IdLaboratorio { get; set; }

    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();

    public virtual Laboratorio? IdLaboratorioNavigation { get; set; } 

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
