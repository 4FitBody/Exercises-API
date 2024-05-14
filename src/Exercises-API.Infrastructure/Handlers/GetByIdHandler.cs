namespace Exercises_API.Infrastructure.Handlers;

using MediatR;
using Exercises_API.Core.Models;
using Exercises_API.Core.Repositories;
using Exercises_API.Infrastructure.Queries;

public class GetByIdHandler : IRequestHandler<GetByIdQuery, Exercise>
{
    private readonly IExerciseRepository exerciseRepository;

    public GetByIdHandler(IExerciseRepository exerciseRepository) => this.exerciseRepository = exerciseRepository;

    public async Task<Exercise> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request.Id);

        var exercise = await this.exerciseRepository.GetByIdAsync((int)request.Id);

        if (exercise is null)
        {
            return new Exercise();
        }

        return exercise!;
    }
}