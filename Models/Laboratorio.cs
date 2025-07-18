using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace webBotica2.Models;

public partial class Laboratorio
{
    public int IdLaboratorio { get; set; }

    public string Nombre { get; set; } = null!;

    [Required(ErrorMessage = "El RUC es obligatorio")]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "El RUC debe tener 11 dígitos")]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "El RUC debe contener solo números")]
    public string Ruc { get; set; } = null!;

    public string? Direccion { get; set; }

    [StringLength(9, MinimumLength = 9, ErrorMessage = "El Numero de telefono debe tener 9 dígitos")]
    public string? Telefono { get; set; }

    [EmailAddress(ErrorMessage = "El Correo no es válido")]
    [StringLength(100, ErrorMessage = "El Correo no puede tener más de 100 caracteres")]
    public string? Correo { get; set; }

    public bool Estado { get; set; } = true;

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();

}
