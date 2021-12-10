using MediatR;
using Net6WebApiTemplate.Domain.Entities;
using System;
namespace Net6WebApiTemplate.Application.Products.Commands.CreateProduct;
public class CreateProductCommand : IRequest<Product>
{
    public string? ProductName { get; set; }
    public decimal? UnitPrice { get; set; }
    public int CategoryId { get; set; }
}
