using Microsoft.AspNetCore.Mvc;
using NServiceBus;
using SagaPattern.API.Application.Messages.Sagas.CreateProduct.Steps;

namespace SagaPattern.API.Data.Repositories;

[Route("api/products")]
[ApiController]
public class ProductsController : ControllerBase
{

    private readonly IMessageSession _saga;

    public ProductsController(IMessageSession saga)
    {
        _saga = saga;
    }

    // POST api/products/
    /// <summary>
    /// Grava o produto
    /// </summary>   
    /// <remarks>
    /// Exemplo request:
    ///
    ///     POST / Produto
    ///     {
    ///         "title": "Sandalia",
    ///         "description": "Sandália Preta Couro Salto Fino",
    ///         "price": 249.50,
    ///         "quantity": 100       
    ///     }
    /// </remarks>        
    /// <returns>Retorna objeto criado da classe Produto</returns>                
    /// <response code="201">O produto foi incluído corretamente</response>                
    /// <response code="400">Falha na requisição</response>         
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ActionName("NewProduct")]
    public async Task<IActionResult> PostAsync([FromBody] CreateProductCommand command)
    {
        var saga = new CreateProductStartStep(
            command.Title,
            command.Description,
            command.Price,
            command.Quantity);

        await _saga.SendLocal(saga);

        return Accepted();
    }

}
