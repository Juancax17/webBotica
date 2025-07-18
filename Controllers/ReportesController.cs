using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webBotica2.Models;

namespace webBotica2.Controllers
{
    public class ReportesController : Controller
    {
        private readonly MiAngelitoContext _context;

        public ReportesController(MiAngelitoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var hoy = DateOnly.FromDateTime(DateTime.Today);
            var diasVencimiento = 30;

            // Productos con bajo stock
            var bajoStock = await _context.Productos
                .Where(p => p.Stock <= p.StockMinimo && p.Estado)
                .ToListAsync();

            // Productos vencidos
            var vencidos = await _context.Productos
                .Where(p => p.FechaVen < hoy && p.Estado)
                .ToListAsync();

            // Productos por vencer
            var porVencer = await _context.Productos
                .Where(p => p.FechaVen >= hoy && p.FechaVen <= hoy.AddDays(diasVencimiento) && p.Estado)
                .ToListAsync();

            // Top 10 productos más vendidos
            var topVendidos = await _context.DetalleVenta
                .GroupBy(d => d.IdProd)
                .Select(g => new
                {
                    IdProd = g.Key,
                    TotalVendidos = g.Sum(x => x.Cant)
                })
                .OrderByDescending(g => g.TotalVendidos)
                .Take(10)
                .Join(_context.Productos,
                    g => g.IdProd,
                    p => p.IdProd,
                    (g, p) => new
                    {
                        p.Nombre,
                        g.TotalVendidos
                    })
                .ToListAsync();

            // Enviar datos a la vista
            ViewBag.BajoStock = bajoStock;
            ViewBag.Vencidos = vencidos;
            ViewBag.PorVencer = porVencer;
            ViewBag.TopVendidos = topVendidos;

            return View();
        }
    }


}
