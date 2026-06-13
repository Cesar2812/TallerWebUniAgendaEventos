using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using WebService.Models;

namespace WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly GestionEventosContext _dbContext;

        public ActivitiesController(GestionEventosContext dbContext)
        {
            _dbContext = dbContext;

        }

        [HttpGet]
        [Route("GetActivities")]
        public async Task<IActionResult> GetActivities()
        {
            var activities = await _dbContext.Activities
            .Include(a => a.Categorie)
            .Select(a => new
            {
                a.Id,
                a.NameActivitie,
                a.StarDate,
                a.EndDate,
                nameCategory = a.Categorie.NameCategory,
                a.Notes
            })
            .ToListAsync();

            return StatusCode(StatusCodes.Status200OK, new
            {
                lista=activities
            });
        }

        [HttpGet]
        [Route("GetActivitye/{id:int}")]
        public async Task<IActionResult> GetActivitye(int id)
        {
            var activity = await _dbContext.Activities
                .Include(a => a.Categorie)
                .Where(a => a.Id == id)
                .Select(a => new
                {
                    a.Id,
                    a.NameActivitie,
                    a.StarDate,
                    a.EndDate,
                    a.CategorieId,
                    nameCategory = a.Categorie.NameCategory,
                    a.Notes
                })
                .FirstOrDefaultAsync();

            return StatusCode(StatusCodes.Status200OK, new { objeto = activity });
        }

        [HttpPost]
        [Route("CreateActivitye")]
        public async Task<IActionResult> CreateActivitye([FromBody] DtoActivities dtoActivity)
        {
            var activity = new Activities
            {
                NameActivitie = dtoActivity.nameActivitie,
                StarDate = dtoActivity.starDate,
                EndDate = dtoActivity.starEnd,
                CategorieId = dtoActivity.categoryId,
                Notes = dtoActivity.notes
            };

            await _dbContext.Activities.AddAsync(activity);
            await _dbContext.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created, new
            {
                mensaje = "Evento creado correctamente"
            });
        }

        [HttpPut]
        [Route("UpdateActivitie")]
        public async Task<IActionResult> UpdateActivitie([FromBody] DtoActivities dtoActivitie)
        {
            var activity = await _dbContext.Activities.FirstOrDefaultAsync(a => a.Id == dtoActivitie.id);

            activity.NameActivitie = dtoActivitie.nameActivitie;
            activity.StarDate = dtoActivitie.starDate;
            activity.EndDate = dtoActivitie.starEnd;
            activity.CategorieId = dtoActivitie.categoryId;
            activity.Notes = dtoActivitie.notes;

            await _dbContext.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, new
            {
                mensaje = "Evento actualizado correctamente"
            });
        }

        [HttpDelete]
        [Route("DeleteActivitie/{id:int}")]
        public async Task<IActionResult> DeleteActivitie(int id)
        {
            var activity = await _dbContext.Activities.FindAsync(id);

            _dbContext.Activities.Remove(activity);

            await _dbContext.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, new
            {
                mensaje = "Evento eliminado correctamente"
            });
        }


    }
}
