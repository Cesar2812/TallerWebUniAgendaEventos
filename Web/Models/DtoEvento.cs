using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class DtoEvento
    {
        public int id { get; set; }
        [Required(ErrorMessage = "El Nombre es Obligatorio")]
        public string nameActivitie { get; set; }

        [Required(ErrorMessage = "La Fecha de Inicio es Obligatoria")]
        public DateOnly? starDate { get; set; }

        [Required(ErrorMessage = "La Fecha de Fin es Obligatoria")]
        public DateOnly? endDate { get; set; }
        public string nameCategory { get; set; } = null!;

        [Range(1, int.MaxValue, ErrorMessage = "La Categoría del Evento es Obligatoria")]
        public int categorieId { get; set; }

        [Required(ErrorMessage = "Las notas del Evento son Obligatorias")]
        public string notes { get; set; }
    }
}
