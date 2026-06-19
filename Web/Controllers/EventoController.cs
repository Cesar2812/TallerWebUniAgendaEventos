using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Services.ServiceCategoria;
using Web.Services.ServiceEvento;

namespace Web.Controllers;

public class EventoController : Controller
{
    private readonly IServiceEvento _servicioEvento;
    private readonly IServiceCategoria _servicioCategory;//para el select 


    public EventoController(IServiceEvento servicioEvento, IServiceCategoria servicioCategoria)
    {
        _servicioEvento = servicioEvento;
        _servicioCategory = servicioCategoria;
    }

    #region Views

    public async Task<IActionResult> ListEvento()
    {
        List<DtoEvento> lista = await _servicioEvento.GetEventos();
        return View(lista);
    }

    public async Task<IActionResult> NuevoEvento()
    {
        var listCategorys = await _servicioCategory.GetCategorias();
        ViewBag.Categorias = listCategorys;
        return View();
    }

    public async Task<IActionResult> EditarEvento(int id)
    {
        DtoEvento objeto = await _servicioEvento.GetEvento(id);

        var listCategorys = await _servicioCategory.GetCategorias();
        ViewBag.Categorias = listCategorys;

        return View(objeto);
    }

    #endregion


    #region Methods
    [HttpPost]
    public async Task<IActionResult> NuevoEvento(DtoEvento dto)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categorias = await _servicioCategory.GetCategorias();
            return View(dto);
        }
        var response = await _servicioEvento.CreateEvento(dto);
        if (response.exito)
        {
            TempData["MensajeExito"] = response.mensaje;
            return RedirectToAction(nameof(NuevoEvento));
        }
        else
        {
            TempData["MensajeError"] = response.mensaje;
            return RedirectToAction(nameof(NuevoEvento));
        }
    }

    [HttpPost]
    public async Task<IActionResult> EditarEvento(DtoEvento dto)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        var response = await _servicioEvento.EditEvento(dto);
        if (response.exito)
        {
            TempData["MensajeExito"] = response.mensaje;
            return RedirectToAction(nameof(EditarEvento));
        }
        else
        {
            TempData["MensajeError"] = response.mensaje;
            return RedirectToAction(nameof(EditarEvento));
        }
    }


    [HttpGet]
    public async Task<IActionResult> EliminarEvento(int id)
    {
        var response = await _servicioEvento.DeleteEvento(id);
        if (response.exito)
        {
            TempData["MensajeExito"] = response.mensaje;
            return RedirectToAction(nameof(ListEvento));
        }
        else
        {
            TempData["MensajeError"] = response.mensaje;
            return RedirectToAction(nameof(ListEvento));
        }
    }
    #endregion
}
