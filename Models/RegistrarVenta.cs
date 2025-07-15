using Microsoft.AspNetCore.Mvc.Rendering;

namespace webBotica2.Models
{
    public class RegistrarVenta
    {
        public string TipoDocumento { get; set; } // ← el valor seleccionado del combo
        public List<SelectListItem> TiposDocumento { get; set; } = new(); // ← las opciones del combo

        public string NumeroDocumento { get; set; }
        public List<SelectListItem> Productos { get; set; } = new();
        public List<DetalleVentum> Detalles { get; set; } = new();
    }

}
