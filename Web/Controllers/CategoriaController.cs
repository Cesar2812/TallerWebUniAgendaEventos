using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Services.ServiceCategoria;

namespace Web.Controllers;

public class CategoriaController : Controller
{
    public readonly IServiceCategoria _servicioCategoria;


    #region Views
    public CategoriaController(IServiceCategoria servicioCategoria)
    {
        _servicioCategoria = servicioCategoria;
    }


    [HttpGet]
    public async Task<IActionResult>  ListCategoria()
    {
        List<DtoCategoria> lista = await _servicioCategoria.GetCategorias();
        return View(lista);
    }  

    public IActionResult NuevaCategoria()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> EditarCategoria(int id)
    {
        DtoCategoria objeto = await _servicioCategoria.GetCategoria(id);
        return View(objeto);
    }

    [HttpGet]
    public async Task<IActionResult> EliminarCategoria(int id)
    {
        DtoCategoria objeto = await _servicioCategoria.GetCategoria(id);
        return View(objeto);
    }
    #endregion





    #region Methods
    [HttpPost]
    public async Task<IActionResult> NuevaCategoria(DtoCategoria dto)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        var response = await _servicioCategoria.CreateCatgoria(dto);
        if (response.exito)
        {
            TempData["MensajeExito"] = response.mensaje;
            return RedirectToAction(nameof(NuevaCategoria));
        }
        else
        {
            TempData["MensajeError"] = response.mensaje;
            return RedirectToAction(nameof(NuevaCategoria));
        }

    }



    [HttpPost]
    public async Task<IActionResult> EditarCategoria(DtoCategoria dto)
    {
        var response = await _servicioCategoria.EditCategoria(dto);
        if (response.exito)
        {
            TempData["MensajeExito"] = response.mensaje;
            return RedirectToAction(nameof(EditarCategoria));
        }
        else
        {
            TempData["MensajeError"] = response.mensaje;
            return RedirectToAction(nameof(EditarCategoria));
        }
    }




    [HttpPost]
    public async Task<IActionResult> EliminarCategoria(DtoCategoria dto)
    {
        var response = await _servicioCategoria.DeleteCategoria(dto.id);
        if (response.exito)
        {
            TempData["MensajeExito"] = response.mensaje;
            return RedirectToAction(nameof(EliminarCategoria));
        }
        else
        {
            TempData["MensajeError"] = response.mensaje;
            return RedirectToAction(nameof(EliminarCategoria));
        }
    }
    #endregion
}
