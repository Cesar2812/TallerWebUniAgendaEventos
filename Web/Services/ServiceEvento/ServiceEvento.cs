using System.Text.Json;
using Web.Models;
using Web.Models.Mappers;

namespace Web.Services.ServiceEvento;

public class ServiceEvento : IServiceEvento
{
    private readonly string? _serverApi;
    public ServiceEvento(IConfiguration configuration)
    {
        _serverApi = configuration["SERVER_API"];
    }


    //listar Evento
    public async Task<List<DtoEvento>> GetEventos()
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri(_serverApi);

        var response = await client.GetAsync("api/Activities/GetActivities");
        var json = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<ApiEventoResponse>(json);

        return result?.lista ?? new List<DtoEvento>();
    }

    //obtener Evento por id
    public async Task<DtoEvento> GetEvento(int id)
    {
        DtoEvento objeto = new DtoEvento();
        var cliente = new HttpClient();
        cliente.BaseAddress = new Uri(_serverApi);

        var response = await cliente.GetAsync($"api/Activities/GetActivitie/{id}");

        if (response.IsSuccessStatusCode)
        {
            var json_respuesta = await response.Content.ReadAsStringAsync();
            var resultado = JsonSerializer.Deserialize<ApiEventoResponse>(json_respuesta);
            objeto = resultado.objeto;
        }

        return objeto;
    }

    //registrar Evento
    public async Task<ApiResponse> CreateEvento(DtoEvento dto)
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri(_serverApi);

        var jsonData = JsonSerializer.Serialize(dto);//convirtiendo a Json
        HttpContent body = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

        var response = await client.PostAsync("api/Activities/CreateActivitie", body);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiResponse>(content);

            return new ApiResponse
            {
                exito = true,
                mensaje = result?.mensaje
            };
        }
        else
        {
            return new ApiResponse
            {
                exito = false,
                mensaje = "Error al crear el Evento"
            };
        }
    }

    //editar evento
    public async Task<ApiResponse> EditEvento(DtoEvento dto)
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri(_serverApi);

        var jsonData = JsonSerializer.Serialize(dto);//convirtiendo a Json
        HttpContent body = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

        var response = await client.PutAsync("api/Activities/UpdateActivitie", body);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiResponse>(content);

            return new ApiResponse
            {
                exito = true,
                mensaje = result?.mensaje
            };
        }
        else
        {
            return new ApiResponse
            {
                exito = false,
                mensaje = "Error al crear el Evento"
            };
        }
    }

    //eliminar evento
    public async Task<ApiResponse> DeleteEvento(int id)
    {
        var cliente = new HttpClient();
        cliente.BaseAddress = new Uri(_serverApi);
        var response = await cliente.DeleteAsync($"api/Activities/DeleteActivitie/{id}");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiResponse>(content);

            return new ApiResponse
            {
                exito = true,
                mensaje = result?.mensaje
            };
        }
        else
        {
            return new ApiResponse
            {
                exito = false,
                mensaje = "Error al eliminar la Categoria"
            };
        }
    }
}
