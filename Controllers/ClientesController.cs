using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using webBotica2.Models;

namespace webBotica.Controllers
{
    public class ClientesController : Controller
    {
        private readonly MiAngelitoContext _context;

        public ClientesController(MiAngelitoContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
             return View(await _context.Clientes
                .OrderByDescending(c => c.Estado) 
                .ThenBy(c => c.Nombre)            
                .ToListAsync());
        }

       

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCliente,Documento,Nombre,ApellidoPaterno,ApellidoMaterno,Telefono,Correo,FechaNac,Direccion,Estado,EsRegistrado")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                var hoy = DateOnly.FromDateTime(DateTime.Today);
                var fechaLimite = hoy.AddYears(-18);
                var fechaLimite2 = hoy.AddYears(-100);

                if (cliente.FechaNac > fechaLimite)
                {
                    ModelState.AddModelError("FechaNac", "El cliente debe ser mayor de 18 años.");
                    return View(cliente);
                } else if (cliente.FechaNac < fechaLimite2)
                {
                    ModelState.AddModelError("FechaNac", "La fecha de nacimiento no es creible");
                    return View(cliente);
                }

                    bool ExistCli = await _context.Clientes.AnyAsync(r => r.Documento == cliente.Documento);

                if (ExistCli)
                {
                    ModelState.AddModelError("Documento", "El Documento ya se encuentra registrado");
                    return View(cliente); 
                }
               
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCliente,Documento,Nombre,ApellidoPaterno,ApellidoMaterno,Telefono,Correo,FechaNac,Direccion,Estado,EsRegistrado")] Cliente cliente)
        {
            if (id != cliente.IdCliente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var hoy = DateOnly.FromDateTime(DateTime.Today);
                    var fechaLimite = hoy.AddYears(-18);
                    var fechaLimite2 = hoy.AddYears(-100);

                    if (cliente.FechaNac > fechaLimite)
                    {
                        ModelState.AddModelError("FechaNac", "El cliente debe ser mayor de 18 años.");
                        return View(cliente);
                    }
                    else if (cliente.FechaNac < fechaLimite2)
                    {
                        ModelState.AddModelError("FechaNac", "La fecha de nacimiento no es creíble");
                        return View(cliente);
                    }

                    bool ExistCli = await _context.Clientes
                        .AnyAsync(r => r.Documento == cliente.Documento && r.IdCliente != cliente.IdCliente);

                    if (ExistCli)
                    {
                        ModelState.AddModelError("Documento", "El Documento ya se encuentra registrado");
                        return View(cliente);
                    }

                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.IdCliente))
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
            return View(cliente);
        }


        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                cliente.Estado = !cliente.Estado;
                await _context.SaveChangesAsync();
            }

           
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.IdCliente == id);
        }
    }
}
