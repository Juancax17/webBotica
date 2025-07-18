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
    public class ProductoesController : Controller
    {
        private readonly MiAngelitoContext _context;

        public ProductoesController(MiAngelitoContext context)
        {
            _context = context;
        }

        // GET: Productoes
        public async Task<IActionResult> Index()
        {
            var productos = await _context.Productos
        .Include(p => p.IdCategoriaNavigation)
        .Include(p => p.IdMarcaNavigation)
        .Include(p => p.IdProveedorNavigation)
        .Include(p => p.IdLaboratorioNavigation)
        .ToListAsync();

            return View(productos);


        }



        // GET: Productoes/Create
        public IActionResult Create()
        {
            ViewData["IdCategoria"] = new SelectList(_context.Categoria.Where(p=>p.Estado==true), "IdCategoria", "Nombre");
            ViewData["IdMarca"] = new SelectList(_context.Marcas.Where(p => p.Estado == true), "IdMarca", "Nombre");
            ViewData["IdProveedor"] = new SelectList(_context.Proveedores.Where(p => p.Estado == true), "IdProveedor", "RazonSocial");
            ViewData["IdLaboratorio"] = new SelectList(_context.Laboratorios.Where(p => p.Estado == true), "IdLaboratorio", "Nombre");
            
            return View();
        }

        // POST: Productoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProd,Sku,Nombre,Presentacion,PrecioCompra,PrecioVenta,FechaVen,Stock,StockMinimo,IdProveedor,IdLaboratorio,IdCategoria,IdMarca,Foto,Estado")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                var ExistProd = _context.Productos.Any(p => p.Sku == producto.Sku);

                if (ExistProd)
                {
                    ModelState.AddModelError("Sku", "El producto ya existe");
                    ViewData["IdCategoria"] = new SelectList(_context.Categoria.Where(p => p.Estado == true), "IdCategoria", "Nombre", producto.IdCategoria);
                    ViewData["IdMarca"] = new SelectList(_context.Marcas.Where(p => p.Estado == true), "IdMarca", "Nombre", producto.IdMarca);
                    ViewData["IdProveedor"] = new SelectList(_context.Proveedores.Where(p => p.Estado == true), "IdProveedor", "RazonSocial", producto.IdProveedor);
                    ViewData["IdLaboratorio"] = new SelectList(_context.Laboratorios.Where(p => p.Estado == true), "IdLaboratorio", "Nombre", producto.IdLaboratorio);
                    return View(producto);
                }

                var validarFecha = await _context.ParametrosGenerales.FirstOrDefaultAsync();

                var numeroF = producto.FechaVen.ToDateTime(TimeOnly.MinValue) - DateTime.Now;

                if (numeroF.TotalDays < validarFecha.DiasVencimientoMinima)
                {
                    ModelState.AddModelError("FechaVen", "La Fecha de vencimiento es muy pronta, por favor actualizar");
                    ViewData["IdCategoria"] = new SelectList(_context.Categoria.Where(p => p.Estado == true), "IdCategoria", "Nombre", producto.IdCategoria);
                    ViewData["IdMarca"] = new SelectList(_context.Marcas.Where(p => p.Estado == true), "IdMarca", "Nombre", producto.IdMarca);
                    ViewData["IdProveedor"] = new SelectList(_context.Proveedores.Where(p => p.Estado == true), "IdProveedor", "RazonSocial", producto.IdProveedor);
                    ViewData["IdLaboratorio"] = new SelectList(_context.Laboratorios.Where(p => p.Estado == true), "IdLaboratorio", "Nombre", producto.IdLaboratorio);
                    return View(producto);
                }


                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categoria.Where(p => p.Estado == true), "IdCategoria", "Nombre", producto.IdCategoria);
            ViewData["IdMarca"] = new SelectList(_context.Marcas.Where(p => p.Estado == true), "IdMarca", "Nombre", producto.IdMarca);
            ViewData["IdProveedor"] = new SelectList(_context.Proveedores.Where(p => p.Estado == true), "IdProveedor", "RazonSocial", producto.IdProveedor);
            ViewData["IdLaboratorio"] = new SelectList(_context.Laboratorios.Where(p => p.Estado == true), "IdLaboratorio", "Nombre", producto.IdLaboratorio);
            return View(producto);
        }

        // GET: Productoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categoria.Where(p => p.Estado == true), "IdCategoria", "Nombre", producto.IdCategoria);
            ViewData["IdMarca"] = new SelectList(_context.Marcas.Where(p => p.Estado == true), "IdMarca", "Nombre", producto.IdMarca);
            ViewData["IdProveedor"] = new SelectList(_context.Proveedores.Where(p => p.Estado == true), "IdProveedor", "RazonSocial", producto.IdProveedor);
            ViewData["IdLaboratorio"] = new SelectList(_context.Laboratorios.Where(p => p.Estado == true), "IdLaboratorio", "Nombre", producto.IdLaboratorio);
            return View(producto);
        }

        // POST: Productoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProd,Sku,Nombre,Presentacion,PrecioCompra,PrecioVenta,FechaVen,Stock,StockMinimo,IdProveedor,IdLaboratorio,IdCategoria,IdMarca,Foto,Estado")] Producto producto)
        {
            if (id != producto.IdProd)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bool ExistProd = _context.Productos.Any(p => p.Sku ==producto.Sku && p.IdProd!=producto.IdProd);
                    if (ExistProd )
                    {
                        ModelState.AddModelError("Sku", "El producto ya existe");
                        ViewData["IdCategoria"] = new SelectList(_context.Categoria.Where(p => p.Estado == true), "IdCategoria", "Nombre", producto.IdCategoria);
                        ViewData["IdMarca"] = new SelectList(_context.Marcas.Where(p => p.Estado == true), "IdMarca", "Nombre", producto.IdMarca);
                        ViewData["IdProveedor"] = new SelectList(_context.Proveedores.Where(p => p.Estado == true), "IdProveedor", "RazonSocial", producto.IdProveedor);
                        ViewData["IdLaboratorio"] = new SelectList(_context.Laboratorios.Where(p => p.Estado == true), "IdLaboratorio", "Nombre", producto.IdLaboratorio);
                        return View(producto);
                    }

                    var validarFecha = await _context.ParametrosGenerales.FirstOrDefaultAsync();

                    var numeroF = producto.FechaVen.ToDateTime(TimeOnly.MinValue) - DateTime.Now;

                    if (numeroF.TotalDays < validarFecha.DiasVencimientoMinima)
                    {
                        ModelState.AddModelError("FechaVen", "La Fecha de vencimiento es muy pronta, por favor actualizar");
                        ViewData["IdCategoria"] = new SelectList(_context.Categoria.Where(p => p.Estado == true), "IdCategoria", "Nombre", producto.IdCategoria);
                        ViewData["IdMarca"] = new SelectList(_context.Marcas.Where(p => p.Estado == true), "IdMarca", "Nombre", producto.IdMarca);
                        ViewData["IdProveedor"] = new SelectList(_context.Proveedores.Where(p => p.Estado == true), "IdProveedor", "RazonSocial", producto.IdProveedor);
                        ViewData["IdLaboratorio"] = new SelectList(_context.Laboratorios.Where(p => p.Estado == true), "IdLaboratorio", "Nombre", producto.IdLaboratorio);
                        return View(producto);
                    }

                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.IdProd))
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
            ViewData["IdCategoria"] = new SelectList(_context.Categoria.Where(p => p.Estado == true), "IdCategoria", "Nombre", producto.IdCategoria);
            ViewData["IdMarca"] = new SelectList(_context.Marcas.Where(p => p.Estado == true), "IdMarca", "Nombre", producto.IdMarca);
            ViewData["IdProveedor"] = new SelectList(_context.Proveedores.Where(p => p.Estado == true), "IdProveedor", "RazonSocial", producto.IdProveedor);
            ViewData["IdLaboratorio"] = new SelectList(_context.Laboratorios.Where(p => p.Estado == true), "IdLaboratorio", "Nombre", producto.IdLaboratorio);
            return View(producto);
        }


        // POST: Productoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                producto.Estado = !producto.Estado;
                await _context.SaveChangesAsync();
            }

          
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.IdProd == id);
        }
    }
}
