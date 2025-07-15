using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using webBotica2.Models;

namespace webBoticaAdmin.Controllers
{
    public class LaboratoriosController : Controller
    {
        private readonly MiAngelitoContext _context;

        public LaboratoriosController(MiAngelitoContext context)
        {
            _context = context;
        }

        // GET: Laboratorios
        public async Task<IActionResult> Index()
        {

            var laboratoriosActivos = await _context.Laboratorios
                .Where(l => l.Estado)
                .ToListAsync();

            return View(laboratoriosActivos);
        }

        // GET: Laboratorios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Laboratorios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdLaboratorio,Nombre,Ruc,Direccion,Telefono,Correo,Estado")] Laboratorio laboratorio)
        {
            if (ModelState.IsValid)
            {
                bool ExistLab = _context.Laboratorios.Any(x => x.Ruc == laboratorio.Ruc);
                if (ExistLab) {
                    ModelState.AddModelError("Ruc", "El ruc ya se encuentra en uso");
                    return View(laboratorio);
                }
                _context.Add(laboratorio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(laboratorio);
        }

        // GET: Laboratorios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laboratorio = await _context.Laboratorios.FindAsync(id);
            if (laboratorio == null)
            {
                return NotFound();
            }
            return View(laboratorio);
        }

        // POST: Laboratorios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdLaboratorio,Nombre,Ruc,Direccion,Telefono,Correo,Estado")] Laboratorio laboratorio)
        {
            if (id != laboratorio.IdLaboratorio)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bool ExistLab = _context.Laboratorios.Any(x => x.Ruc == laboratorio.Ruc && x.IdLaboratorio !=laboratorio.IdLaboratorio);
                    if (ExistLab)
                    {
                        ModelState.AddModelError("Ruc", "El ruc ya se encuentra en uso");
                        return View(laboratorio);
                    }
                    _context.Update(laboratorio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LaboratorioExists(laboratorio.IdLaboratorio))
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
            return View(laboratorio);
        }

        // GET: Laboratorios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laboratorio = await _context.Laboratorios
                .FirstOrDefaultAsync(m => m.IdLaboratorio == id);
            if (laboratorio == null)
            {
                return NotFound();
            }

            return View(laboratorio);
        }

        // POST: Laboratorios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var laboratorio = await _context.Laboratorios.FindAsync(id);
            if (laboratorio != null)
            {
                laboratorio.Estado = false;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LaboratorioExists(int id)
        {
            return _context.Laboratorios.Any(e => e.IdLaboratorio == id);
        }
    }
}
