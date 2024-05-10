using MediatR;

namespace Exercises_API.Infrastructure.Commands;

public class ApproveCommand : IRequest
{
    public int? Id { get; set;}

    public ApproveCommand(int? id)
    {
        this.Id = id;
    }

    public ApproveCommand() { }
}