using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class DtoCategoria
    {

        public int id { get; set; }

        [Required(ErrorMessage ="El Nombre es Obligatorio")]
        public string? nameCategory { get; set; }

    }
}
