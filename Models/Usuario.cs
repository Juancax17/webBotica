using System;
using System.Collections.Generic;

namespace webBotica2.Models;

public partial class Usuario
{
    public int IdUser { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Usuario1 { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public bool Estado { get; set; } = true;

    public int? IdRol { get; set; }

    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();

    public virtual Rol? IdRolNavigation { get; set; }
}
