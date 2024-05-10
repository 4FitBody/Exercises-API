namespace Exercises_API.Infrastructure.Queries;

using Exercises_API.Core.Models;
using MediatR;

public class GetAllQuery : IRequest<IEnumerable<Exercise>>
{

}