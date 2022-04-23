using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net6WebApiTemplate.Api.Contracts.Version1.Requests;
using Net6WebApiTemplate.Api.Routes.Version1;
using Net6WebApiTemplate.Application.Categories.Commands.CreateCategory;
using Net6WebApiTemplate.Application.Categories.Commands.DeleteCategory;
using Net6WebApiTemplate.Application.Categories.Commands.PatchCategory;
using Net6WebApiTemplate.Application.Categories.NQueries.GetCategory;
using Net6WebApiTemplate.Application.Categories.NQueries.GetCategoryById;

namespace Net6WebApiTemplate.Api.Controllers.Version1
{
    [Produces("application/json")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create new Category
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="201">Success creating new Category</response>
        /// <response code="400">Bad request</response>
        /// <response code ="429">Too Many Requests</response>
        [HttpPost]
        [Route(ApiRoutes.Category.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CategoryRequest request)
        {
            var command = new CreateCategoryCommand()
            {
                CategoryName = request.CategoryName,
                Description = request.Description
            };

            await _mediator.Send(command);

            return Created(ApiRoutes.Category.Create, command);
        }

        /// <summary>
        /// Retrieve Category by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns> 
        /// <response code="200">Success Retrieve Category by Id</response>
        /// <response code="400">Bad request</response>
        /// <response code ="429">Too Many Requests</response>
        [HttpGet]
        [Route(ApiRoutes.Category.Get)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get([FromQuery] int id)
        {
            var query = new GetCategoryByIdQuery()
            {
                Id = id
            };
            await _mediator.Send(query);

            return Ok(query);
        }

        /// <summary>
        ///  Get list of Categories
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Success retrieving Category list</response>
        /// <response code="400">Bad request</response>
        /// <response code ="429">Too Many Requests</response>
        [HttpGet]
        [Route(ApiRoutes.Category.GetAll)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetCategoryQuery();
            var results = await _mediator.Send(query);

            return Ok(results);
        }

        /// <summary>
        /// Update an exsiting Category
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Success updating exsiting Category</response>
        /// <response code="400">Bad request</response>
        /// <response code ="429">Too Many Requests</response>
        [HttpPatch]
        [Route(ApiRoutes.Category.Patch)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] CategoryRequest request)
        {
            var command = new PatchCategoryCommand()
            {
                Id = request.Id,
                CategoryName = request.CategoryName,
                Description = request.Description
            };
            var results = await _mediator.Send(command);

            return Ok(results);
        }

        /// <summary>
        ///  Delete an existing Category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="204">Success delete an exsiting Category</response>
        /// <response code="400">Bad request</response>
        /// <response code ="429">Too Many Requests</response>
        [HttpDelete]
        [Route(ApiRoutes.Category.Delete)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var command = new DeleteCategoryCommand { Id = id };
            var results = await _mediator.Send(command);

            return Ok(results);
        }
    }
}