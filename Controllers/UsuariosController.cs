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
    public class UsuariosController : Controller
    {
        private readonly MiAngelitoContext _context;

        public UsuariosController(MiAngelitoContext context)
        { 
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            var miAngelitoContext = _context.Usuarios.Include(p => p.IdRolNavigation);
            return View(await miAngelitoContext.ToListAsync());
        }

 

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            ViewData["IdRol"]= new SelectList(_context.Rols, "IdRol", "Rol1" );
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUser,Nombre,Apellido,Usuario1,Contraseña,IdRol,Estado")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                bool ExistUser = _context.Usuarios.Any(p => p.Usuario1.ToLower() == usuario.Usuario1.ToLower());
                if ( ExistUser)
                {
                    ModelState.AddModelError("Usuario1", "El usuario ya existe");
                    ViewData["IdRol"] = new SelectList(_context.Rols, "IdRol", "Rol1", usuario.IdRol);
                    return View(usuario);
                }
                if (usuario.Contraseña.Length<=5)
                {
                    ViewData["IdRol"] = new SelectList(_context.Rols, "IdRol", "Rol1", usuario.IdRol);
                    ModelState.AddModelError("Contraseña", "La Contraseña debe tener minimo 6 caracteres");
                    return View(usuario);
                }
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdRol"] = new SelectList(_context.Rols, "IdRol", "Rol1", usuario.IdRol);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var usuario = await _context.Usuarios.FindAsync(id);
                if (usuario == null)
                {
                    return NotFound();
                }

                ViewData["IdRol"] = new SelectList(_context.Rols, "IdRol", "Rol1", usuario.IdRol);
                return View(usuario);
            } catch(Exception e)
            {
                return NotFound();
            }
            
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUser,Nombre,Apellido,Usuario1,Contraseña,IdRol,Estado")] Usuario usuario)
        {
            if (id != usuario.IdUser)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bool ExistUser = _context.Usuarios.Any(p => p.Usuario1.ToLower() == usuario.Usuario1.ToLower() && p.IdUser!=usuario.IdUser);
                    if (ExistUser)
                    {
                        ModelState.AddModelError("Usuario1", "El usuario ya existe");
                        ViewData["IdRol"] = new SelectList(_context.Rols, "IdRol", "Rol1", usuario.IdRol);
                        return View(usuario);
                    }
                    if (usuario.Contraseña.Length <= 5)
                    {
                        ViewData["IdRol"] = new SelectList(_context.Rols, "IdRol", "Rol1", usuario.IdRol);
                        ModelState.AddModelError("Contraseña", "La Contraseña debe tener minimo 6 caracteres");
                        return View(usuario);
                    }
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.IdUser))
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
            ViewData["IdRol"] = new SelectList(_context.Rols, "IdRol", "Rol1", usuario.IdRol);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.IdUser == id);
            if (usuario == null)
            {
                return NotFound();
            }
            ViewData["IdRol"] = new SelectList(_context.Rols, "IdRol", "Rol1", usuario.IdRol);
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                usuario.Estado = false;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.IdUser == id);
        }
    }
}
