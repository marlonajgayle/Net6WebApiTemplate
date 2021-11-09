using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Net6WebApiTemplate.Application.Clients.Commands.CreateClient
{
    public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
    {
        private readonly IStringLocalizer<Messages> _localizer;

        public CreateClientCommandValidator(IStringLocalizer<Messages> localizer)
        {
            _localizer = localizer;

            RuleFor(v => v.FirstName)
                .NotEmpty().WithMessage(_localizer["FNameRequired"].Value);

            RuleFor(v => v.LastName)
                .NotEmpty().WithMessage("Last Name field is required.");

            RuleFor(v => v.Trn)
                .NotEmpty().WithMessage("TRN field is required.");

            RuleFor(v => v.AddressLine1)
                .NotEmpty().WithMessage("Addressline1 field is required.");

            RuleFor(v => v.Parish)
                .NotEmpty().WithMessage("Parish field is required.");
        }
    }
}