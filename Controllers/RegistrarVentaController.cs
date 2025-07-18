using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using webBotica2.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace webBotica2.Controllers
{
    public class RegistrarVentaController : Controller
    {
        private readonly MiAngelitoContext _context;

        public RegistrarVentaController(MiAngelitoContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var model = new RegistrarVenta
            {
                TipoDocumento = "DNI", // Valor por defecto
                TiposDocumento = new List<SelectListItem>
        {
            new SelectListItem { Text = "DNI", Value = "DNI" },
            new SelectListItem { Text = "RUC", Value = "RUC" }
        },
                Productos = await _context.Productos
                    .Where(p => p.Estado == true)
                    .Select(p => new SelectListItem
                    {
                        Text = p.Nombre,
                        Value = p.IdProd.ToString()
                    }).ToListAsync()
            };

            // 🔽 ESTA ES LA CLAVE para que tu JS funcione
            ViewBag.ProductosModal = await _context.Productos
                .Where(p => p.Estado == true)
                .Select(p => new ProductoVM
                {
                    Id = p.IdProd,
                    Sku = p.Sku,
                    Nombre = p.Nombre,
                    Precio = p.PrecioVenta,
                    Stock = p.Stock
                }).ToListAsync();

            var igv = await _context.ParametrosGenerales.Where(p => p.Id == 2).FirstOrDefaultAsync();
      
            ViewBag.igv = (igv?.Igv ?? 0);

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Index(RegistrarVenta model)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Hubo errores en el formulario. Por favor, verifica los datos ingresados.";

                model.TiposDocumento = new List<SelectListItem>
                {
            new SelectListItem { Text = "DNI", Value = "DNI" },
            new SelectListItem { Text = "RUC", Value = "RUC" }
        };

                model.Productos = await _context.Productos
                    .Where(p => p.Estado == true)
                    .Select(p => new SelectListItem
                    {
                        Text = p.Nombre,
                        Value = p.IdProd.ToString()
                    }).ToListAsync();

                ViewBag.ProductosModal = await _context.Productos
                .Where(p => p.Estado == true)
                .Select(p => new ProductoVM
                {
                    Id = p.IdProd,
                    Sku = p.Sku,
                    Nombre = p.Nombre,
                    Precio = p.PrecioVenta,
                    Stock = p.Stock
                }).ToListAsync();

                return View(model);
            }

            foreach (var item in model.Detalles)
            {
                var prod = await _context.Productos.FindAsync(item.IdProd);

                if (prod.Stock < item.Cant)
                {
                    TempData["error"] = prod.Nombre + " stock insuficiente";

                    model.TiposDocumento = new List<SelectListItem>
                {
                    new SelectListItem { Text = "DNI", Value = "DNI" },
                    new SelectListItem { Text = "RUC", Value = "RUC" }
                };

                    model.Productos = await _context.Productos
                        .Where(p => p.Estado == true)
                        .Select(p => new SelectListItem
                        {
                            Text = p.Nombre,
                            Value = p.IdProd.ToString()
                        }).ToListAsync();

                    ViewBag.ProductosModal = await _context.Productos
                        .Where(p => p.Estado == true)
                        .Select(p => new ProductoVM
                        {
                            Id = p.IdProd,
                            Sku = p.Sku,
                            Nombre = p.Nombre,
                            Precio = p.PrecioVenta,
                            Stock = p.Stock
                        }).ToListAsync();

                    return View(model);
                }
            }

           var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Documento == model.NumeroDocumento);

            if (cliente == null)
            {
                if (Request.Form["NombreCliente"]=="" )
                {
                    TempData["error"] = "Por Favor Primero Complete la información del cliente";

                    model.TiposDocumento = new List<SelectListItem>
                {
            new SelectListItem { Text = "DNI", Value = "DNI" },
            new SelectListItem { Text = "RUC", Value = "RUC" }
        };

                    model.Productos = await _context.Productos
                        .Where(p => p.Estado == true)
                        .Select(p => new SelectListItem
                        {
                            Text = p.Nombre,
                            Value = p.IdProd.ToString()
                        }).ToListAsync();

                    ViewBag.ProductosModal = await _context.Productos
                    .Where(p => p.Estado == true)
                    .Select(p => new ProductoVM
                    {
                        Id = p.IdProd,
                        Sku = p.Sku,
                        Nombre = p.Nombre,
                        Precio = p.PrecioVenta,
                        Stock = p.Stock
                    }).ToListAsync();

                    return View(model);
                }

                foreach (var item in model.Detalles)
                {
                    var prod = await _context.Productos.FindAsync(item.IdProd);

                    if (prod.Stock < item.Cant)
                    {
                        TempData["error"] = prod.Nombre + " stock insuficiente";

                        model.TiposDocumento = new List<SelectListItem>
                {
                    new SelectListItem { Text = "DNI", Value = "DNI" },
                    new SelectListItem { Text = "RUC", Value = "RUC" }
                };

                        model.Productos = await _context.Productos
                            .Where(p => p.Estado == true)
                            .Select(p => new SelectListItem
                            {
                                Text = p.Nombre,
                                Value = p.IdProd.ToString()
                            }).ToListAsync();

                        ViewBag.ProductosModal = await _context.Productos
                            .Where(p => p.Estado == true)
                            .Select(p => new ProductoVM
                            {
                                Id = p.IdProd,
                                Sku = p.Sku,
                                Nombre = p.Nombre,
                                Precio = p.PrecioVenta,
                                Stock = p.Stock
                            }).ToListAsync();

                        return View(model);
                    }
                }
                
                var nuevoCliente = new Cliente
                {

                    Documento = model.NumeroDocumento,
                    Nombre = Request.Form["NombreCliente"],
                    ApellidoPaterno = "",
                    ApellidoMaterno = "",
                    Telefono = Request.Form["Telefono"],
                    Correo = Request.Form["Email"],
                    FechaNac = null,
                    Direccion = "",
                    Estado = true,
                    EsRegistrado = false,
                };
                cliente = nuevoCliente;

                _context.Clientes.Add(nuevoCliente);
                await _context.SaveChangesAsync();

            }

            var igv = await _context.ParametrosGenerales.Where(p => p.Id == 2).FirstOrDefaultAsync();
            ViewBag.IgvDebug = igv?.Igv ?? 0;

            ViewBag.igv = (igv?.Igv ?? 0);



            //  Crear nueva venta
            var venta = new Venta
            {
                IdCliente = cliente.IdCliente,
                Fecha = DateOnly.FromDateTime(DateTime.Now),
                EstadoVenta = "Pendiente",      
                Total = model.Detalles.Sum(d => d.Subtotal)
            };

            if (model.Detalles == null || !model.Detalles.Any())
            {
                TempData["error"] = "Debes agregar al menos un producto para registrar la venta.";

                model.TiposDocumento = new List<SelectListItem>
                {
                    new SelectListItem { Text = "DNI", Value = "DNI" },
                    new SelectListItem { Text = "RUC", Value = "RUC" }
                };

                model.Productos = await _context.Productos
                    .Where(p => p.Estado == true)
                    .Select(p => new SelectListItem
                    {
                        Text = p.Nombre,
                        Value = p.IdProd.ToString()
                    }).ToListAsync();

                ViewBag.ProductosModal = await _context.Productos
                    .Where(p => p.Estado == true)
                    .Select(p => new ProductoVM
                    {
                        Id = p.IdProd,
                        Sku = p.Sku,
                        Nombre = p.Nombre,
                        Precio = p.PrecioVenta,
                        Stock = p.Stock
                    }).ToListAsync();

                return View(model);
            }

            //  Añadir los detalles de la venta
            foreach (var item in model.Detalles)
            {
                venta.DetalleVenta.Add(new DetalleVentum
                {
                    IdProd = item.IdProd,
                    Cant = item.Cant,
                    PrecioVenta = item.PrecioVenta,
                    Subtotal = item.Subtotal
                });

      
                var prod = await _context.Productos.FindAsync(item.IdProd);

               

                if (prod != null)
                {
                    prod.Stock -= item.Cant;
                }
            }



            _context.Ventas.Add(venta);
            await _context.SaveChangesAsync();

            TempData["success"] = "Venta registrada correctamente.";
            return RedirectToAction("Index");
        }


        [HttpGet("RegistrarVenta/EnviarTelefono")]
        public async Task<IActionResult> ConsultarTelefono([FromQuery] string docu)
        {
            var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Documento == docu);

            if (cliente != null)
            {
                return Json(new
                {
                    telefono = cliente.Telefono ?? "",
                    correo = cliente.Correo ?? ""
                });
            }

            return Json(null);
        }



    }
}
