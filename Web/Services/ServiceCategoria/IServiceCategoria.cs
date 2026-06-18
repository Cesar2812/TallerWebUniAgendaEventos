using Web.Models;

namespace Web.Services.ServiceCategoria;

public interface IServiceCategoria
{

    Task<List<DtoCategoria>> GetCategorias();
    Task<ApiResponse> CreateCatgoria(DtoCategoria dto);

    Task<ApiResponse> EditCategoria(DtoCategoria dto);

    Task<ApiResponse> DeleteCategoria(int id);

    Task<DtoCategoria> GetCategoria(int id);

}
