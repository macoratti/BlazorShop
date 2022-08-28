using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorShop.Api.Entities;

public class Produto
{
    public int Id { get; set; }
    
    [MaxLength(100)]
    public string Nome { get; set; } = string.Empty;

    [MaxLength(200)]
    public string Descricao { get; set; } = string.Empty;

    [MaxLength(200)]
    public string ImagemUrl { get; set; } = string.Empty;

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Preco { get; set; }
    public int Quantidade { get; set; }

    public int CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }

    public ICollection<CarrinhoItem> Itens { get; set; }
       = new List<CarrinhoItem>();

}
