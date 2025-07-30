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
    public class ProveedoresController : Controller
    {
        private readonly MiAngelitoContext _context;

        public ProveedoresController(MiAngelitoContext context)
        {
            _context = context;
        }
        
        // GET: Proveedores
        public async Task<IActionResult> Index(string searchString)
        {
            var proveedores = from p in _context.Proveedores
                              select p;

            if (!string.IsNullOrEmpty(searchString))
            {
                proveedores = proveedores.Where(p =>
                    p.RazonSocial.Contains(searchString) ||
                    p.Ruc.Contains(searchString)
                );
            }

            ViewData["CurrentFilter"] = searchString;

            return View(await proveedores.OrderByDescending(c => c.Estado)
                .ThenBy(c => c.RazonSocial)
                .ToListAsync());
        }


        // GET: Proveedores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Proveedores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Ruc,RazonSocial,Telefono,Correo,Estado")] Proveedore proveedore)
        {
            if (ModelState.IsValid)
            {
                bool ExistProv = _context.Proveedores.Any(r => r.Ruc == proveedore.Ruc);

                if (ExistProv)
                {
                    ModelState.AddModelError("Ruc", "El RUC ya se encuentra registrado");
                    return View(proveedore);
                }

                _context.Add(proveedore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(proveedore);
        }


        // GET: Proveedores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proveedore = await _context.Proveedores.FindAsync(id);
            if (proveedore == null)
            {
                return NotFound();
            }
            
            return View(proveedore);
        }

        // POST: Proveedores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProveedor,Ruc,RazonSocial,Telefono,Correo,Estado")] Proveedore proveedore)
        {
            if (id != proveedore.IdProveedor)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool ExistProv = _context.Proveedores.Any(r => r.Ruc == proveedore.Ruc && r.IdProveedor != proveedore.IdProveedor);

                if (ExistProv)
                {
                    ModelState.AddModelError("Ruc", "El RUC ya se encuentra registrado");
                    return View(proveedore); // ← ¡RETORNAR AQUÍ!
                }

                try
                {
                    _context.Update(proveedore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProveedoreExists(proveedore.IdProveedor))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(proveedore);
        }


        // POST: Proveedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var proveedore = await _context.Proveedores.FindAsync(id);
            if (proveedore != null)
            {
                proveedore.Estado = !proveedore.Estado; 
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

       
     
        


        private bool ProveedoreExists(int id)
        {
            return _context.Proveedores.Any(e => e.IdProveedor == id);
        }
    }
}
