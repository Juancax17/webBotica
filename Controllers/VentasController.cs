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
    public class VentasController : Controller
    {
        private readonly MiAngelitoContext _context;

        public VentasController(MiAngelitoContext context)
        {
            _context = context;
        }

        // GET: Ventas
        public async Task<IActionResult> Index()
        {
            var miAngelitoContext = _context.Ventas.Include(v => v.IdClienteNavigation);
            return View(await miAngelitoContext.ToListAsync());
        }

        


    }
}
