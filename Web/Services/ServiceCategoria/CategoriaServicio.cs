using System.Text.Json;
using Web.Models;
using Web.Models.Mappers;

namespace Web.Services.ServiceCategoria;

public class CategoriaServicio:IServiceCategoria
{
    private readonly string _serverApi;

    public CategoriaServicio( IConfiguration configuration)
    {
        _serverApi = configuration["SERVER_API"];
    }


    //obtener categorias
    public async Task<List<DtoCategoria>> GetCategorias()
    {
        var client= new HttpClient();
        client.BaseAddress = new Uri(_serverApi);

        var response = await client.GetAsync("api/Category/GetCategories");
        var json= await response.Content.ReadAsStringAsync();

        var jsonResult = JsonSerializer.Deserialize<ApiCategoriaResponse>(json);

        return jsonResult?.lista ?? new List<DtoCategoria>();

    }

    
    //obtener categoria por Id
    public async Task<DtoCategoria> GetCategoria(int id)
    {
        DtoCategoria objeto = new DtoCategoria();
        var cliente = new HttpClient();
        cliente.BaseAddress = new Uri(_serverApi);

        var response = await cliente.GetAsync($"api/Category/GetCategories/{id}");

        if (response.IsSuccessStatusCode)
        {
            var json_respuesta = await response.Content.ReadAsStringAsync();
            var resultado = JsonSerializer.Deserialize<ApiCategoriaResponse>(json_respuesta);
            objeto = resultado.objeto;
        }

        return objeto;
    }


    //crear Categorias
    public async Task<ApiResponse> CreateCatgoria(DtoCategoria dto)
    {
        var client= new HttpClient();
        client.BaseAddress = new Uri(_serverApi);

        var data=JsonSerializer.Serialize(dto);
        HttpContent body = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

        var response = await client.PostAsync("api/Category/CreateCategorie", body);

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
                mensaje = "Error al crear la Categoria"
            };
        }
    }


    //editar Categoria
    public async Task<ApiResponse> EditCategoria(DtoCategoria dto)
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri(_serverApi);

        var data = JsonSerializer.Serialize(dto);
        HttpContent body = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

        var response = await client.PutAsync("api/Category/UpdateCategorie", body);

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
                mensaje = "Error al editar la Categoria"
            };
        }
    }


    //eliminar categoria
    public async Task<ApiResponse> DeleteCategoria(int id)
    {
        var cliente = new HttpClient();
        cliente.BaseAddress = new Uri(_serverApi);
        var response = await cliente.DeleteAsync($"api/Category/DeleteCategorie/{id}");

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
