using FluentValidation;

namespace Net6WebApiTemplate.Application.Clients.Commands.CreateClient
{
    public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
    {
        public CreateClientCommandValidator()
        {
            RuleFor(v => v.FirstName)
                .NotEmpty().WithMessage("First Name field is required.");

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