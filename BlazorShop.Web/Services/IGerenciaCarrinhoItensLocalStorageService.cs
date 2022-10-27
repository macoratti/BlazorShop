using BlazorShop.Models.DTOs;

namespace BlazorShop.Web.Services;

public interface IGerenciaCarrinhoItensLocalStorageService
{
    Task<List<CarrinhoItemDto>> GetCollection();
    Task SaveCollection(List<CarrinhoItemDto> carrinhoItensDto);
    Task RemoveCollection();
}
