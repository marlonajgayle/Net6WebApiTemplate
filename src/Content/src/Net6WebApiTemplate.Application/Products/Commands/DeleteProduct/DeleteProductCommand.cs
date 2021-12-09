using MediatR;
using System;

public class DeleteProductCommand : IRequest<bool>
{
    public int Id { get; set; }
}
