using System;
using System.Collections.Generic;

namespace webBotica2.Models;

public partial class Marca
{
    public int IdMarca { get; set; }

    public string Nombre { get; set; } = null!;

    public bool Estado { get; set; } = true;

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
