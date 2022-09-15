namespace BlazorShop.Api.Entities;

public class Carrinho
{
    public int Id { get; set; }
    public string UsuarioId { get; set; }

    public ICollection<CarrinhoItem> Itens { get; set; } =
        new List<CarrinhoItem>();
}
