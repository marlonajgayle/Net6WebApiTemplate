using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Net6WebApiTemplate.Application.Products.Commands.PatchProduct
{
    public class PatchProductCommandValidator : AbstractValidator<PatchProductCommand>
    {
        private readonly IStringLocalizer<Messages> _localizer;

        public PatchProductCommandValidator(IStringLocalizer<Messages> localizer)
        {
            _localizer = localizer;

            RuleFor(v => v.Id)
                .NotEmpty().WithMessage(_localizer["PIdRequired"].Value);
        }
    }
}
