using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webBotica2.Models;

namespace webBotica2.Controllers
{
    public class DashBoardController : Controller
    {
        private readonly MiAngelitoContext _context;

        public DashBoardController(MiAngelitoContext context)
        {
            _context = context;
        }

        public async Task< IActionResult> Index()
        {
            var ahora = DateTime.Now;
            var añoActual = ahora.Year;

            // --- Datos para gráfico mensual (promedio por mes del año actual) ---
            var datosMensuales = _context.Ventas
                .Where(v => v.Fecha.Year == añoActual)
                .GroupBy(v => v.Fecha.Month)
                .Select(g => new
                {
                    Mes = g.Key,
                    Promedio = Math.Round(g.Average(v => v.Total), 2)
                })
                .OrderBy(x => x.Mes)
                .ToList();

            var nombresMeses = new[]
            {
        "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio",
        "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"
    };

            ViewBag.LabelsMensuales = datosMensuales.Select(x => nombresMeses[x.Mes - 1]).ToList();
            ViewBag.DataMensuales = datosMensuales.Select(x => x.Promedio).ToList();

            // --- Datos para gráfico anual (promedio por año disponible) ---
            var datosAnuales = _context.Ventas
                .GroupBy(v => v.Fecha.Year)
                .Select(g => new
                {
                    Anio = g.Key,
                    Promedio = Math.Round(g.Average(v => v.Total), 2)
                })
                .OrderBy(x => x.Anio)
                .ToList();

            ViewBag.LabelsAnuales = datosAnuales.Select(x => x.Anio.ToString()).ToList();
            ViewBag.DataAnuales = datosAnuales.Select(x => x.Promedio).ToList();

            // --- Recaudado este mes ---
            ViewBag.TotalMes = _context.Ventas
                .Where(v => v.Fecha.Month == ahora.Month && v.Fecha.Year == ahora.Year)
                .Sum(v => (decimal?)v.Total) ?? 0;

            // --- Recaudado este año ---
            ViewBag.TotalAnio = _context.Ventas
                .Where(v => v.Fecha.Year == ahora.Year)
                .Sum(v => (decimal?)v.Total) ?? 0;

            // --- Porcentaje cumplimiento ---

            var parametros = await _context.ParametrosGenerales.FirstOrDefaultAsync();

            if (parametros != null)
            {
                decimal metaMensual = parametros.GananciaMinimaMensual;
                decimal metaAnual = parametros.GananciaMinimaAnual;

                decimal totalMes = ViewBag.TotalMes ?? 0m;
                decimal totalAnio = ViewBag.TotalAnio ?? 0m;

                ViewBag.CumplimientoMensual = metaMensual > 0
                    ? Math.Min((totalMes / metaMensual) * 100, 100)
                    : 0;

                ViewBag.CumplimientoAnual = metaAnual > 0
                    ? Math.Min((totalAnio / metaAnual) * 100, 100)
                    : 0;
            }
            else
            {
                // Si no hay parámetros, asumimos 0%
                ViewBag.CumplimientoMensual = 0;
                ViewBag.CumplimientoAnual = 0;
            }

            return View();
        }


    }
}
