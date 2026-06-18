using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class EventoController : Controller
    {
        public IActionResult ListEvento()
        {
            return View();
        }
    }
}
