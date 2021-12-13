using FluentValidation;
using Microsoft.Extensions.Localization;
using Net6WebApiTemplate.Application.Categories.Commands.CreateCategory;

namespace Net6WebApiTemplate.Application.Categories.Commands.CreateProduct
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        private readonly IStringLocalizer<Messages> _localizer;

        public CreateCategoryCommandValidator(IStringLocalizer<Messages> localizer)
        {
            _localizer = localizer;

            RuleFor(v => v.CategoryName)
                .NotEmpty().WithMessage(_localizer["CNameRequired"].Value);

            RuleFor(v => v.Description)
                .NotEmpty().WithMessage(_localizer["DescriptionRequired"].Value);
        }
    }
}
