using BlazorShop.Models.DTOs;

namespace BlazorShop.Web.Services;

public interface ICarrinhoCompraService
{
    Task<List<CarrinhoItemDto>> GetItens(string usuarioId);
    Task<CarrinhoItemDto> AdicionaItem(CarrinhoItemAdicionaDto carrinhoItemAdicionaDto);
    Task<CarrinhoItemDto> DeletaItem(int id);
    Task<CarrinhoItemDto> AtualizaQuantidade(CarrinhoItemAtualizaQuantidadeDto carrinhoItemAtualizaQuantidadeDto);

    event Action<int> OnCarrinhoCompraChanged;
    void RaiseEventOnCarrinhoCompraChanged(int totalQuantidade);

}
