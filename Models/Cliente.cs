using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;

namespace webBotica2.Models;

public partial class Cliente
{
    [Key]
    public int IdCliente { get; set; }

    [Required(ErrorMessage = "El Documento es obligatorio")]
    [StringLength(12, ErrorMessage = "El Documento no puede tener más de 12 caracteres")]
    [RegularExpression("^[0-9]+$", ErrorMessage = "El Documento debe contener solo números")]
    public string Documento { get; set; } = null!;

    [Required(ErrorMessage = "El Nombre es obligatorio")]
    [StringLength(50, ErrorMessage = "El Nombre no puede tener más de 50 caracteres")]
    public string Nombre { get; set; } = null!;

    [StringLength(50, ErrorMessage = "El Apellido Paterno no puede tener más de 50 caracteres")]
    public string? ApellidoPaterno { get; set; }

    [StringLength(50, ErrorMessage = "El Apellido Materno no puede tener más de 50 caracteres")]
    public string? ApellidoMaterno { get; set; }

    [Phone(ErrorMessage = "El Teléfono no es válido")]
    [StringLength(9, ErrorMessage = "El Teléfono no puede tener más de 9 caracteres")]
    public string? Telefono { get; set; }

    [EmailAddress(ErrorMessage = "El Correo no es válido")]
    [StringLength(100, ErrorMessage = "El Correo no puede tener más de 100 caracteres")]
    public string? Correo { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Fecha de Nacimiento")]
    public DateOnly? FechaNac { get; set; }

    [StringLength(200, ErrorMessage = "La Dirección no puede tener más de 200 caracteres")]
    public string? Direccion { get; set; }

    public bool Estado { get; set; } = true;

    public bool EsRegistrado { get; set; } = true;

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}

