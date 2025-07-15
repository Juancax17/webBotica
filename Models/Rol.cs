using System;
using System.Collections.Generic;

namespace webBotica2.Models;

public partial class Rol
{
    public int IdRol { get; set; }

    public string Rol1 { get; set; } = null!;

    public string? Descripcion { get; set; }

    public bool Estado { get; set; } = true;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
