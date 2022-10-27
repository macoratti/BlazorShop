using Blazored.LocalStorage;
using BlazorShop.Models.DTOs;

namespace BlazorShop.Web.Services;

public class GerenciaCarrinhoItensLocalStorageService : IGerenciaCarrinhoItensLocalStorageService
{
    const string key = "CarrinhoItemCollection";

    private readonly ILocalStorageService localStorageService;
    private readonly ICarrinhoCompraService carrinhoCompraService;

    public GerenciaCarrinhoItensLocalStorageService(ILocalStorageService localStorageService,
        ICarrinhoCompraService carrinhoCompraService)
    {
        this.localStorageService = localStorageService;
        this.carrinhoCompraService = carrinhoCompraService;
    }

    public async Task<List<CarrinhoItemDto>> GetCollection()
    {
        return await this.localStorageService.GetItemAsync<List<CarrinhoItemDto>>(key)
               ?? await AddCollection();
    }

    public async Task RemoveCollection()
    {
        await this.localStorageService.RemoveItemAsync(key);
    }

    public async  Task SaveCollection(List<CarrinhoItemDto> carrinhoItensDto)
    {
        await this.localStorageService.SetItemAsync(key, carrinhoItensDto);
    }

    //obtem os dados do servidor e armazena no localstorage
    private async Task<List<CarrinhoItemDto>> AddCollection()
    {
        var carrinhoCompraCollection = await this.carrinhoCompraService
                                          .GetItens(UsuarioLogado.UsuarioId);

        if (carrinhoCompraCollection != null)
        {
            await this.localStorageService.SetItemAsync(key, carrinhoCompraCollection);
        }
        return carrinhoCompraCollection;
    }
}



