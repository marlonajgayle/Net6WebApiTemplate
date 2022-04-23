using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net6WebApiTemplate.Api.Contracts.Version1.Requests;
using Net6WebApiTemplate.Api.Routes.Version1;
using Net6WebApiTemplate.Application.Clients.Commands.CreateClient;
using Net6WebApiTemplate.Application.Clients.Commands.DeleteClient;
using Net6WebApiTemplate.Application.Clients.Commands.Queries.GetClientByIdQuery;
using Net6WebApiTemplate.Application.Clients.Commands.Queries.GetClientsQuery;
using Net6WebApiTemplate.Application.Clients.Commands.UpdateClient;
using Net6WebApiTemplate.Application.Clients.Queries.GetGitHubUser;

namespace Net6WebApiTemplate.Api.Controllers.Version1
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ClientController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create new client
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="201">Success creating new client</response>
        /// <response code="400">Bad request</response>
        /// <response code ="429">Too Many Requests</response>
        [HttpPost]
        [Route(ApiRoutes.Client.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] ClientRequest request)
        {
            var command = new CreateClientCommand()
            {
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                LastName = request.LastName,
                Trn = request.Trn,
                AddressLine1 = request.AddressLine1,
                AddressLine2 = request.AddressLine2,
                Parish = request.Parish
            };

            await _mediator.Send(command);

            return Created(ApiRoutes.Client.Create, command);
        }

        /// <summary>
        /// Retrieve Client by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns> 
        /// <response code="200">Success Retrieve Client by Id</response>
        /// <response code="400">Bad request</response>
        /// <response code ="429">Too Many Requests</response>
        [HttpGet]
        [Route(ApiRoutes.Client.Get)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get([FromQuery] int id)
        {
            var query = new GetClientByIdQuery()
            {
                Id = id
            };
            await _mediator.Send(query);

            return Ok(query);
        }

        /// <summary>
        /// Retrieve GitHub user by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns> 
        /// <response code="200">Success Retrieve GitHuib user info</response>
        /// <response code="400">Bad request</response>
        /// <response code ="429">Too Many Requests</response>
        [HttpGet]
        [Route(ApiRoutes.Client.GetGitHubUser)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetGitHubUser(string username)
        {
            var query = new GetGitHubUserQuery()
            {
                Username = username
            };

            var results = await _mediator.Send(query);
            return Ok(results);
        }

        /// <summary>
        ///  Get list of clients
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Success retrieving client list</response>
        /// <response code="400">Bad request</response>
        /// <response code ="429">Too Many Requests</response>
        [HttpGet]
        [Route(ApiRoutes.Client.GetAll)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetClientsQuery();
            var results = await _mediator.Send(query);

            return Ok(results);
        }

        /// <summary>
        /// Update an exsiting Client
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Success updating exsiting client</response>
        /// <response code="400">Bad request</response>
        /// <response code ="429">Too Many Requests</response>
        [HttpPatch]
        [Route(ApiRoutes.Client.Update)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] ClientRequest request)
        {
            var command = new UpdateClientCommand()
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName
            };
            await _mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        ///  Delete an existing client
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="204">Success delete an exsiting client</response>
        /// <response code="400">Bad request</response>
        /// <response code ="429">Too Many Requests</response>
        [HttpDelete]
        [Route(ApiRoutes.Client.Delete)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var command = new DeleteClientCommand { Id = id };
            await _mediator.Send(command);

            return NoContent();
        }
    }
}