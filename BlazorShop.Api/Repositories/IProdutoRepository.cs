using BlazorShop.Api.Entities;

namespace BlazorShop.Api.Repositories;

public interface IProdutoRepository
{
    Task<IEnumerable<Produto>> GetItens();
    Task<Produto> GetItem(int id);

    Task<IEnumerable<Produto>> GetItensPorCategoria(int id);
    Task<IEnumerable<Categoria>> GetCategorias();
}
