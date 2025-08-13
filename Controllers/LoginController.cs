using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using webBotica2.Models;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

[AllowAnonymous]
public class LoginController : Controller
{
    private readonly MiAngelitoContext _db;
    public LoginController(MiAngelitoContext db) => _db = db;

    // GET /Login
    public IActionResult Index() => View("Index");

    // POST /Login
    [HttpPost]
    public async Task<IActionResult> Index(LoginViewModel lg)
    {
        if (!ModelState.IsValid)
            return View("Index", lg);

        var usuario = _db.Usuarios.Include(p =>p.IdRolNavigation)
                         .FirstOrDefault(u => u.Usuario1 == lg.username.Trim() &&
                                              u.Contraseña == lg.password);


        if (usuario == null)
        {
            ModelState.AddModelError(string.Empty, "Usuario o contraseña incorrectos");
            return View("Index", lg);
        }
        if (usuario.Estado == false)
        {
            ModelState.AddModelError(string.Empty, "El usuario ya no tiene acceso al sistema");
            return View("Index", lg);
        }

        // Guardar datos en sesión
        HttpContext.Session.SetString("User", usuario.Usuario1);
        HttpContext.Session.SetString("Nombre", usuario.Nombre);
        HttpContext.Session.SetString("Rol", usuario.IdRolNavigation.Rol1.ToLower().Trim());

        var claims = new[]
{
    new Claim(ClaimTypes.Name, usuario.Usuario1),
    new Claim(ClaimTypes.Role, usuario.IdRolNavigation.Rol1) 
};

       
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

       
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        return RedirectToAction("Index", "Home");  
    }

    public async Task<IActionResult> Logout()
    {
        // 🔒 Cerrar sesión de cookies
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        // ❌ Limpiar sesión
        HttpContext.Session.Clear();

        // 🔄 Redirigir al Login
        return RedirectToAction("Index", "Login");
    }
}
