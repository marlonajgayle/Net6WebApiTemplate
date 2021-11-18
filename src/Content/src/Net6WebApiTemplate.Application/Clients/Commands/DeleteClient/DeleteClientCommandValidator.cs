using FluentValidation;

namespace Net6WebApiTemplate.Application.Clients.Commands.DeleteClient
{
    public class DeleteClientCommandValidator : AbstractValidator<DeleteClientCommand>
    {
        public DeleteClientCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotNull().WithMessage("id field is required")
                .LessThan(1).WithMessage("Invalid id.");
        }
    }
}