using MediatR;
using Microsoft.EntityFrameworkCore;
using Net6WebApiTemplate.Application.Categories.Dto;
using Net6WebApiTemplate.Application.Common.Interfaces;

namespace Net6WebApiTemplate.Application.Categories.NQueries.GetCategory
{
    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, IList<CategoryDto>>
    {
        private readonly INet6WebApiTemplateDbContext _dbContext;

        public GetCategoryQueryHandler(INet6WebApiTemplateDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IList<CategoryDto>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var categories = await _dbContext.Categories
               .Select(category => new CategoryDto
               {
                   Id = category.Id,
                   CategoryName = category.CategoryName,
                   Description = category.Description
               })
               .ToListAsync(cancellationToken);

            return categories;

        }
    }
}
