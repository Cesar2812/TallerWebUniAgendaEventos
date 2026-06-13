using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebService.Models;

namespace WebService.Controllers;


[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly GestionEventosContext _dbContext;

    public CategoryController(GestionEventosContext dbContext)
    {
        _dbContext = dbContext;
    }


    [HttpGet]
    [Route("GetCategories")]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _dbContext.Categories.Select(c => new
        {
            c.Id,
            c.NameCategory
        }).ToListAsync();

        return StatusCode(StatusCodes.Status200OK, new
        {
            lista = categories
        });
    }

    [HttpGet]
    [Route("GetCategories/{id:int}")]
    public async Task<IActionResult> GetCategories(int id)
    {
        var categorie = await _dbContext.Categories.Where(c => c.Id == id).Select(c => new
        {
            c.Id,
            c.NameCategory
        }).FirstOrDefaultAsync();

        return StatusCode(StatusCodes.Status200OK, new 
        { 
            objeto = categorie
        });
    }


    [HttpPost]
    [Route ("CreateCategorie")]
    public async Task<IActionResult> CreateCategorie([FromBody] DtoCategories dto) 
    {
        var categorie = new Categories
        {
            NameCategory=dto.nameCategory
        };
        await _dbContext.Categories.AddAsync(categorie);
        await _dbContext.SaveChangesAsync(); 

        return StatusCode(StatusCodes.Status201Created, new
        {
            mensaje="Categoria Insertada Correctamente"
        });
    }


    
    [HttpPut]
    [Route("UpdateCategorie")]
    public async Task<IActionResult> UpdateCategorie([FromBody] DtoCategories dto)
    {
        var categorie = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == dto.id);

        categorie.NameCategory = dto.nameCategory;

        await _dbContext.SaveChangesAsync();

        return StatusCode(StatusCodes.Status200OK, new
        {
            mensaje = "Categoria Editada Correctamente"
        });
    }

    [HttpDelete]
    [Route("DeleteCategorie/{id:int}")]
    public async Task<IActionResult> DeleteCategorie(int id)
    {
        var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);

        _dbContext.Categories.Remove(category);
        await _dbContext.SaveChangesAsync();
        return StatusCode(StatusCodes.Status200OK, new
        {
            mensaje = "Categoria Eliminada Correctamente"
        });
    }
}