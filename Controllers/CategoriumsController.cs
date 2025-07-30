using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using webBotica2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace webBotica2.Controllers;

[Authorize]
public class CategoriumsController : Controller
{
    private readonly MiAngelitoContext _context;

    public CategoriumsController(MiAngelitoContext context)
    {
        _context = context;
    }

    // GET: Categoriums
    public async Task<IActionResult> Index()
    {
        return View(await _context.Categoria.OrderByDescending(c => c.Estado)
        .ThenBy(c => c.Nombre)
        .ToListAsync());
    }


    // GET: Categoriums/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Categoriums/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("IdCategoria,Nombre,Estado")] Categorium categorium)
    {
        if (ModelState.IsValid)
        {
            var ExisteCat=_context.Categoria.Any(c => c.Nombre.ToLower().Trim()==categorium.Nombre.ToLower().Trim());

            if (ExisteCat)
            {
                ModelState.AddModelError("Nombre", "La categoria ya existe!");
                return View(categorium);
            }
            _context.Add(categorium);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(categorium);
    }

    // GET: Categoriums/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var categorium = await _context.Categoria.FindAsync(id);
        if (categorium == null)
        {
            return NotFound();
        }
        return View(categorium);
    }

    // POST: Categoriums/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("IdCategoria,Nombre,Estado")] Categorium categorium)
    {
        if (id != categorium.IdCategoria)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var ExisteCat = _context.Categoria.Any(c => c.Nombre.ToLower().Trim() == categorium.Nombre.ToLower().Trim() && c.IdCategoria!=categorium.IdCategoria);

                if (ExisteCat)
                {
                    ModelState.AddModelError("Nombre", "La categoria ya existe!");
                    return View(categorium);
                }
                _context.Update(categorium);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriumExists(categorium.IdCategoria))
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
        return View(categorium);
    }

    // GET: Categoriums/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var categorium = await _context.Categoria
            .FirstOrDefaultAsync(m => m.IdCategoria == id);
        if (categorium == null)
        {
            return NotFound();
        }

        return View(categorium);
    }

    // POST: Categoriums/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var categorium = await _context.Categoria.FindAsync(id);
        if (categorium != null)
        {
            categorium.Estado = !categorium.Estado;
            await _context.SaveChangesAsync();
        }

        
        return RedirectToAction(nameof(Index));
    }

    private bool CategoriumExists(int id)
    {
        return _context.Categoria.Any(e => e.IdCategoria == id);
    }
}
