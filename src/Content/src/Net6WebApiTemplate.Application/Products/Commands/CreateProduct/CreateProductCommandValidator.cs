using FluentValidation;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net6WebApiTemplate.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        private readonly IStringLocalizer<Messages> _localizer;

        public CreateProductCommandValidator(IStringLocalizer<Messages> localizer)
        {
            _localizer = localizer;

            RuleFor(v => v.ProductName)
                .NotEmpty().WithMessage(_localizer["PNameRequired"].Value);

            RuleFor(v => v.UnitPrice)
                .NotEmpty().WithMessage(_localizer["UnitPriceRequired"].Value);

            RuleFor(v => v.CategoryId)
                .NotEmpty().WithMessage(_localizer["CategoryIdRequired"].Value);
        }
    }
}
