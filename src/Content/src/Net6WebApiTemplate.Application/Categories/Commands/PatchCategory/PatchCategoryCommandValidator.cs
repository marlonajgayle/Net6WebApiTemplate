using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Net6WebApiTemplate.Application.Categories.Commands.PatchCategory
{
    public class PatchCategoryCommandValidator : AbstractValidator<PatchCategoryCommand>
    {
        private readonly IStringLocalizer<Messages> _localizer;

        public PatchCategoryCommandValidator(IStringLocalizer<Messages> localizer)
        {
            _localizer = localizer;

            RuleFor(v => v.Id)
                .NotEmpty().WithMessage(_localizer["CIdRequired"].Value);
        }
    }
}
