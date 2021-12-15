using MediatR;
using Microsoft.EntityFrameworkCore;
using Net6WebApiTemplate.Application.Categories.Dto;
using Net6WebApiTemplate.Application.Common.Exceptions;
using Net6WebApiTemplate.Application.Common.Interfaces;
using Net6WebApiTemplate.Domain.Entities;

namespace Net6WebApiTemplate.Application.Categories.NQueries.GetCategoryById
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
    {
        private readonly INet6WebApiTemplateDbContext _dbContext;

        public GetCategoryByIdQueryHandler(INet6WebApiTemplateDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            Category result = await Task.Run(() => _dbContext
           .Categories           
           .Where(s => s.Id.Equals(request.Id))
           .FirstOrDefault(), cancellationToken);



            if (result == null)
            {
                throw new NotFoundException($"Category with id {request.Id} not found.");
            }

            CategoryDto categoryDto = new()
            {
                Id = request.Id,
                CategoryName = result.CategoryName,
                Description = result.Description
            };

            return categoryDto;
        }
    }
}
