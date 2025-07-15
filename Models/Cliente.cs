using System;
using System.Collections.Generic;

namespace webBotica2.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string Documento { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string? ApellidoPaterno { get; set; }

    public string? ApellidoMaterno { get; set; }

    public string? Telefono { get; set; }

    public string? Correo { get; set; }

    public DateOnly? FechaNac { get; set; }

    public string? Direccion { get; set; }

    public bool Estado { get; set; }

    public bool EsRegistrado { get; set; }

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
