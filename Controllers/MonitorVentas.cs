using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webBotica2.Models;

namespace webBotica2.Controllers
{
    public class MonitorVentas : Controller
    {

        private readonly MiAngelitoContext _context;

        public MonitorVentas(MiAngelitoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var ventas = await _context.Ventas
                .Include(v => v.IdClienteNavigation)
                .Include(v => v.Comprobantes)
                .Include(v => v.Comprobantes)
                .Include(v => v.DetalleVenta)                
                .ThenInclude(d => d.IdProdNavigation)
                .OrderByDescending(v => v.Fecha)
                .ToListAsync();

            return View(ventas);
        }

        [HttpGet]
        public async Task<IActionResult> BuscarVenta(string filtro, DateTime? fechaInicio, DateTime? fechaFin)
        {
            var ventasQuery = _context.Ventas
                .Include(v => v.IdClienteNavigation)
                .Include(v => v.DetalleVenta)
                    .ThenInclude(d => d.IdProdNavigation)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filtro))
            {
                filtro = filtro.Trim();

                if (int.TryParse(filtro, out int idVenta))
                {
                    ventasQuery = ventasQuery.Where(v => v.IdVenta == idVenta);
                }
                else
                {
                    ventasQuery = ventasQuery.Where(v => v.IdClienteNavigation.Documento.Contains(filtro));
                }
            }

            // Filtro por fechas
            if (fechaInicio.HasValue)
            {
                DateOnly inicio = DateOnly.FromDateTime(fechaInicio.Value);
                ventasQuery = ventasQuery.Where(v => v.Fecha >= inicio);
            }

            if (fechaFin.HasValue)
            {
                DateOnly fin = DateOnly.FromDateTime(fechaFin.Value);
                ventasQuery = ventasQuery.Where(v => v.Fecha <= fin);
            }


            var ventasFiltradas = await ventasQuery.OrderByDescending(v => v.Fecha).ToListAsync();

            // Mantener filtros en la vista
            ViewBag.Filtro = filtro;
            ViewBag.FechaInicio = fechaInicio?.ToString("yyyy-MM-dd");
            ViewBag.FechaFin = fechaFin?.ToString("yyyy-MM-dd");

            return View("Index", ventasFiltradas);
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarEstadoVenta(int IdVenta, string EstadoVenta)
        {
            var venta = await _context.Ventas.FindAsync(IdVenta);
            if (venta == null)
                return NotFound();

            venta.EstadoVenta = EstadoVenta;
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

       




    }
}