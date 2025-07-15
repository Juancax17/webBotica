using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using webBotica2.Models;

namespace webBotica2.Controllers
{
    public class DetalleVentumsController : Controller
    {
        private readonly MiAngelitoContext _context;

        public DetalleVentumsController(MiAngelitoContext context)
        {
            _context = context;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(RegistrarVenta model)
        {
            if (model.Detalles == null || !model.Detalles.Any())
            {
                ModelState.AddModelError("", "Debe agregar al menos un producto.");
                return View(model);
            }

            // Aquí podrías buscar el cliente por DNI/RUC si lo tuvieras
            // Por simplicidad, omito esa parte

            var venta = new Venta
            {
                Fecha = DateOnly.FromDateTime(DateTime.Now),
                IdCliente = null, 
                Total = model.Detalles.Sum(d => d.Subtotal),
                EstadoVenta = "Pagada"
            };

            _context.Ventas.Add(venta);
            await _context.SaveChangesAsync(); 

            foreach (var detalle in model.Detalles)
            {
                var nuevoDetalle = new DetalleVentum
                {
                    IdVenta = venta.IdVenta,
                    IdProd = detalle.IdProd,
                    Cant = detalle.Cant,
                    PrecioVenta = detalle.PrecioVenta,
                    Subtotal = detalle.Subtotal
                };

                _context.DetalleVenta.Add(nuevoDetalle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Ventas"); 
        }
       
    }
}
