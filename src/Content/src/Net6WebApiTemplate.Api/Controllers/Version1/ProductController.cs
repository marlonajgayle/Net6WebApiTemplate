using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net6WebApiTemplate.Api.Contracts.Version1.Requests;
using Net6WebApiTemplate.Api.Routes.Version1;
using Net6WebApiTemplate.Application.products.Commands.Createproduct;
using Net6WebApiTemplate.Application.products.Commands.Deleteproduct;
using Net6WebApiTemplate.Application.products.Commands.Queries.GetproductByIdQuery;
using Net6WebApiTemplate.Application.products.Commands.Queries.GetproductsQuery;
using Net6WebApiTemplate.Application.products.Commands.Updateproduct;

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
                CategoryId = request.CategoryId,
                Category = request.Category,
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
        [Route(ApiRoutes.product.Get)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get([FromQuery] int id)
        {
            var query = new GetproductByIdQuery()
            {
                Id = id
            };
            await _mediator.Send(query);

            return Ok(query);
        }

        /// <summary>
        ///  Get list of products
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Success retrieving product list</response>
        /// <response code="400">Bad request</response>
        /// <response code ="429">Too Many Requests</response>
        [HttpGet]
        [Route(ApiRoutes.product.GetAll)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetproductsQuery();
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
        [Route(ApiRoutes.product.Update)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] productRequest request)
        {
            var command = new UpdateproductCommand()
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName
            };
            await _mediator.Send(command);

            return NoContent();
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
        [Route(ApiRoutes.product.Delete)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var command = new DeleteproductCommand { Id = id };
            await _mediator.Send(command);

            return NoContent();
        }
    }
}