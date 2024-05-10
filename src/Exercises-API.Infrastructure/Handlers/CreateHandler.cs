namespace Exercises_API.Infrastructure.Handlers;

using System.Threading;
using System.Threading.Tasks;
using Exercises_API.Core.Repositories;
using Exercises_API.Infrastructure.Commands;
using MediatR;

public class CreateHandler : IRequestHandler<CreateCommand>
{
    private readonly IExerciseRepository exerciseRepository;

    public CreateHandler(IExerciseRepository exerciseRepository) => this.exerciseRepository = exerciseRepository;

    public async Task Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request.Exercise);

        await this.exerciseRepository.CreateAsync(request.Exercise);
    }
}