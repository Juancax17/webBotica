using Microsoft.AspNetCore.Mvc;

namespace webBotica2.Controllers
{
    public class DashBoardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
}
