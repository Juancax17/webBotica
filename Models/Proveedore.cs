using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webBotica2.Models;

public partial class Proveedore
{
    public int IdProveedor { get; set; }

    [Required(ErrorMessage = "El RUC es obligatorio")]
    [RegularExpression(@"^(10|20)\d{9}$", ErrorMessage = "El RUC debe empezar con 10 o 20 y tener 11 dígitos")]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "El RUC debe tener exactamente 11 dígitos")]
    public string Ruc { get; set; } = null!;

    [Required(ErrorMessage = "La razón social es obligatoria")]
    public string RazonSocial { get; set; } = null!;

    [StringLength(9, MinimumLength = 9, ErrorMessage = "El Telefono debe tener 9 dígitos")]
    public string? Telefono { get; set; }

    [EmailAddress(ErrorMessage = "El correo no es válido")]
    public string Correo { get; set; } = null!;

    public bool Estado { get; set; } = true;

    [NotMapped]
    public string Tipo => Ruc.StartsWith("10") ? "Persona Natural" : "Persona Jurídica";

    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();

}
