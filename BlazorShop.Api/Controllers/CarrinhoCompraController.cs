using BlazorShop.Api.Mappings;
using BlazorShop.Api.Repositories;
using BlazorShop.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BlazorShop.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarrinhoCompraController : ControllerBase
{
    private readonly ICarrinhoCompraRepository carrinhoCompraRepo;
    private readonly IProdutoRepository produtoRepo;

    private ILogger<CarrinhoCompraController> logger;

    public CarrinhoCompraController(ICarrinhoCompraRepository
        carrinhoCompraRepository,
        IProdutoRepository produtoRepository,
        ILogger<CarrinhoCompraController> logger)
    {
        carrinhoCompraRepo = carrinhoCompraRepository;
        produtoRepo = produtoRepository;
        this.logger = logger;
    }

    [HttpGet]
    [Route("{usuarioId}/GetItens")]
    public async Task<ActionResult<IEnumerable<CarrinhoItemDto>>> GetItens(string usuarioId)
    {
        try
        {
            var carrinhoItens = await carrinhoCompraRepo.GetItens(usuarioId);
            if (carrinhoItens == null)
            {
                return NoContent(); // 204 Status Code
            }

            var produtos = await this.produtoRepo.GetItens();
            if (produtos == null)
            {
                throw new Exception("Não existem produtos...");
            }

            var carrinhoItensDto = carrinhoItens.ConverterCarrinhoItensParaDto(produtos);
            return Ok(carrinhoItensDto);
        }
        catch (Exception ex)
        {
            logger.LogError("## Erro ao obter itens do carrinho");
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CarrinhoItemDto>> GetItem(int id)
    {
        try
        {
            var carrinhoItem = await carrinhoCompraRepo.GetItem(id);
            if (carrinhoItem == null)
            {
                return NotFound($"Item não encontrado"); //404 status code
            }

            var produto = await produtoRepo.GetItem(carrinhoItem.ProdutoId);

            if (produto == null)
            {
                return NotFound($"Item não existe na fonte de dados");
            }
            var cartItemDto = carrinhoItem.ConverterCarrinhoItemParaDto(produto);

            return Ok(cartItemDto);
        }
        catch (Exception ex)
        {
            logger.LogError($"## Erro ao obter o item ={id} do carrinho");
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<CarrinhoItemDto>> PostItem([FromBody]
    CarrinhoItemAdicionaDto carrinhoItemAdicionaDto)
    {
        try
        {
            var novoCarrinhoItem = await carrinhoCompraRepo.AdicionaItem(carrinhoItemAdicionaDto);

            if (novoCarrinhoItem == null)
            {
                return NoContent(); //Status 204
            }

            var produto = await produtoRepo.GetItem(novoCarrinhoItem.ProdutoId);

            if (produto == null)
            {
                throw new Exception($"Produto não localizado (Id:({carrinhoItemAdicionaDto.ProdutoId})");
            }

            var novoCarrinhoItemDto = novoCarrinhoItem.ConverterCarrinhoItemParaDto(produto);

            return CreatedAtAction(nameof(GetItem), new { id = novoCarrinhoItemDto.Id },
                novoCarrinhoItemDto);

        }
        catch (Exception ex)
        {
            logger.LogError("## Erro criar um novo item no carrinho");
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<CarrinhoItemDto>> DeleteItem(int id)
    {
        try
        {
            var carrinhoItem = await carrinhoCompraRepo.DeletaItem(id);

            if (carrinhoItem == null)
            {
                return NotFound();
            }

            var produto = await produtoRepo.GetItem(carrinhoItem.ProdutoId);

            if (produto is null)
                return NotFound();

            var carrinhoItemDto = carrinhoItem.ConverterCarrinhoItemParaDto(produto);
            return Ok(carrinhoItemDto);

        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPatch("{id:int}")]
    public async Task<ActionResult<CarrinhoItemDto>> AtualizaQuantidade(int id, 
        CarrinhoItemAtualizaQuantidadeDto carrinhoItemAtualizaQuantidadeDto)
    {
        try
        {

            var carrinhoItem = await carrinhoCompraRepo.AtualizaQuantidade(id, 
                                   carrinhoItemAtualizaQuantidadeDto);

            if (carrinhoItem == null)
            {
                return NotFound();
            }
            var produto = await produtoRepo.GetItem(carrinhoItem.ProdutoId);
            var carrinhoItemDto = carrinhoItem.ConverterCarrinhoItemParaDto(produto);
            return Ok(carrinhoItemDto);

        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
