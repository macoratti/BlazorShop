using BlazorShop.Models.DTOs;

namespace BlazorShop.Web.Services;

public interface IProdutoService
{
    Task<IEnumerable<ProdutoDto>> GetItens();
    Task<ProdutoDto> GetItem(int id);
}
