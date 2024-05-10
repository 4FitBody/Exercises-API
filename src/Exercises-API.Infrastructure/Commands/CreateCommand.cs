namespace Exercises_API.Infrastructure.Commands;

using Exercises_API.Core.Models;
using MediatR;

public class CreateCommand : IRequest
{
    public Exercise? Exercise { get; set; }

    public CreateCommand(Exercise? exercise) => this.Exercise = exercise;

    public CreateCommand() {}
}