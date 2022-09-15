using BlazorShop.Models.DTOs;
using System.Net;
using System.Net.Http.Json;

namespace BlazorShop.Web.Services;

public class ProdutoService : IProdutoService
{
    public HttpClient _httpClient;
    public ILogger<ProdutoService> _logger;

    public ProdutoService(HttpClient httpClient,
        ILogger<ProdutoService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<IEnumerable<ProdutoDto>> GetItens()
    {
        try
        {
            var produtosDto = await _httpClient.
                             GetFromJsonAsync<IEnumerable<ProdutoDto>>("api/produtos");
            return produtosDto;
        }
        catch (Exception)
        {
            _logger.LogError("Erro ao acessar produtos : api/produtos ");
            throw;
        }
    }

    public async Task<ProdutoDto> GetItem(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/produtos/{id}");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return default(ProdutoDto);
                }
                return await response.Content.ReadFromJsonAsync<ProdutoDto>();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Erro a obter produto pelo id= {id} - {message}");
                throw new Exception($"Status Code : {response.StatusCode} - {message}");
            }
        }
        catch (Exception)
        {
            _logger.LogError($"Erro a obter produto pelo id={id}");
            throw;
        }
    }
}
