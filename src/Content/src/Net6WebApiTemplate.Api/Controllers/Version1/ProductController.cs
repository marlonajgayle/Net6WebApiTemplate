using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net6WebApiTemplate.Api.Contracts.Version1.Requests;
using Net6WebApiTemplate.Api.Routes.Version1;
using Net6WebApiTemplate.Application.Products.Commands.CreateProduct;
using Net6WebApiTemplate.Application.Products.Commands.DeleteProduct;
using Net6WebApiTemplate.Application.Products.Commands.PatchProduct;
using Net6WebApiTemplate.Application.Products.Dto;
using Net6WebApiTemplate.Application.Products.NQueries.GetProductById;
using Net6WebApiTemplate.Application.Products.NQueries.GetProducts;

namespace Net6WebApiTemplate.Api.Controllers.Version1
{
    [Produces("application/json")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create new product
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="201">Success creating new product</response>
        /// <response code="400">Bad request</response>
        /// <response code ="429">Too Many Requests</response>
        [HttpPost]
        [Route(ApiRoutes.Product.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] ProductRequest request)
        {
            var command = new CreateProductCommand()
            {
                ProductName = request.ProductName,
                UnitPrice = request.UnitPrice,
                CategoryId = request.CategoryId         
            };

            await _mediator.Send(command);

            return Created(ApiRoutes.Product.Create, command);
        }

        /// <summary>
        /// Retrieve product by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns> 
        /// <response code="200">Success Retrieve product by Id</response>
        /// <response code="400">Bad request</response>
        /// <response code ="429">Too Many Requests</response>
        [HttpGet]
        [Route(ApiRoutes.Product.Get)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var query = new GetProductByIdQuery()
            {
                Id = id
            };
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        /// <summary>
        ///  Get list of Products
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Success retrieving product list</response>
        /// <response code="400">Bad request</response>
        /// <response code ="429">Too Many Requests</response>
        [HttpGet]
        [Route(ApiRoutes.Product.GetAll)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetProductsQuery();
            var results = await _mediator.Send(query);

            return Ok(results);
        }

        /// <summary>
        /// Update an exsiting product
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Success updating exsiting product</response>
        /// <response code="400">Bad request</response>
        /// <response code ="429">Too Many Requests</response>
        [HttpPatch]
        [Route(ApiRoutes.Product.Patch)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ProductRequest request)
        {
            var command = new PatchProductCommand()
            {
                Id = id,
                ProductName = request.ProductName,
                UnitPrice = request.UnitPrice,
                CategoryId = request.CategoryId,
            };
            var results = await _mediator.Send(command);

            return Ok(results);
        }

        /// <summary>
        ///  Delete an existing product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="204">Success delete an exsiting product</response>
        /// <response code="400">Bad request</response>
        /// <response code ="429">Too Many Requests</response>
        [HttpDelete]
        [Route(ApiRoutes.Product.Delete)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var command = new DeleteProductCommand { Id = id };
            var results = await _mediator.Send(command);

            return Ok(results);
        }
    }
}