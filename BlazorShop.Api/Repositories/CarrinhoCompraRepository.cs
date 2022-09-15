using BlazorShop.Api.Context;
using BlazorShop.Api.Entities;
using BlazorShop.Models.DTOs;

namespace BlazorShop.Api.Repositories;

public class CarrinhoCompraRepository : ICarrinhoCompraRepository
{
    private readonly AppDbContext _context;

    public CarrinhoCompraRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<CarrinhoItem> AdicionaItem(CarrinhoItemAdicionaDto carrinhoItemAdicionaDto)
    {
        throw new NotImplementedException();
    }

    public Task<CarrinhoItem> AtualizaQuantidade(int id, CarrinhoItemAtualizaQuantidadeDto carrinhoItemAtualizaQuantidadeDto)
    {
        throw new NotImplementedException();
    }

    public Task<CarrinhoItem> DeletaItem(int id)
    {
        throw new NotImplementedException();
    }

    public Task<CarrinhoItem> GetItem(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CarrinhoItem>> GetItens(string usuarioId)
    {
        throw new NotImplementedException();
    }
}
