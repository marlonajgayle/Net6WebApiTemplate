using FluentValidation;

namespace Net6WebApiTemplate.Application.Clients.Commands.UpdateClient
{
    public class UpdateClientCommandValidator : AbstractValidator<UpdateClientCommand>
    {
        public UpdateClientCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotNull().WithMessage("id field required.")
                .LessThan(1).WithMessage("Invalid id");
        }
    }
}