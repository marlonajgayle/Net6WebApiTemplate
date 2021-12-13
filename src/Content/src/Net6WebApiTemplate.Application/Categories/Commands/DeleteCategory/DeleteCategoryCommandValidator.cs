using FluentValidation;
using Net6WebApiTemplate.Application.Categories.Commands.DeleteCategory;

namespace Net6WebApiTemplate.Application.Categories.Commands.DeleteProduct
{
    public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotNull().WithMessage("id field is required")
                .LessThan(1).WithMessage("Invalid id.");
        }
    }
}
