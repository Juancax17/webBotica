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
    public class ParametrosGeneralesController : Controller
    {
        private readonly MiAngelitoContext _context;

        public ParametrosGeneralesController(MiAngelitoContext context)
        {
            _context = context;
        }

        // GET: ParametrosGenerales
        public async Task<IActionResult> Index()
        {
            var parametros = await _context.ParametrosGenerales.FirstOrDefaultAsync();
            if (parametros == null)
            {
               
                return View();
            }
            return View(parametros);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ParametrosGenerales parametrosActualizados)
        {
            if (!ModelState.IsValid)
            {
                return View(parametrosActualizados);
            }

            var parametros = await _context.ParametrosGenerales.FirstOrDefaultAsync();
            if (parametros == null)
            {
                return NotFound("No se encontró el registro de parámetros.");
            }

            // Actualizamos los valores uno por uno
            parametros.ruc = parametrosActualizados.ruc;
            parametros.NombreEmporesa = parametrosActualizados.NombreEmporesa;
            parametros.Igv = parametrosActualizados.Igv;
            parametros.GananciaMinimaMensual = parametrosActualizados.GananciaMinimaMensual;
            parametros.GananciaMinimaAnual = parametrosActualizados.GananciaMinimaAnual;
            parametros.DiasVencimientoMinima = parametrosActualizados.DiasVencimientoMinima;
            parametros.modoOscuro = parametrosActualizados.modoOscuro;


            if (Request.Form.Files["LogoSistema"] is IFormFile logo && logo.Length > 0)
            {
                var carpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img");
                Directory.CreateDirectory(carpeta);

                var nombreArchivo = "logo_sistema" + Path.GetExtension(logo.FileName);
                var rutaArchivo = Path.Combine(carpeta, nombreArchivo);

                // Eliminar archivo anterior si existe
                if (System.IO.File.Exists(rutaArchivo))
                {
                    System.IO.File.Delete(rutaArchivo);
                }

                using (var stream = new FileStream(rutaArchivo, FileMode.Create))
                {
                    await logo.CopyToAsync(stream);
                }

                parametros.LogoSistema = "/img/" + nombreArchivo;
            }





            await _context.SaveChangesAsync();

            TempData["Mensaje"] = "Parámetros actualizados correctamente.";
            return RedirectToAction(nameof(Index));
        }
    }
}
