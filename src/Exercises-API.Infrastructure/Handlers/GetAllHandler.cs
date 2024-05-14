#pragma warning disable CS8766
#pragma warning disable CS1998

namespace Exercises_API.Infrastructure.Handlers;

using MediatR;
using Exercises_API.Core.Models;
using Exercises_API.Core.Repositories;
using Exercises_API.Infrastructure.Queries;

public class GetAllHandler : IRequestHandler<GetAllQuery, IEnumerable<Exercise>>
{
    private readonly IExerciseRepository exerciseRepository;

    public GetAllHandler(IExerciseRepository exerciseRepository) => this.exerciseRepository = exerciseRepository;

    public async Task<IEnumerable<Exercise>>? Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        var exercises = this.exerciseRepository.GetAll();

        if (exercises is null)
        {
            return Enumerable.Empty<Exercise>();
        }

        return exercises;
    }
}