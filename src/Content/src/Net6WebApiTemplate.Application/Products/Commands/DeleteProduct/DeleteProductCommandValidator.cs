﻿using FluentValidation;

namespace Net6WebApiTemplate.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotNull().WithMessage("id field is required")
                .GreaterThan(0).WithMessage("Invalid id.");
        }
    }
}
