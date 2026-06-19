namespace WebService.Models
{
    public class DtoActivities
    {

        public int id { get; set; }
        public string? nameActivitie { get; set; }

        public DateOnly starDate { get; set; }


        public DateOnly endDate { get; set; }

        public int categorieId { get; set; }
        public string? notes { get; set; }

    }
}
