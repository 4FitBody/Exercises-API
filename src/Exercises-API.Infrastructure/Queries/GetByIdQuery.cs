namespace Exercises_API.Infrastructure.Queries;

using Exercises_API.Core.Models;
using MediatR;

public class GetByIdQuery : IRequest<Exercise>
{
    public int Id { get; set; }

    public GetByIdQuery(int id)
    {
        this.Id = id;
    }

    public GetByIdQuery() { }
}