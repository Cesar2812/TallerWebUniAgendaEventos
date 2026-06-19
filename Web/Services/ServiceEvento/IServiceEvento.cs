using Web.Models;

namespace Web.Services.ServiceEvento
{
    public interface IServiceEvento
    {

        Task<List<DtoEvento>> GetEventos();

        Task<DtoEvento> GetEvento(int id);

        Task<ApiResponse> CreateEvento(DtoEvento dto);

        Task<ApiResponse> EditEvento(DtoEvento dto);

        Task<ApiResponse> DeleteEvento(int id);
    }
}
