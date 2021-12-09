using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net6WebApiTemplate.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotNull().WithMessage("id field is required")
                .LessThan(1).WithMessage("Invalid id.");
        }
    }
}
