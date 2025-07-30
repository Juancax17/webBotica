using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webBotica2.Models;
using Rotativa.AspNetCore;

namespace webBotica2.Controllers
{
    public class ComprobantesController : Controller
    {
        private readonly MiAngelitoContext _context;

        public ComprobantesController(MiAngelitoContext context)
        {
            _context = context;
        }

        // GET: Comprobantes
        public async Task<IActionResult> Index()
        {
            var miAngelitoContext = _context.Comprobantes.Include(c => c.IdVentaNavigation).Include(c => c.IdComprobante);
            return View(await miAngelitoContext.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> CrearComprobante(int idVenta)
        {
            try
            {
                var venta = await _context.Ventas
                    .Include(v => v.IdClienteNavigation)
                    .Include(v => v.Comprobantes)
                    .FirstOrDefaultAsync(v => v.IdVenta == idVenta);

                

                if (venta == null) return NotFound();

                var tipo = venta.IdClienteNavigation.Documento.Trim().Length == 8 ? "Boleta" : "Factura";

                var comprobante = new Comprobante
                {
                    Tipo = tipo,
                    Numero = GenerarNumeroComprobante(tipo),
                    Fecha = DateOnly.FromDateTime(DateTime.Today),
                    IdVenta = idVenta,
                    Estado = "Emitido"
                };

                _context.Comprobantes.Add(comprobante);
                await _context.SaveChangesAsync();
                TempData["MensajeExito"] = "✅ Comprobante generado exitosamente.";
               
                return RedirectToAction("Index", "MonitorVentas"); 
            }
            catch (Exception ex)
            {
                // Para depurar en desarrollo
                return Content($"Ocurrió un error: {ex.Message}");
            }
        }


        private string GenerarNumeroComprobante(string tipo)
        {
            int count = _context.Comprobantes.Count(c => c.Tipo == tipo) + 1;

            if (tipo == "Boleta")
            {
                return $"BO-{count.ToString("D4")}";
            }
            else // Factura
            {
                return $"FA-{count.ToString("D4")}";
            }
        }

        public async Task<IActionResult> GenerarPDF(int idComprobante)
        {
            var comprobante = await _context.Comprobantes
                .Include(c => c.IdVentaNavigation)
                    .ThenInclude(v => v.IdClienteNavigation)
                .Include(c => c.IdVentaNavigation)
                    .ThenInclude(v => v.DetalleVenta)
                        .ThenInclude(dv => dv.IdProdNavigation)
                .FirstOrDefaultAsync(c => c.IdComprobante == idComprobante);

            if (comprobante == null)
                return NotFound("Comprobante no encontrado");

            return new ViewAsPdf("VistaPDF", comprobante)
            {
                //FileName = $"Comprobante_{comprobante.Numero}.pdf"
            };
        }
    }
}
