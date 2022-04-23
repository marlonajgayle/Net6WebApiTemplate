using MediatR;
using Microsoft.EntityFrameworkCore;
using Net6WebApiTemplate.Application.Common.Interfaces;

namespace Net6WebApiTemplate.Application.Clients.Commands.Queries.GetClientsQuery
{
    public class GetClientsQueryHandler : IRequestHandler<GetClientsQuery, IList<ClientDto>>
    {
        private readonly INet6WebApiTemplateDbContext _dbContext;

        public GetClientsQueryHandler(INet6WebApiTemplateDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IList<ClientDto>> Handle(GetClientsQuery request, CancellationToken cancellationToken)
        {
            var clients = await _dbContext.Clients
                .Select(client => new ClientDto
                {
                    Id = client.Id,
                    FirstName = client.FirstName,
                    LastName = client.LastName
                })
                .ToListAsync(cancellationToken);

            return clients;
        }
    }
}