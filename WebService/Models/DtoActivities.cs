namespace WebService.Models
{
    public class DtoActivities
    {

        public int id { get; set; }
        public string? nameActivitie { get; set; }

        public DateOnly starDate { get; set; }


        public DateOnly starEnd { get; set; }

        public int categoryId { get; set; }
        public string? notes { get; set; }

    }
}
