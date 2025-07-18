using System;
using System.Collections.Generic;

namespace webBotica2.Models;

using System.ComponentModel.DataAnnotations;

public partial class Usuario
{
    public int IdUser { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres")]
    public string Nombre { get; set; } = null!;

    [Required(ErrorMessage = "El apellido es obligatorio")]
    [StringLength(50, ErrorMessage = "El apellido no puede exceder los 50 caracteres")]
    public string Apellido { get; set; } = null!;

    [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
    [StringLength(30, ErrorMessage = "El usuario no puede exceder los 30 caracteres")]
    public string Usuario1 { get; set; } = null!;

    [Required(ErrorMessage = "La contraseña es obligatoria")]
    [StringLength(100, ErrorMessage = "La contraseña no puede exceder los 100 caracteres")]
    [DataType(DataType.Password)]
    public string Contraseña { get; set; } = null!;

    public bool Estado { get; set; } = true;

    [Required(ErrorMessage = "Ningun Rol seleccionado")]
    public int? IdRol { get; set; }

    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();

    public virtual Rol? IdRolNavigation { get; set; }
}
