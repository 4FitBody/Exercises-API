namespace Exercises_API.Infrastructure.Commands;

using Exercises_API.Core.Models;
using MediatR;

public class UpdateCommand : IRequest
{
    public int? Id { get; set; }
    public Exercise? Exercise { get; set; }

    public UpdateCommand(int? id, Exercise? exercise)
    {
        this.Id = id;

        this.Exercise = exercise;
    }

    public UpdateCommand() {}
}