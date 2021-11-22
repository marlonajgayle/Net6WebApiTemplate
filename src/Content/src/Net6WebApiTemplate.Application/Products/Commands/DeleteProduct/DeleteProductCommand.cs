using MediatR;
using System;

public class DeleteProductCommand : IRequest<bool>
{
    public int id { get; set; }
}
