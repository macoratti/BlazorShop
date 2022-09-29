using BlazorShop.Models.DTOs;

namespace BlazorShop.Web.Services;

public interface ICarrinhoCompraService
{
    Task<List<CarrinhoItemDto>> GetItens(string usuarioId);
    Task<CarrinhoItemDto> AdicionaItem(CarrinhoItemAdicionaDto carrinhoItemAdicionaDto);
    Task<CarrinhoItemDto> DeletaItem(int id);
}
