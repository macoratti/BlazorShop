using BlazorShop.Models.DTOs;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace BlazorShop.Web.Services;

public class CarrinhoCompraService : ICarrinhoCompraService
{
    private readonly HttpClient httpClient;
    public CarrinhoCompraService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public event Action<int> OnCarrinhoCompraChanged;

    public async Task<CarrinhoItemDto> AdicionaItem(CarrinhoItemAdicionaDto carrinhoItemAdicionaDto)
    {
        try
        {
            var response = await httpClient
                          .PostAsJsonAsync<CarrinhoItemAdicionaDto>("api/CarrinhoCompra", 
                           carrinhoItemAdicionaDto);

            if (response.IsSuccessStatusCode)// status code entre 200 a 299
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    // retorna o valor "padrão" ou vazio
                    // para uma objeto do tipo carrinhoItemDto
                    return default(CarrinhoItemDto);
                }
                //le o conteudo HTTP e retorna o valor resultante
                //da serialização do conteudo JSON para o objeto Dto
                return await response.Content.ReadFromJsonAsync<CarrinhoItemDto>();
            }
            else
            {
                //serializa o conteudo HTTP como uma string
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"{response.StatusCode} Message -{message}");
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<CarrinhoItemDto> AtualizaQuantidade(CarrinhoItemAtualizaQuantidadeDto
                                                   carrinhoItemAtualizaQuantidadeDto)
    {
        try
        {
            var jsonRequest = JsonSerializer.Serialize(carrinhoItemAtualizaQuantidadeDto);

            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");

            var response = await httpClient.PatchAsync($"api/CarrinhoCompra/{carrinhoItemAtualizaQuantidadeDto.CarrinhoItemId}", content);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<CarrinhoItemDto>();
            }
            return null;
        }
        catch (Exception)
        {
            throw;
        }

    }

    public async Task<CarrinhoItemDto> DeletaItem(int id)
    {
        try
        {
            var response = await httpClient.DeleteAsync($"api/CarrinhoCompra/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<CarrinhoItemDto>();
            }
            return default(CarrinhoItemDto);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<List<CarrinhoItemDto>> GetItens(string usuarioId)
    {
        try
        {
            //envia um request GET para a uri da API CarrinhoCompra
            var response = await httpClient.GetAsync($"api/CarrinhoCompra/{usuarioId}/GetItens");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return Enumerable.Empty<CarrinhoItemDto>().ToList();
                }
                return await response.Content.ReadFromJsonAsync<List<CarrinhoItemDto>>();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http Status Code: {response.StatusCode} Mensagem: {message}");
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    public void RaiseEventOnCarrinhoCompraChanged(int totalQuantidade)
    {
        if (OnCarrinhoCompraChanged != null)
        {
            OnCarrinhoCompraChanged.Invoke(totalQuantidade);
        }
    }
}
