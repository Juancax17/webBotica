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
                .Include(v => v.DetalleVenta)
                .ThenInclude(d => d.IdProdNavigation)
                .OrderByDescending(v => v.Fecha)
                .ToListAsync();

            return View(ventas);
        }

        [HttpGet]
        public async Task<IActionResult> BuscarVenta(string filtro)
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
                    // Comparación exacta por ID de venta
                    ventasQuery = ventasQuery.Where(v => v.IdVenta == idVenta);
                }
                else
                {
                    // Comparación por documento exacto
                    ventasQuery = ventasQuery.Where(v => v.IdClienteNavigation.Documento.Contains(filtro));
                }
            }

            var ventas = await ventasQuery
                .OrderByDescending(v => v.Fecha)
                .ToListAsync();

            ViewBag.Filtro = filtro;
            return View("Index", ventas);
        }


    }
}
